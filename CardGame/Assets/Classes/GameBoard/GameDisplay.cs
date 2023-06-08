using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameDisplay : MonoBehaviour
{
    [Header("General References")]

    public Sprite[] creatureReachIcons;

    [SerializeField]
    private SlotController[] slotArray;

    [SerializeField]
    private GameObject greyOverlay;

    [Header("Player Display")]

    public Color humanColour;

    [SerializeField]
    private TextMeshProUGUI playerHealthText;

    [Header("Enemy Display")]

    public Color enemyColour;

    [SerializeField]
    private TextMeshProUGUI enemyHealthText;

    public void DisplayTargets(SlotController slot)
    {
        greyOverlay.SetActive(true);

        foreach (SlotController slotElement in slotArray)
        {
            if (slotElement != slot && slotElement != null && slotElement.CreatureCard != null)
            {
                if (slotElement.AssignedPlayer != slot.AssignedPlayer) 
                {
                    if (GameUtilities.IsCreatureRange(slotElement, slot)) //Highlight Creature (In-Range)
                    {
                        slotElement.transform.SetParent(greyOverlay.transform);
                        return;
                    }
                }

                //Do not highlight creature (Out of Range)
                slotElement.transform.SetParent(slotElement.DefaultParent);
            }
        }
    }

    public void HideDisplayTargets()
    {
        for(int i = 0; i < greyOverlay.transform.childCount; i++)
        {
            greyOverlay.transform.GetChild(i).SetParent(greyOverlay.transform.GetChild(i).GetComponent<SlotController>().DefaultParent);
        }

        greyOverlay.SetActive(false);
    }

    public void DisplayEnemyHealth()
    {
        enemyHealthText.text = GameplayManager.enemyPlayer.EntityHealth.ToString();
    }

    public void DisplayPlayerHealth()
    {
        playerHealthText.text = GameplayManager.humanPlayer.EntityHealth.ToString();
    }
}
