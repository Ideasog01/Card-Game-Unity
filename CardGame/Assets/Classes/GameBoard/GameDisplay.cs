using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameDisplay : MonoBehaviour
{
    [Header("General References")]

    public Sprite[] creatureReachIcons;

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

    public void DisplayTargets(Card card, Target attacker, bool isCardBeingPlayed)
    {
        greyOverlay.SetActive(true);

        int targetCount = 0;


        if (isCardBeingPlayed)
        {
            foreach (TargetController target in GameplayManager.targetControllerList)
            {
                targetCount += target.HighlightCardTarget(greyOverlay.transform, card, attacker);
            }
        }
        else
        {
            foreach (TargetController target in GameplayManager.targetControllerList)
            {
                targetCount += target.HighlightTarget(greyOverlay.transform, card, attacker);
            }
        }

        if(targetCount == 0)
        {
            greyOverlay.SetActive(false);
        }
    }

    public void HideDisplayTargets()
    {
        foreach (TargetController target in GameplayManager.targetControllerList)
        {
            target.HideTarget();
        }

        GameplayManager.potentialTargets.Clear();
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
