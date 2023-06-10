using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static int playerIndex;

    public static GameDisplay gameDisplay;
    public static CardEffectManager cardEffectManager;
    public static CardDisplayManager cardDisplayManager;

    public static List<Target> potentialTargets = new List<Target>();

    public static PlayerEntityController activePlayer;
    public static PlayerController humanPlayer;
    public static EnemyController enemyPlayer;

    public static List<TargetController> targetControllerList = new List<TargetController>();

    private void Awake()
    {
        foreach(TargetController target in FindObjectsOfType<TargetController>())
        {
            targetControllerList.Add(target);
        }

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
            activePlayer = humanPlayer;
        }
        else if(playerIndex == 1)
        {
            GameUtilities.DrawCard(enemyPlayer);
            enemyPlayer.PlayRandomCard();
            GameUtilities.ResetMana(enemyPlayer);
            activePlayer = enemyPlayer;
        }

        foreach (TargetController target in targetControllerList)
        {
            if (target.CreatureControlllerRef != null && target.CreatureControlllerRef.CreatureCard != null)
            {
                cardEffectManager.OnStartTurnEffect(target.CreatureControlllerRef.CreatureCard);
            }

            if (target.StructureControllerRef != null && target.StructureControllerRef.StructureCard != null)
            {
                cardEffectManager.OnStartTurnEffect(target.StructureControllerRef.StructureCard);
            }
        }

        if (playerIndex == 1)
        {
            OnEndPlayerTurn();
        }
    }

    public void OnEndPlayerTurn()
    {
        //On End Turn Card Effects
        
        foreach(TargetController target in targetControllerList)
        {
            if(target.CreatureControlllerRef != null && target.CreatureControlllerRef.CreatureCard != null)
            {
                cardEffectManager.OnEndTurnEffect(target.CreatureControlllerRef.CreatureCard);
            }

            if(target.StructureControllerRef != null && target.StructureControllerRef.StructureCard != null)
            {
                cardEffectManager.OnEndTurnEffect(target.StructureControllerRef.StructureCard);
            }
        }
        
        OnNewPlayerTurn();
    }
}
