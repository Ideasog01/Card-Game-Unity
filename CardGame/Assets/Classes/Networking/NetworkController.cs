using Photon.Pun;
using System;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED TO THE " + PhotonNetwork.CloudRegion + " SERVER!");
    }
}
