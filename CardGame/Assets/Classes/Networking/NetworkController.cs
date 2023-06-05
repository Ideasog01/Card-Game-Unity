using Photon.Pun;
using System;
using Photon.Realtime;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED TO THE " + PhotonNetwork.CloudRegion + " SERVER!");
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinRoom() //Via Inspector
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Room Connection Failed");
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined the Room!");
    }
}
