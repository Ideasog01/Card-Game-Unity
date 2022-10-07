using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<PlayerController> activePlayers = new List<PlayerController>();

    private void Start()
    {
        foreach(PlayerController player in GameObject.FindObjectsOfType<PlayerController>())
        {
            activePlayers.Add(player);
        }
    }

    public void StartTurn()
    {

    }

    public void EndTurn()
    {

    }
}
