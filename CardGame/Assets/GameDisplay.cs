using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDisplay : MonoBehaviour
{
    [SerializeField]
    private SlotController[] slotArray;

    [SerializeField]
    private GameObject greyOverlay;

    [SerializeField]
    private Transform slotDefaultParent;

    public void DisplayTargets(SlotController slot)
    {
        greyOverlay.SetActive(true);

        foreach(SlotController slotElement in slotArray)
        {
            if(slotElement != slot && slotElement.AssignedCreatureController != null && slotElement.AssignedCreatureController.CreatureCard != null)
            {
                if(slotElement.AssignedCreatureController.AssignedPlayer != slot.AssignedCreatureController.AssignedPlayer)
                {
                    slotElement.AssignedCreatureController.transform.SetParent(greyOverlay.transform);
                }
            }
            else
            {
                slotElement.transform.SetParent(slotDefaultParent);
            }
        }
    }

    public void HideDisplayTargets()
    {
        foreach (SlotController slotElement in slotArray)
        {
            slotElement.transform.SetParent(slotDefaultParent);
        }

        greyOverlay.SetActive(false);
    }
}
