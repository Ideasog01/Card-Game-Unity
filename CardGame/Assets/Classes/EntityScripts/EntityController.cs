using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityController : MonoBehaviour
{
    [SerializeField]
    private int[] manaAmountArray;

    [SerializeField]
    private int entityHealth;

    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    [SerializeField]
    private List<Card> playerHand = new List<Card>();

    [Header("Entity Events")]

    [SerializeField]
    private UnityEvent onCardPlayedEvent;

    [SerializeField]
    private UnityEvent onEntityTakeDamageEvent;

    [SerializeField]
    private UnityEvent onEntityDeathEvent;

    private int _fatigueAmount = 1;

    private bool _isPlayerDead;

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

    public int EntityHealth
    {
        get { return entityHealth; }
        set { entityHealth = value; }
    }

    public int FatigueAmount
    {
        get { return _fatigueAmount; }
        set { _fatigueAmount = value; }
    }

    public virtual IEnumerator PlayCard(Card card, SlotController slot)
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
                    if (slot.ManaCard != null && slot.AssignedPlayer == this && slot.CreatureCard == null)
                    {
                        if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                        {
                            slot.AddCreature(card);
                            OnCardPlayed(card);
                        }
                    }
                    break;
                case Card.CardType.Spell:
                    if (slot.CreatureCard != null)
                    {
                        if (slot.AssignedPlayer != this) //If the creature belongs to ANOTHER player, attack this creature
                        {
                            if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                            {
                                GameplayManager.cardEffectManager.CardEffect(card, slot);
                                OnCardPlayed(card);
                            }
                        }
                        else
                        {
                            Debug.Log("Target for spell belongs to the owning player");
                        }
                    }
                    break;
                case Card.CardType.Structure:
                    if(slot.ManaCard != null && slot.AssignedPlayer == this && slot.StructureCard == null)
                    {
                        if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                        {
                            slot.AddStructure(card);
                            OnCardPlayed(card);
                        }
                    }
                    break;
            }
        }
    }

    public virtual IEnumerator PlayCard(Card card, EntityController entity)
    {
        yield return new WaitForSeconds(0.05f);

        if (entity != null)
        {
            switch (card.Object_cardType)
            {
                case Card.CardType.Mana:

                    break;
                case Card.CardType.Spell:
                    if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                    {
                        GameplayManager.cardEffectManager.CardEffect(card, entity);
                        OnCardPlayed(card);
                    }
                    break;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if(!_isPlayerDead)
        {
            entityHealth -= amount;
            onEntityTakeDamageEvent.Invoke();

            Debug.Log("Player Took Damage!");

            if (entityHealth <= 0)
            {
                PlayerDeath();
            }
        }
    }

    public virtual void UseWeapon(EntityController target, Card weapon)
    {

    }

    public virtual void UseWeapon(CreatureController creature, Card weapon)
    {

    }

    private void PlayerDeath()
    {
        onEntityDeathEvent.Invoke();
        _isPlayerDead = true;
    }

    private void OnCardPlayed(Card card)
    {
        GameUtilities.RemoveCard(this, card);
        onCardPlayedEvent.Invoke();
        Debug.Log("CARD PLAYED");
    }
}
