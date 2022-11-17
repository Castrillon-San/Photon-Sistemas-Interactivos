using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ControllerLobby : MonoBehaviourPunCallbacks
{

    [SerializeField]
    string gameVersion = "1";

    [SerializeField]
    private Button button;

    [SerializeField]
    TMP_Text localUsername;

    [SerializeField]
    TMP_Text rivalUsername;

    private void Start()
    {
        SetButton(true, "Ready!");

    }
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    void SetButton(bool state, string msg)
    {
        button.GetComponentInChildren<TMPro.TMP_Text>().text = msg;
        button.GetComponent<Button>().enabled = state;
    }

    public override void OnJoinedRoom()
    {

        localUsername.text = PhotonNetwork.LocalPlayer.NickName;
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        SetButton(false, "WAITING PLAYERS");
        //PhotonNetwork.LoadLevel("Lobby");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("Room is Ready");
            //SetButton(true, "Ready!");
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(newPlayer!= PhotonNetwork.LocalPlayer)
        {
            rivalUsername.gameObject.SetActive(true);
            rivalUsername.text = newPlayer.NickName;
        }
      
        Debug.Log(newPlayer.NickName + " Se Ha unido al cuarto, Players: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            SetButton(true, "Ready!");
            //ShowRoomPanel();

        }

    }

    public void LoadGameLevel()
    {
        PhotonNetwork.LoadLevel("FlappyLevel");
    }
}
