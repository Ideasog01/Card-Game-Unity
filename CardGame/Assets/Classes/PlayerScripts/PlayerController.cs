using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerEntityController
{
    public static TargetController hoverTarget;

    public static TargetController clickedTarget;

    public static CardController selectedCard;

    public GameObject cardSelectDisplay;

    [SerializeField]
    private TargetController[] targetArray;

    private void Update()
    {
        EnterDetection();
    }

    public void SelectCard(CardController cardController)
    {
        if (!cardSelectDisplay.activeSelf)
        {
            selectedCard = cardController;
        }
    }

    private void EnterDetection() //Run every tick to establish which target is currently hovered over.
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (TargetController target in targetArray)
        {
            if (target.BoxCollider.bounds.Contains(mousePosition))
            {
                hoverTarget = target;
                Debug.Log("Slot Assigned");
            }
            else if(target == hoverTarget)
            {
                hoverTarget = null;
            }
        }
    }
}
