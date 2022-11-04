using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public enum RegionCode
{
    AUTO,
    CAE,
    EU,
    US,
    USW,
    SA
}
public class ConnectCrtl : MonoBehaviourPunCallbacks
{

    [SerializeField]
    string gameVersion = "1";
    [SerializeField]
    string regionCode = null;
    [SerializeField]
    private GameObject PanelConnect;
    [SerializeField]
    private GameObject PanelRoom;
    [SerializeField]
    private Button button;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = RegionCode.AUTO.ToString();
    }

    public void SetRegion(int index)
    {
        RegionCode region = (RegionCode)index;

        if(region == RegionCode.AUTO)
        {
            regionCode = null;
        }
        else
        {
            regionCode = region.ToString();
        }

        Debug.Log("Region seleccionada: " + regionCode);
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = regionCode;

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
        //GameObject.Find("Button").GetComponentInChildren<TMPro.TMP_Text>().text = msg;
        //GameObject.Find("Button").GetComponent<Button>().enabled = state;
        button.GetComponentInChildren<TMPro.TMP_Text>().text = msg;
        button.GetComponent<Button>().enabled = state;
    }

    void ShowRoomPanel()
    {
        PanelConnect.SetActive(false);
        PanelRoom.SetActive(true);
    }

    public void SetColor(int index)
    {
        string color = GameObject.Find("DropdownColors").GetComponent<Dropdown>().options[index].text;

        Debug.Log("Color: " + color);

        var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "color", color } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);

    }

    public void SetReady()
    {

        var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "ready", true } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
    }

    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        SetButton(true, "Go to room");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        //SetButton(false, "WAITING PLAYERS");
        PhotonNetwork.LoadLevel("Lobby");
        //if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        //{
        //    Debug.Log("Room is Ready");   
        //    //ShowRoomPanel();
        //}
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        Debug.Log(newPlayer.NickName + " Se Ha unido al cuarto, Players: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.LoadLevel("FlappyLevel");
            //ShowRoomPanel();

        }

    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    //{
    //    if (changedProps.ContainsKey("color"))
    //    {

    //    }
    //    if (changedProps.ContainsKey("ready"))
    //    {
    //        int playersReady = 0;
    //        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values) 
    //        {
    //            bool ready = (bool)player.CustomProperties["ready"];
    //            Debug.Log(player.NickName + " is ready? ...." + ready);

    //            if (ready)
    //            {
    //                playersReady++;
    //            }

    //            if(playersReady == PhotonNetwork.CurrentRoom.MaxPlayers)
    //            {
    //                PhotonNetwork.LoadLevel("Game");
    //            }
    //        }
    //    }
    //}


    #endregion

}
