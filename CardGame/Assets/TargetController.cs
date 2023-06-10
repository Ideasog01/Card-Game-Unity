using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Card;
using static UnityEngine.GraphicsBuffer;

public class TargetController : MonoBehaviour
{

    [SerializeField]
    private PlayerEntityController playerController;

    [SerializeField]
    private SlotController slotController;

    [SerializeField]
    private CreatureController creatureController;

    [SerializeField]
    private StructureController structureController;

    public PlayerEntityController PlayerControllerRef
    {
        get { return playerController; }
    }

    public SlotController SlotControllerRef
    {
        get { return slotController; }
    }

    public CreatureController CreatureControlllerRef
    {
        get { return creatureController; }
    }

    public StructureController StructureControllerRef
    {
        get { return structureController; }
    }

    public int HighlightTarget(Transform overlay, Card card, Target attacker)
    {
        int targets = 0;

        if(playerController != null)
        {
            if(card.TargetTypeArray.Contains(Card.TargetType.Player) && attacker != playerController && attacker.AssignedPlayer != playerController || card.CanAttackFriendly)
            {
                playerController.PlayerPortrait.SetParent(overlay);
                GameplayManager.potentialTargets.Add(playerController);
                targets++;
            }
            else
            {
                playerController.PlayerPortrait.SetParent(playerController.DisplayDefaultParent);
            }
        }

        if(creatureController != null && creatureController.CreatureCard != null && creatureController != attacker)
        {
            if(attacker != creatureController)
            {
                if (card.TargetTypeArray.Contains(Card.TargetType.Creature) && GameUtilities.IsCreatureRange(creatureController, attacker.TargetControllerRef.CreatureControlllerRef))
                {
                    creatureController.CreatureUI.SetParent(overlay);
                    GameplayManager.potentialTargets.Add(creatureController);
                    targets++;
                }
                else
                {
                    creatureController.CreatureUI.SetParent(creatureController.DisplayDefaultParent);
                }
            }
        }

        if(structureController != null && structureController.StructureCard != null && structureController != attacker)
        {
            if(card.TargetTypeArray.Contains(Card.TargetType.Structure))
            {
                structureController.StructureUI.SetParent(overlay);
                GameplayManager.potentialTargets.Add(structureController);
                targets++;
            }
            else
            {
                structureController.StructureUI.SetParent(structureController.DisplayDefaultParent);
            }
        }
        
        if(slotController != null && slotController.ManaCard == null)
        {
            if(card.TargetTypeArray.Contains(Card.TargetType.Slot))
            {
                slotController.SlotBorder.transform.SetParent(overlay);
                GameplayManager.potentialTargets.Add(slotController);
                targets++;
            }
            else
            {
                slotController.SlotBorder.transform.SetParent(slotController.DefaultParent);
            }
        }

        return targets;
    }

    public int HighlightCardTarget(Transform overlay, Card card, Target attacker)
    {
        int targets = 0;

        if (playerController != null)
        {
            if (card.CardReleaseTargetArray.Contains(Card.TargetType.Player) && attacker != playerController && attacker.AssignedPlayer != playerController || card.CanAttackFriendly)
            {
                playerController.PlayerPortrait.SetParent(overlay);
                GameplayManager.potentialTargets.Add(playerController);
                targets++;
            }
            else
            {
                playerController.PlayerPortrait.SetParent(playerController.DisplayDefaultParent);
            }
        }

        if (creatureController != null && creatureController.CreatureCard != null && creatureController != attacker)
        {
            if (attacker != creatureController)
            {
                if (card.CardReleaseTargetArray.Contains(Card.TargetType.Creature))
                {
                    creatureController.CreatureUI.SetParent(overlay);
                    GameplayManager.potentialTargets.Add(creatureController);
                    targets++;
                }
                else
                {
                    creatureController.CreatureUI.SetParent(creatureController.DisplayDefaultParent);
                }
            }
        }

        if (structureController != null && structureController.StructureCard != null && structureController != attacker)
        {
            if (card.CardReleaseTargetArray.Contains(Card.TargetType.Structure))
            {
                structureController.StructureUI.SetParent(overlay);
                GameplayManager.potentialTargets.Add(structureController);
                targets++;
            }
            else
            {
                structureController.StructureUI.SetParent(structureController.DisplayDefaultParent);
            }
        }

        if (slotController != null)
        {
            if(slotController.ManaCard == null && card.Object_cardType == Card.CardType.Mana || card.Object_cardType == CardType.Creature && slotController.ManaCard != null && creatureController.CreatureCard == null || card.Object_cardType == CardType.Structure && slotController.ManaCard != null && structureController.StructureCard == null)
            {
                if (card.CardReleaseTargetArray.Contains(Card.TargetType.Slot))
                {
                    slotController.SlotBorder.transform.SetParent(overlay);
                    GameplayManager.potentialTargets.Add(slotController);
                    targets++;
                }
                else
                {
                    slotController.SlotBorder.transform.SetParent(slotController.DefaultParent);
                }
            }
        }

        return targets;
    }

    public void HideTarget()
    {
        if (playerController != null)
        {
            playerController.PlayerPortrait.SetParent(playerController.DisplayDefaultParent);
        }

        if (creatureController != null && creatureController.CreatureCard != null)
        {
            creatureController.CreatureUI.SetParent(creatureController.DisplayDefaultParent);
        }

        if(structureController != null && structureController.StructureCard != null)
        {
            structureController.StructureUI.SetParent(structureController.DisplayDefaultParent);
        }

        if(slotController != null)
        {
            slotController.SlotBorder.transform.SetParent(slotController.DefaultParent);
        }
    }
}
