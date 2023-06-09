using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void HighlightTarget(Transform overlay, bool highlight, List<Card.TargetType> targetTypes)
    {
        if(playerController != null)
        {
            if(highlight && targetTypes.Contains(Card.TargetType.Player))
            {
                playerController.PlayerPortrait.SetParent(overlay);
            }
            else
            {
                playerController.PlayerPortrait.SetParent(playerController.DisplayDefaultParent);
            }
        }

        if(creatureController != null && creatureController.CreatureCard != null)
        {
            if(highlight && targetTypes.Contains(Card.TargetType.Creature))
            {
                creatureController.CreatureUI.SetParent(overlay);
            }
            else
            {
                creatureController.CreatureUI.SetParent(creatureController.DisplayDefaultParent);
            }
        }

        if(structureController != null && structureController.StructureCard != null)
        {
            if(highlight && targetTypes.Contains(Card.TargetType.Structure))
            {
                structureController.StructureUI.SetParent(overlay);
            }
            else
            {
                structureController.StructureUI.SetParent(structureController.DisplayDefaultParent);
            }
        }
        
        if(slotController != null && slotController.ManaCard == null)
        {
            if(highlight && targetTypes.Contains(Card.TargetType.Slot))
            {
                slotController.SlotBorder.transform.SetParent(overlay);
            }
            else
            {
                slotController.SlotBorder.transform.SetParent(slotController.DefaultParent);
            }
        }
    }
}
