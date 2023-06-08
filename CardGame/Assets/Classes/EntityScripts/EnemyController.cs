using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField]
    public SlotController[] slotArray;

    public void PlayRandomCard()
    {
        SlotController slot = FindEmptySlot();

        if(slot != null && PlayerHand.Count > 0)
        {
            if(slot.AssignedPlayer == this || slot.ManaCard == null)
            {
                Card randomCard = PlayerHand[Random.Range(0, PlayerHand.Count - 1)];
                StartCoroutine(PlayCard(randomCard, slot));
            }
        }
    }

    private SlotController FindEmptySlot()
    {
        SlotController slot = null;

        foreach (SlotController slotElement in slotArray)
        {
            if (slotElement.CreatureCard == null)
            {
                slot = slotElement;
            }
        }


        return slot;
    }
}
