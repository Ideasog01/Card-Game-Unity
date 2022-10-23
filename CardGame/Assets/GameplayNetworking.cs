using Photon.Pun;
using System;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;

public class GameplayNetworking : MonoBehaviour
{
    public List<PlayerController> activePlayerList = new List<PlayerController>();

    [SerializeField]
    private GameObject playerPrefab;

    public void InstantiatePlayer()
    {
        GameObject obj = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        activePlayerList.Add(obj.GetComponent<PlayerController>());
        Debug.Log(obj.name + " OBJECT CREATED");
    }
}
