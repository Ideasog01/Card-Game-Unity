using Photon.Pun.Demo.Cockpit.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static SlotController selectedSlot;

    public static CardController cardSelected;

    [SerializeField]
    private int[] manaAmountArray;

    [SerializeField]
    private int playerHealth;

    [SerializeField]
    private GameObject cardSelectDisplay;

    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    [SerializeField]
    private List<Card> playerHand = new List<Card>();

    private int _fatigueAmount = 1;

    private CardDisplayManager _cardDisplayManager;

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
    }

    private void Start()
    {
        _cardDisplayManager.DisplayCardData(playerHand);
        _cardDisplayManager.DisplayMana();
    }

    private void Update()
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

    private void AddManaToCounter(Card.ManaType manaType, int amount)
    {
        manaAmountArray[(int)manaType] += amount;
    }

    private IEnumerator PlayCard(Card card)
    {
        yield return new WaitForSeconds(0.15f);

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
                                selectedSlot.AddCreature(card);
                                OnCardPlayed();
                            }
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
}
