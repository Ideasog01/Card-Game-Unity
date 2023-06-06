using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static int playerIndex;

    public static GameDisplay gameDisplay;
    public static SpellManager spellManager;
    public static CardDisplayManager cardDisplayManager;

    public static PlayerController humanPlayer;
    public static EnemyController enemyPlayer;

    public List<CreatureController> creatureControllerList = new List<CreatureController>();

    private void Awake()
    {
        gameDisplay = this.GetComponent<GameDisplay>();
        spellManager = this.GetComponent<SpellManager>();

        humanPlayer = FindObjectOfType<PlayerController>();
        enemyPlayer = FindObjectOfType<EnemyController>();

        cardDisplayManager = GameObject.Find("PlayerController").GetComponent<CardDisplayManager>();
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

        if(playerIndex == 0)
        {
            GameUtilities.DrawCard(humanPlayer);
            cardDisplayManager.DisplayCardData();
            cardDisplayManager.DisplayMana();
        }
        else if(playerIndex == 1)
        {
            GameUtilities.DrawCard(enemyPlayer);
            enemyPlayer.PlayRandomCard();
            OnNewPlayerTurn();
        }
    }

    public void OnEndPlayerTurn() //Via Inspector
    {
        OnNewPlayerTurn();
    }
}
