using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEntityController : EntityController
{
    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    [SerializeField]
    private List<Card> playerHand = new List<Card>();

    [Header("Entity Events")]

    [SerializeField]
    private UnityEvent onCardPlayedEvent;

    [Header("Display")]

    [SerializeField]
    private Transform playerPortait;

    public List<Card> PlayerCards
    {
        get { return playerCards; }
        set { playerCards = value; }
    }

    public List<Card> PlayerHand
    {
        get { return playerHand; }
    }

    public Transform PlayerPortrait
    {
        get { return playerPortait; }
    }

    public void PlayCard(Card card, Target t)
    {
        TargetController target = t.TargetControllerRef;

        if (target != null)
        {
            switch (card.Object_cardType)
            {
                case Card.CardType.Mana:

                    if (target.SlotControllerRef != null && target.SlotControllerRef.ManaCard == null)
                    {
                        target.SlotControllerRef.AddMana(card, this);
                        GameUtilities.AddMana(this, (int)card.ObjectManaType, card.ManaGain);
                        OnCardPlayed(card);
                    }

                    break;
                case Card.CardType.Creature:

                    if (target.CreatureControlllerRef != null && target.SlotControllerRef != null && target.CreatureControlllerRef.CreatureCard == null && target.SlotControllerRef.AssignedPlayer == this)
                    {
                        if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                        {
                            target.CreatureControlllerRef.AddCreature(card);
                            OnCardPlayed(card);
                        }
                    }

                    break;
                case Card.CardType.Spell:

                    if(GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                    {
                        GameplayManager.cardEffectManager.OnCardReleaseEffect(card, t);
                        OnCardPlayed(card);
                    }

                    break;
                case Card.CardType.Structure:

                    if (target.StructureControllerRef != null && target.StructureControllerRef.StructureCard == null && target.SlotControllerRef.AssignedPlayer == this)
                    {
                        if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                        {
                            target.StructureControllerRef.AddStructure(card);
                            OnCardPlayed(card);
                        }
                    }

                    break;

                case Card.CardType.Equipment:

                    Debug.Log("Equipment are not implemented yet. :)");

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
