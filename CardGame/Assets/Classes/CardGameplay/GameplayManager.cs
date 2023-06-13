using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static int playerIndex;

    public static Action onDrawCard;

    public static GameDisplay gameDisplay;
    public static CardEffectManager cardEffectManager;
    public static CardDisplayManager cardDisplayManager;

    public static List<Target> potentialTargets = new List<Target>();

    public static PlayerEntityController activePlayer;
    public static PlayerController humanPlayer;
    public static EnemyController enemyPlayer;

    public static List<TargetController> targetControllerList = new List<TargetController>();

    public static List<PlayerEntityController> playerList = new List<PlayerEntityController>();
    public static List<SlotController> slotList = new List<SlotController>();
    public static List<CreatureController> creatureList = new List<CreatureController>();
    public static List<StructureController> structureList = new List<StructureController>();
    public static List<WeaponController> weaponList = new List<WeaponController>();
    public static List<EnchantmentController> enchantmentList = new List<EnchantmentController>();

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
            activePlayer = humanPlayer;
        }
        else if(playerIndex == 1)
        {
            GameUtilities.DrawCard(enemyPlayer);
            enemyPlayer.PlayRandomCard();
            activePlayer = enemyPlayer;
        }

        foreach (CreatureController creature in creatureList)
        {
            if (creature.AssignedPlayer == activePlayer)
            {
                if (creature.CreatureCard != null)
                {
                    creature.TurnEffect(CardEffect.ActivationType.OnStartTurn);
                }
            }
        }

        if (playerIndex == 1) //If it is the enemy's turn, immediately end it after actions have taken place (for testing)
        {
            OnEndPlayerTurn();
        }
    }

    public void OnEndPlayerTurn()
    {
        //On End Turn Card Effects

        foreach (CreatureController creature in creatureList)
        {
            if (creature.AssignedPlayer == activePlayer)
            {
                if (creature.CreatureCard != null)
                {
                    creature.TurnEffect(CardEffect.ActivationType.OnEndTurn);
                }
            }
        }

        GameUtilities.ResetMana(activePlayer);

        OnNewPlayerTurn();
    }
}
