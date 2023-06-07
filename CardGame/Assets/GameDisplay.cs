using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameDisplay : MonoBehaviour
{
    [Header("General References")]

    [SerializeField]
    private SlotController[] slotArray;

    [SerializeField]
    private GameObject greyOverlay;

    [SerializeField]
    private Transform slotDefaultParent;

    [Header("Player Display")]

    [SerializeField]
    private TextMeshProUGUI playerHealthText;

    [Header("Enemy Display")]

    [SerializeField]
    private TextMeshProUGUI enemyHealthText;

    public void DisplayTargets(SlotController slot)
    {
        greyOverlay.SetActive(true);

        foreach (SlotController slotElement in slotArray)
        {
            if (slotElement != slot && slotElement.AssignedCreatureController != null && slotElement.AssignedCreatureController.CreatureCard != null)
            {
                if (slotElement.AssignedPlayer != slot.AssignedPlayer) 
                {
                    if (GameUtilities.IsCreatureRange(slotElement.AssignedCreatureController, slot.AssignedCreatureController)) //Highlight Creature (In-Range)
                    {
                        slotElement.AssignedCreatureController.transform.SetParent(greyOverlay.transform);
                        return;
                    }
                }

                //Do not highlight creature (Out of Range)
                slotElement.transform.SetParent(slotDefaultParent);
            }
        }
    }

    public void HideDisplayTargets()
    {
        for(int i = 0; i < greyOverlay.transform.childCount; i++)
        {
            greyOverlay.transform.GetChild(i).SetParent(slotDefaultParent);
        }

        greyOverlay.SetActive(false);
    }

    public void DisplayEnemyHealth()
    {
        enemyHealthText.text = GameplayManager.enemyPlayer.PlayerHealth.ToString();
    }

    public void DisplayPlayerHealth()
    {
        playerHealthText.text = GameplayManager.humanPlayer.PlayerHealth.ToString();
    }
}
