using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerEntityController
{
    public Target hoverTarget;

    public Target clickedTarget;

    public CardController selectedCard;

    public GameObject cardSelectDisplay;

    private void Start()
    {
        foreach (TargetController target in GameplayManager.targetControllerList)
        {
            if (target.PlayerControllerRef != null)
            {
                GameplayManager.playerList.Add(target.PlayerControllerRef);
            }

            if (target.SlotControllerRef != null)
            {
                GameplayManager.slotList.Add(target.SlotControllerRef);
            }

            if (target.CreatureControlllerRef != null)
            {
                GameplayManager.creatureList.Add(target.CreatureControlllerRef);
            }

            if(target.StructureControllerRef != null)
            {
                GameplayManager.structureList.Add(target.StructureControllerRef);
            }

            if(target.WeaponControllerRef != null)
            {
                GameplayManager.weaponList.Add(target.WeaponControllerRef);
            }

            if(target.EnchantmentControllerRef != null)
            {
                GameplayManager.enchantmentList.Add(target.EnchantmentControllerRef);
            }

            if(target.AbilityControllerRef != null)
            {
                GameplayManager.abilityList.Add(target.AbilityControllerRef);
            }

            if(target.TributeControllerRef != null)
            {
                GameplayManager.tributeList.Add(target.TributeControllerRef);
            }
        }
    }

    private void Update()
    {
        EnterDetection();

        if(cardSelectDisplay.activeSelf)
        {
            cardSelectDisplay.transform.position = Input.mousePosition;
        }
    }

    public void SelectCard(CardController cardController)
    {
        if (!cardSelectDisplay.activeSelf)
        {
            selectedCard = cardController;
        }
    }

    public void ClickCard()
    {
        if(selectedCard != null)
        {
            cardSelectDisplay.SetActive(true);
            selectedCard.gameObject.SetActive(false);

            GameplayManager.gameDisplay.DisplayTargets(selectedCard.AssignedCard, this, true);
        }
    }

    public void ReleaseCard()
    {
        if(cardSelectDisplay.activeSelf && selectedCard != null)
        {
            selectedCard.gameObject.SetActive(true);

            if (hoverTarget != null)
            {
                if(selectedCard.AssignedCard.DoesNotRequireTarget)
                {
                    PlayCard(selectedCard.AssignedCard, this);
                }
                else if (GameplayManager.potentialTargets.Contains(hoverTarget))
                {
                    PlayCard(selectedCard.AssignedCard, hoverTarget);
                }
            }

            cardSelectDisplay.SetActive(false);
            selectedCard = null;

            Debug.Log("Card Released");

            GameplayManager.gameDisplay.HideDisplayTargets();
        }
    }

    public void ClickDetection()
    {
        if(clickedTarget != null)
        {
            Target newTarget =  FindTarget();

            if(newTarget != null && clickedTarget != newTarget)
            {
                switch (clickedTarget.TargetType)
                {
                    case Card.TargetType.Creature:

                        if(GameplayManager.potentialTargets.Contains(newTarget))
                        {
                            if (newTarget.TargetType == Card.TargetType.Creature)
                            {
                                clickedTarget.TargetControllerRef.CreatureControlllerRef.FightCreature(newTarget.TargetControllerRef.CreatureControlllerRef);
                            }
                            else if (newTarget.TargetType == Card.TargetType.Player)
                            {
                                newTarget.TargetControllerRef.PlayerControllerRef.TakeDamage(clickedTarget.TargetControllerRef.CreatureControlllerRef.CreatureAttack, clickedTarget);
                            }
                        }

                        break;

                    case Card.TargetType.Weapon:

                        if (GameplayManager.potentialTargets.Contains(newTarget))
                        {
                            if (newTarget.TargetType == Card.TargetType.Creature)
                            {
                                newTarget.TargetControllerRef.CreatureControlllerRef.TakeDamage(PlayerWeapon.AssignedWeapon.WeaponAttack, PlayerWeapon);
                            }
                            else if (newTarget.TargetType == Card.TargetType.Player)
                            {
                                newTarget.TargetControllerRef.PlayerControllerRef.TakeDamage(PlayerWeapon.AssignedWeapon.WeaponAttack, PlayerWeapon);
                            }
                        }

                        break;
                }
            }


            GameplayManager.gameDisplay.HideDisplayTargets();

            clickedTarget = null;
        }
        else
        {
            clickedTarget = FindTarget();

            if (clickedTarget != null)
            {
                Card card = null;

                if (clickedTarget.TargetType == Card.TargetType.Creature)
                {
                    card = clickedTarget.TargetControllerRef.CreatureControlllerRef.CreatureCard;
                }
                else if (clickedTarget.TargetType == Card.TargetType.Structure)
                {
                    card = clickedTarget.TargetControllerRef.StructureControllerRef.StructureCard;
                }
                else if(clickedTarget.TargetType == Card.TargetType.Weapon)
                {
                    card = clickedTarget.TargetControllerRef.WeaponControllerRef.AssignedWeapon;
                }
                else if(clickedTarget.TargetType == Card.TargetType.Ability)
                {
                    TargetControllerRef.AbilityControllerRef.ActivateAbility();
                }

                if (card != null)
                {
                    if (card.TargetTypeArray.Count > 0)
                    {
                        GameplayManager.gameDisplay.DisplayTargets(card, clickedTarget, false);
                    }
                    else
                    {
                        clickedTarget = null;
                    }
                }
                else
                {
                    clickedTarget = null;
                }
            }
        }
    }

    private void EnterDetection() //Run every tick to establish which target is currently hovered over.
    {
        hoverTarget = FindTarget();

        if(hoverTarget != null)
        {
            Debug.Log("Hover Target: " + hoverTarget);
        }
    }

    private Target FindTarget()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (PlayerEntityController player in GameplayManager.playerList)
        {
            if (player.BoxCollider.bounds.Contains(mousePosition))
            {
                return player;
            }
        }

        foreach (CreatureController creature in GameplayManager.creatureList)
        {
            if (creature.BoxCollider.bounds.Contains(mousePosition))
            {
                return creature;
            }
        }

        foreach (StructureController structure in GameplayManager.structureList)
        {
            if (structure.BoxCollider.bounds.Contains(mousePosition))
            {
                return structure;
            }
        }

        foreach (SlotController slot in GameplayManager.slotList)
        {
            if (slot.BoxCollider.bounds.Contains(mousePosition))
            {
                return slot;
            }
        }

        foreach(WeaponController weapon in GameplayManager.weaponList)
        {
            if(weapon.BoxCollider.bounds.Contains(mousePosition))
            {
                return weapon;
            }
        }

        foreach(EnchantmentController enchantment in GameplayManager.enchantmentList)
        {
            if(enchantment.BoxCollider.bounds.Contains(mousePosition))
            {
                return enchantment;
            }
        }

        foreach(AbilityController ability in GameplayManager.abilityList)
        {
            if(ability.BoxCollider.bounds.Contains(mousePosition))
            {
                return ability;
            }
        }

        foreach(TributeController tribute in GameplayManager.tributeList)
        {
            if(tribute.BoxCollider.bounds.Contains(mousePosition))
            {
                return tribute;
            }
        }

        return null;
    }
}
