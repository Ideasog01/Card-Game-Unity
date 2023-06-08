using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{
    private Card _structureCard;
    private SlotController _slot;

    public Card StructureCard
    {
        get { return _structureCard; }
        set { _structureCard = value; }
    }

    public SlotController AssignedSlot
    {
        get { return _slot; }
    }
}
