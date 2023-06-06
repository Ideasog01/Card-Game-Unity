using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static SlotController selectedSlot;

    public static CardController cardSelected;

    public GameObject cardSelectDisplay;

    [SerializeField]
    private CreatureController selectedCreature;

    [SerializeField]
    private int[] manaAmountArray;

    [SerializeField]
    private SlotController[] slotArray;

    [SerializeField]
    private int playerHealth;

    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    [SerializeField]
    private List<Card> playerHand = new List<Card>();

    private int _fatigueAmount = 1;

    private CardDisplayManager _cardDisplayManager;

    private GameplayManager _gameplayManager;

    private SpellManager _spellManager;

    public List<Card> PlayerHand
    {
        get { return playerHand; }
    }

    public List<Card> PlayerCards
    {
        get { return playerCards; }
    }

    public int[] ManaAmountArray
    {
        get { return manaAmountArray; }
    }

    private void Awake()
    {
        _cardDisplayManager = this.GetComponent<CardDisplayManager>();
        _gameplayManager = GameObject.Find("GameManager").GetComponent<GameplayManager>();
        _spellManager = GameObject.Find("GameManager").GetComponent<SpellManager>();
    }

    private void Start()
    {
        _cardDisplayManager.DisplayCardData(playerHand);
        _cardDisplayManager.DisplayMana();
    }

    private void Update()
    {
        CheckSelectSlot();

        if (selectedCreature == null)
        {
            HandCardSelect();
            SelectCreature();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if(selectedSlot != null)
            {
                CreatureController creatureController = selectedSlot.AssignedCreatureController;

                if (creatureController != null)
                {
                    Card creatureCard = creatureController.CreatureCard;

                    if(creatureCard != null)
                    {
                        if (creatureController.AssignedPlayer != this) //If the creature belongs to ANOTHER player, attack this creature
                        {
                            selectedCreature.FightCreature(creatureController);
                            selectedCreature = null;
                            Debug.Log("CREATURE FIGHT!");
                        }
                        else
                        {
                            Debug.Log("CREATURE BELONGS TO PLAYER");
                        }
                    }

                    GameplayManager.gameDisplay.HideDisplayTargets();
                    selectedCreature = null;
                }
            }
        }
    }

    public void SelectCard(CardController selectedCard)
    {
        if (!cardSelectDisplay.activeSelf)
        {
            cardSelected = selectedCard;
        }
    }

    public void DrawCard()
    {
        if(playerCards.Count == 0)
        {
            //Player Take Damage
            playerHealth -= _fatigueAmount;
            _fatigueAmount++;
            return;
        }

        Card newCard = playerCards[playerCards.Count - 1];

        if(playerHand.Count < 10)
        {
            playerHand.Add(newCard);         
        }
        else
        {
            //Play Card Destroyed Animation
        }

        playerCards.RemoveAt(playerCards.Count - 1);
        _cardDisplayManager.DisplayCardData(playerHand);
    }

    public void RemoveCard(Card card)
    {
        playerHand.Remove(card);
        _cardDisplayManager.DisplayCardData(playerHand);
    }

    public void ShuffleHand()
    {
        for (int i = 0; i < playerCards.Count; i++)
        {
            int rnd = Random.Range(0, playerCards.Count);
            Card tempCard = playerCards[rnd];
            playerCards[rnd] = playerCards[i];
            playerCards[i] = tempCard;
        }
    }

    public void SelectCreature()
    {
        if(selectedSlot != null)
        {
            if(selectedSlot.CreatureCard != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CreatureController creature = selectedSlot.AssignedCreatureController;

                    if(selectedCreature == null) //Player has not selected creature
                    {
                        if (creature.AssignedPlayer == this) //If the creature belongs to the player
                        {
                            selectedCreature = creature;
                            GameplayManager.gameDisplay.DisplayTargets(selectedSlot);
                            Debug.Log("CREATURE SELECT!");
                        }
                        else
                        {
                            Debug.Log("CREATURE DOES NOT BELONG TO PLAYER");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("SELECTED SLOT IS NULL");
        }
    }

    private void HandCardSelect()
    {
        if (cardSelected != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cardSelectDisplay.SetActive(true);
                cardSelected.gameObject.SetActive(false);
            }

            if (cardSelectDisplay.activeSelf)
            {
                cardSelectDisplay.transform.position = Input.mousePosition;

                if (Input.GetMouseButtonUp(0))
                {
                    cardSelectDisplay.SetActive(false);
                    cardSelected.gameObject.SetActive(true);

                    StartCoroutine(PlayCard(cardSelected.AssignedCard));
                }
            }
        }
    }

    private void AddManaToCounter(Card.ManaType manaType, int amount)
    {
        manaAmountArray[(int)manaType] += amount;
    }

    private IEnumerator PlayCard(Card card)
    {
        yield return new WaitForSeconds(0.05f);

        if (selectedSlot != null)
        {
            switch (card.Object_cardType)
            {
                case Card.CardType.Mana:
                    if(selectedSlot.ManaCard == null)
                    {
                        selectedSlot.AddMana(card);
                        AddManaToCounter(card.ObjectManaType, card.ManaGain);
                        OnCardPlayed();
                    }
                    break;
                case Card.CardType.Creature:
                    if (selectedSlot.ManaCard != null)
                    {
                        if (selectedSlot.CreatureCard == null)
                        {
                            if (HasMana(card.ManaCost, (int)card.ObjectManaType))
                            {
                                selectedSlot.AddCreature(card, this);
                                OnCardPlayed();
                            }
                        }
                    }
                    break;
                case Card.CardType.Spell:
                    if(selectedSlot.CreatureCard != null)
                    {
                        if (selectedSlot.AssignedCreatureController.AssignedPlayer != this) //If the creature belongs to ANOTHER player, attack this creature
                        {
                            if (HasMana(card.ManaCost, (int)card.ObjectManaType))
                            {
                                _spellManager.CastSpell(card, selectedSlot);
                                OnCardPlayed();
                            }
                        }
                        else
                        {
                            Debug.Log("Target for spell belongs to the owning player");
                            cardSelected = null;
                            cardSelectDisplay.SetActive(false);
                        }
                    }
                    break;
            }
        }
    }

    private void OnCardPlayed()
    {
        RemoveCard(cardSelected.AssignedCard);
        _cardDisplayManager.DisplayMana();
        cardSelected = null;
        Debug.Log("CARD PLAYED");
    }

    private bool HasMana(int amount, int manaIndex)
    {
        if(manaAmountArray[manaIndex] >= amount)
        {
            manaAmountArray[manaIndex] -= amount;
            return true;
        }

        return false;
    }

    private void CheckSelectSlot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (SlotController slot in slotArray)
        {
            if (slot.SlotBox.bounds.Contains(mousePosition))
            {
                selectedSlot = slot;
                Debug.Log("Slot Assigned");
            }
            else if(slot == selectedSlot)
            {
                selectedSlot = null;
            }
        }
    }
}
