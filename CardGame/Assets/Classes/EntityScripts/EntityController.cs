using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityController : MonoBehaviour
{
    [SerializeField]
    private int[] manaAmountArray;

    [SerializeField]
    private int playerHealth;

    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    [SerializeField]
    private List<Card> playerHand = new List<Card>();

    [SerializeField]
    private UnityEvent onCardPlayedEvent;

    private int _fatigueAmount = 1;

    public int[] ManaAmountArray
    {
        get { return manaAmountArray; }
    }

    public List<Card> PlayerCards
    {
        get { return playerCards; }
        set { playerCards = value; }
    }

    public List<Card> PlayerHand
    {
        get { return playerHand; }
    }

    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public int FatigueAmount
    {
        get { return _fatigueAmount; }
        set { _fatigueAmount = value; }
    }

    public IEnumerator PlayCard(Card card, SlotController slot)
    {
        yield return new WaitForSeconds(0.05f);

        if (slot != null)
        {
            switch (card.Object_cardType)
            {
                case Card.CardType.Mana:
                    if (slot.ManaCard == null)
                    {
                        slot.AddMana(card, this);
                        GameUtilities.AddMana(this, (int)card.ObjectManaType, card.ManaGain);
                        OnCardPlayed(card);
                    }
                    break;
                case Card.CardType.Creature:
                    if (slot.ManaCard != null && slot.AssignedPlayer == this)
                    {
                        if (slot.AssignedCreatureController.CreatureCard == null)
                        {
                            if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                            {
                                slot.AddCreature(card, this);
                                OnCardPlayed(card);
                            }
                        }
                    }
                    break;
                case Card.CardType.Spell:
                    if (slot.AssignedCreatureController.CreatureCard != null)
                    {
                        if (slot.AssignedCreatureController.AssignedSlot.AssignedPlayer != this) //If the creature belongs to ANOTHER player, attack this creature
                        {
                            if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                            {
                                GameplayManager.spellManager.CastSpell(card, slot);
                                OnCardPlayed(card);
                            }
                        }
                        else
                        {
                            Debug.Log("Target for spell belongs to the owning player");
                        }
                    }
                    break;
            }
        }
    }

    private void OnCardPlayed(Card card)
    {
        GameUtilities.RemoveCard(this, card);
        onCardPlayedEvent.Invoke();
        Debug.Log("CARD PLAYED");
    }
}
