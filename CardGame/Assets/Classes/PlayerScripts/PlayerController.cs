using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    public static SlotController selectedSlot;

    public static CardController cardSelected;

    public static EntityController selectedPlayer;

    public GameObject cardSelectDisplay;

    [SerializeField]
    private CreatureController selectedCreature;

    [SerializeField]
    private SlotController[] slotArray;

    [Header("Players")]

    [SerializeField]
    private BoxCollider2D humanPlayerBox;

    [SerializeField]
    private BoxCollider2D enemyPlayerBox;

    private void Update()
    {
        CheckSelectSlot();

        if (selectedCreature == null)
        {
            HandCardSelect();
            SelectCreature();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if(selectedSlot != null)
            {
                CreatureController creatureController = selectedSlot.AssignedCreatureController;

                if (creatureController != null)
                {
                    Card creatureCard = creatureController.CreatureCard;

                    if(creatureCard != null)
                    {
                        if (creatureController.AssignedSlot.AssignedPlayer != this) //If the creature belongs to ANOTHER player, attack this creature
                        {
                            if(GameUtilities.IsCreatureRange(creatureController, selectedCreature))
                            {
                                selectedCreature.FightCreature(creatureController);
                                selectedCreature = null;
                                Debug.Log("CREATURE FIGHT!");
                            }
                            else
                            {
                                Debug.Log("Creature is not in range!");
                            }
                        }
                        else
                        {
                            Debug.Log("CREATURE BELONGS TO PLAYER");
                        }
                    }

                    GameplayManager.gameDisplay.HideDisplayTargets();
                    selectedCreature = null;
                }
            }
        }
    }

    public void SelectCard(CardController selectedCard)
    {
        if (!cardSelectDisplay.activeSelf)
        {
            cardSelected = selectedCard;
        }
    }

    public void SelectCreature()
    {
        if(selectedSlot != null)
        {
            if(selectedSlot.AssignedCreatureController.CreatureCard != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CreatureController creature = selectedSlot.AssignedCreatureController;

                    if(selectedCreature == null) //Player has not selected creature
                    {
                        if (creature.AssignedSlot.AssignedPlayer == this) //If the creature belongs to the player
                        {
                            selectedCreature = creature;
                            GameplayManager.gameDisplay.DisplayTargets(selectedSlot);
                            Debug.Log("CREATURE SELECT!");
                        }
                        else
                        {
                            Debug.Log("CREATURE DOES NOT BELONG TO PLAYER");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("SELECTED SLOT IS NULL");
        }
    }

    private void HandCardSelect()
    {
        if (cardSelected != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cardSelectDisplay.SetActive(true);
                cardSelected.gameObject.SetActive(false);
            }

            if (cardSelectDisplay.activeSelf)
            {
                cardSelectDisplay.transform.position = Input.mousePosition;

                if (Input.GetMouseButtonUp(0))
                {
                    cardSelectDisplay.SetActive(false);
                    cardSelected.gameObject.SetActive(true);

                    if(GameplayManager.playerIndex == 0) //Is this the player's turn?
                    {
                        if(selectedSlot != null)
                        {
                            StartCoroutine(PlayCard(cardSelected.AssignedCard, selectedSlot));
                        }
                        else if(selectedPlayer != null)
                        {
                            StartCoroutine(PlayCard(cardSelected.AssignedCard, selectedPlayer));
                            Debug.Log("Player Targeted!");
                        }
                    }
                    
                    cardSelected = null;
                }
            }
        }
    }

    private void CheckSelectSlot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (SlotController slot in slotArray)
        {
            if (slot.SlotBox.bounds.Contains(mousePosition))
            {
                selectedSlot = slot;
                Debug.Log("Slot Assigned");
            }
            else if(slot == selectedSlot)
            {
                selectedSlot = null;
            }
        }


        //Check to see if the player is hovering their mouse over either themselves or the enemy player
        if(selectedSlot == null)
        {
            if(humanPlayerBox.bounds.Contains(mousePosition))
            {
                selectedPlayer = this;
            }
            else if(enemyPlayerBox.bounds.Contains(mousePosition))
            {
                selectedPlayer = GameplayManager.enemyPlayer;
            }
        }
    }
}
