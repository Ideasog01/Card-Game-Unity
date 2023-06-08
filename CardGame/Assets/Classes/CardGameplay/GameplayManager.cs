using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static int playerIndex;

    public static GameDisplay gameDisplay;
    public static CardEffectManager cardEffectManager;
    public static CardDisplayManager cardDisplayManager;

    public static PlayerController humanPlayer;
    public static EnemyController enemyPlayer;

    public static List<CreatureController> creatureControllerList = new List<CreatureController>();
    public static List<StructureController> structureControllerList = new List<StructureController>();

    private void Awake()
    {
        gameDisplay = this.GetComponent<GameDisplay>();
        cardEffectManager = this.GetComponent<CardEffectManager>();
        cardDisplayManager = this.GetComponent<CardDisplayManager>();

        humanPlayer = FindObjectOfType<PlayerController>();
        enemyPlayer = FindObjectOfType<EnemyController>();
    }

    private void Start()
    {
        OnGameBegin();
    }

    public void OnGameBegin()
    {
        GameUtilities.ShuffleHand(humanPlayer);
        GameUtilities.ShuffleHand(enemyPlayer);

        OnNewPlayerTurn();
    }

    public void OnNewPlayerTurn()
    {
        playerIndex++;

        if (playerIndex >= 2)
        {
            playerIndex = 0;
        }

        if (playerIndex == 0)
        {
            GameUtilities.DrawCard(humanPlayer);
            cardDisplayManager.DisplayCardData();
            cardDisplayManager.DisplayMana();
            GameUtilities.ResetMana(humanPlayer);
            cardDisplayManager.DisplayMana();
        }
        else if(playerIndex == 1)
        {
            GameUtilities.DrawCard(enemyPlayer);
            enemyPlayer.PlayRandomCard();
            GameUtilities.ResetMana(enemyPlayer);
        }

        //On Start & End Turn Card Effects

        foreach (CreatureController creature in creatureControllerList)
        {
            if (creature.AssignedSlot.AssignedPlayer == humanPlayer && playerIndex == 0 || creature.AssignedSlot.AssignedPlayer == enemyPlayer && playerIndex == 1)
            {
                cardEffectManager.CardEffect(creature.AssignedSlot.AssignedPlayer, creature.CreatureCard);
            }
        }

        foreach (StructureController structure in structureControllerList)
        {
            if (structure.AssignedSlot.AssignedPlayer == humanPlayer && playerIndex == 0 || structure.AssignedSlot.AssignedPlayer == enemyPlayer && playerIndex == 1)
            {
                cardEffectManager.CardEffect(structure.AssignedSlot.AssignedPlayer, structure.StructureCard);
            }
        }

        if(playerIndex == 1)
        {
            OnEndPlayerTurn();
        }
    }

    public void OnEndPlayerTurn()
    {
        //On End Turn Card Effects

        foreach (CreatureController creature in creatureControllerList)
        {
            if (creature.AssignedSlot.AssignedPlayer == humanPlayer && playerIndex == 0 || creature.AssignedSlot.AssignedPlayer == enemyPlayer && playerIndex == 1)
            {
                cardEffectManager.CardEffect(creature.AssignedSlot.AssignedPlayer, creature.CreatureCard);
            }
        }

        foreach (StructureController structure in structureControllerList)
        {
            if (structure.AssignedSlot.AssignedPlayer == humanPlayer && playerIndex == 0 || structure.AssignedSlot.AssignedPlayer == enemyPlayer && playerIndex == 1)
            {
                cardEffectManager.CardEffect(structure.AssignedSlot.AssignedPlayer, structure.StructureCard);
            }
        }

        OnNewPlayerTurn();
    }
}
