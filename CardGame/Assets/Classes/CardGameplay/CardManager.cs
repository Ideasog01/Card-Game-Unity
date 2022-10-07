using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static int playerIndex;
    public static PlayerController player;
    public List<PlayerController> activePlayers = new List<PlayerController>();

    private void Start()
    {
        foreach(PlayerController player in GameObject.FindObjectsOfType<PlayerController>())
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
            player.SortCards();
        }
    }

    public void OnNewPlayerTurn()
    {
        playerIndex++;

        if(playerIndex >= activePlayers.Count)
        {
            playerIndex = 0;
        }

        player = activePlayers[playerIndex];
    }

    public void OnEndPlayerTurn()
    {

    }
}
