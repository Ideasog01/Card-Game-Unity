using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    [SerializeField]
    private Image playerWeaponIcon;

    [SerializeField]
    private WeaponController playerWeapon;

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

    public WeaponController PlayerWeapon
    {
        get { return playerWeapon; }
    }

    public void PlayCard(Card card, Target t)
    {
        TargetController target = t.TargetControllerRef;

        if (t != null)
        {
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

                        if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                        {
                            foreach(CardEffect effect in card.CardEffectList)
                            {
                                if(effect.ActivationTypeRef == CardEffect.ActivationType.OnCardPlayed)
                                {
                                    GameplayManager.cardEffectManager.CardEffect(effect, t);
                                }
                            }
                            
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

                        if (GameUtilities.HasMana(this, card.ManaCost, (int)card.ObjectManaType))
                        {
                            if (target.CreatureControlllerRef != null && target.CreatureControlllerRef.CreatureCard != null)
                            {
                                target.CreatureControlllerRef.ChangeCreatureProperties(card.WeaponAttack);
                                OnCardPlayed(card);
                            }
                            else if (target.PlayerControllerRef != null)
                            {
                                target.PlayerControllerRef.AssignWeapon(card);
                                OnCardPlayed(card);
                            }
                        }

                        break;
                }
            }
        }
    }

    public void AssignWeapon(Card card)
    {
        playerWeapon.AssignedWeapon = card;
        playerWeaponIcon.sprite = card.CardArt;
    }

    private void OnCardPlayed(Card card)
    {
        GameUtilities.RemoveCard(this, card);
        onCardPlayedEvent.Invoke();
        Debug.Log("CARD PLAYED");
    }
}
