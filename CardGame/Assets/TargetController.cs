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

    public int HighlightTarget(Transform overlay, List<Card.TargetType> targetTypes, Target attacker)
    {
        int targets = 0;

        if(playerController != null)
        {
            if (targetTypes.Contains(Card.TargetType.Player))
            {
                playerController.PlayerPortrait.SetParent(overlay);
                targets++;
            }
            else
            {
                playerController.PlayerPortrait.SetParent(playerController.DisplayDefaultParent);
            }
        }

        if(creatureController != null && creatureController.CreatureCard != null && creatureController != attacker)
        {
            if(targetTypes.Contains(Card.TargetType.Creature) && GameUtilities.IsCreatureRange(creatureController, attacker.TargetControllerRef.CreatureControlllerRef))
            {
                creatureController.CreatureUI.SetParent(overlay);
                targets++;
            }
            else
            {
                creatureController.CreatureUI.SetParent(creatureController.DisplayDefaultParent);
            }
        }

        if(structureController != null && structureController.StructureCard != null && structureController != attacker)
        {
            if(targetTypes.Contains(Card.TargetType.Structure))
            {
                structureController.StructureUI.SetParent(overlay);
                targets++;
            }
            else
            {
                structureController.StructureUI.SetParent(structureController.DisplayDefaultParent);
            }
        }
        
        if(slotController != null && slotController.ManaCard == null)
        {
            if(targetTypes.Contains(Card.TargetType.Slot))
            {
                slotController.SlotBorder.transform.SetParent(overlay);
                targets++;
            }
            else
            {
                slotController.SlotBorder.transform.SetParent(slotController.DefaultParent);
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

        if(slotController != null && slotController.ManaCard == null)
        {
            slotController.SlotBorder.transform.SetParent(slotController.DefaultParent);
        }
    }
}
