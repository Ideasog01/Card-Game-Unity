using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static int playerIndex;
    public static PlayerController currentPlayer;

    public static GameDisplay gameDisplay;

    public List<PlayerController> activePlayers = new List<PlayerController>();
    public List<CreatureController> creatureControllerList = new List<CreatureController>();

    private void Awake()
    {
        gameDisplay = this.GetComponent<GameDisplay>();
    }

    private void Start()
    {
        foreach (PlayerController player in GameObject.FindObjectsOfType<PlayerController>())
        {
            activePlayers.Add(player);
        }

        OnGameBegin();
    }

    public void OnGameBegin()
    {
        foreach (PlayerController player in GameObject.FindObjectsOfType<PlayerController>())
        {
            activePlayers.Add(player);
            player.ShuffleHand();
        }

        OnNewPlayerTurn();
    }

    public void OnNewPlayerTurn()
    {
        playerIndex++;

        if (playerIndex >= activePlayers.Count)
        {
            playerIndex = 0;
        }

        currentPlayer = activePlayers[playerIndex];
        currentPlayer.DrawCard();
    }

    public void OnEndPlayerTurn()
    {

        OnNewPlayerTurn();
    }
}
