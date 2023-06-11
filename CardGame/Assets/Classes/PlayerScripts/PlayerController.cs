using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerEntityController
{
    public Target hoverTarget;

    public Target clickedTarget;

    public CardController selectedCard;

    public GameObject cardSelectDisplay;

    private List<PlayerEntityController> playerList = new List<PlayerEntityController>();
    private List<SlotController> slotList = new List<SlotController>();
    private List<CreatureController> creatureList = new List<CreatureController>();
    private List<StructureController> structureList = new List<StructureController>();
    private List<WeaponController> weaponList = new List<WeaponController>();

    private void Start()
    {
        foreach (TargetController target in GameplayManager.targetControllerList)
        {
            if (target.PlayerControllerRef != null)
            {
                playerList.Add(target.PlayerControllerRef);
            }

            if (target.SlotControllerRef != null)
            {
                slotList.Add(target.SlotControllerRef);
            }

            if (target.CreatureControlllerRef != null)
            {
                creatureList.Add(target.CreatureControlllerRef);
            }

            if(target.StructureControllerRef != null)
            {
                structureList.Add(target.StructureControllerRef);
            }

            if(target.WeaponControllerRef != null)
            {
                weaponList.Add(target.WeaponControllerRef);
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

            if(selectedCard.AssignedCard.Object_cardType == Card.CardType.Equipment)
            {
                AssignWeapon(selectedCard.AssignedCard);
            }
            else
            {
                if (hoverTarget != null)
                {
                    if (GameplayManager.potentialTargets.Contains(hoverTarget))
                    {
                        PlayCard(selectedCard.AssignedCard, hoverTarget);
                    }
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

                        if(newTarget.TargetType == Card.TargetType.Creature )
                        {
                            clickedTarget.TargetControllerRef.CreatureControlllerRef.FightCreature(newTarget.TargetControllerRef.CreatureControlllerRef);
                        }
                        else if(newTarget.TargetType == Card.TargetType.Player)
                        {
                            newTarget.TargetControllerRef.PlayerControllerRef.TakeDamage(clickedTarget.TargetControllerRef.CreatureControlllerRef.CreatureCard.CreatureAttack);
                        }

                        break;

                    case Card.TargetType.Weapon:

                        if (newTarget.TargetType == Card.TargetType.Creature)
                        {
                            newTarget.TargetControllerRef.CreatureControlllerRef.TakeDamage(PlayerWeapon.AssignedWeapon.WeaponAttack);
                        }
                        else if (newTarget.TargetType == Card.TargetType.Player)
                        {
                            newTarget.TargetControllerRef.PlayerControllerRef.TakeDamage(PlayerWeapon.AssignedWeapon.WeaponAttack);
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

        foreach (PlayerEntityController player in playerList)
        {
            if (player.BoxCollider.bounds.Contains(mousePosition))
            {
                return player;
            }
        }

        foreach (CreatureController creature in creatureList)
        {
            if (creature.BoxCollider.bounds.Contains(mousePosition))
            {
                return creature;
            }
        }

        foreach (StructureController structure in structureList)
        {
            if (structure.BoxCollider.bounds.Contains(mousePosition))
            {
                return structure;
            }
        }

        foreach (SlotController slot in slotList)
        {
            if (slot.BoxCollider.bounds.Contains(mousePosition))
            {
                return slot;
            }
        }

        foreach(WeaponController weapon in weaponList)
        {
            if(weapon.BoxCollider.bounds.Contains(mousePosition))
            {
                return weapon;
            }
        }

        return null;
    }
}
