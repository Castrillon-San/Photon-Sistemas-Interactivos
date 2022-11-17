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

    [SerializeField]
    TMP_Text roomIsOpen;

    private void Start()
    {

        localUsername.text = PhotonNetwork.LocalPlayer.NickName;


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
        if (PhotonNetwork.CurrentRoom.Players[1] != PhotonNetwork.LocalPlayer)
        {
            rivalUsername.gameObject.SetActive(true);
            rivalUsername.text = PhotonNetwork.CurrentRoom.Players[1].NickName;
        }
        localUsername.text = PhotonNetwork.LocalPlayer.NickName;
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        SetButton(false, "WAITING PLAYERS");
        Debug.Log("PlayerCount:" + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            roomIsOpen.gameObject.SetActive(true);
            roomIsOpen.text="Room is Ready";
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

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            roomIsOpen.gameObject.SetActive(true);
            roomIsOpen.text = "Room is Ready";
        }
    }

    public void LoadGameLevel()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("FlappyLevel");
    }
}
