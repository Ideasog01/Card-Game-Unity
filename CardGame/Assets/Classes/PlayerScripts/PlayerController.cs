using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerEntityController
{
    public static Target hoverTarget;

    public static Target clickedTarget;

    public static CardController selectedCard;

    public GameObject cardSelectDisplay;

    [SerializeField]
    private TargetController[] targetArray;

    private List<PlayerEntityController> playerList = new List<PlayerEntityController>();
    private List<SlotController> slotList = new List<SlotController>();
    private List<CreatureController> creatureList = new List<CreatureController>();
    private List<StructureController> structureList = new List<StructureController>();

    private void Awake()
    {
        foreach (TargetController target in targetArray)
        {
            if (target.PlayerControllerRef != null)
            {
                playerList.Add(target.PlayerControllerRef);
            }

            if (target.SlotControllerRef != null)
            {
                slotList.Add(target.SlotControllerRef);
            }

            if (target.CreatureControlllerRef != null)
            {
                creatureList.Add(target.CreatureControlllerRef);
            }

            if(target.StructureControllerRef != null)
            {
                structureList.Add(target.StructureControllerRef);
            }
        }
    }

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

    public void ClickCard()
    {
        if(selectedCard != null)
        {
            cardSelectDisplay.SetActive(true);
            selectedCard.gameObject.SetActive(false);
        }
    }

    public void ReleaseCard()
    {
        if(selectedCard != null && cardSelectDisplay.activeSelf)
        {
            //Play Card

            PlayCard(selectedCard.AssignedCard, hoverTarget);

            //Hide Display
            cardSelectDisplay.SetActive(false);
            selectedCard.gameObject.SetActive(true);
            selectedCard = null;
        }
    }

    private void EnterDetection() //Run every tick to establish which target is currently hovered over.
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (cardSelectDisplay.activeSelf)
        {
            cardSelectDisplay.transform.position = Input.mousePosition;
        }

        foreach (PlayerEntityController player in playerList)
        {
            if (player.BoxCollider.bounds.Contains(mousePosition))
            {
                hoverTarget = player;
            }
        }

        foreach(SlotController slot in slotList)
        {
            if(slot.BoxCollider.bounds.Contains(mousePosition))
            {
                hoverTarget = slot;
            }
        }

        foreach(CreatureController creature in creatureList)
        {
            if (creature.BoxCollider.bounds.Contains(mousePosition))
            {
                hoverTarget = creature;
            }
        }

        foreach (StructureController structure in structureList)
        {
            if (structure.BoxCollider.bounds.Contains(mousePosition))
            {
                hoverTarget = structure;
            }
        }

        if(hoverTarget != null)
        {
            Debug.Log("Hover Target: " + hoverTarget.gameObject.name);
        }
    }
}
