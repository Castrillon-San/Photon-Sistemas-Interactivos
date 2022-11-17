using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject gameOverCanvas;
    public TMP_Text winner;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
    }

    public void GameOver(GameObject player)
    {
        if (!player.GetPhotonView().IsMine)
        {
            string winner = PhotonNetwork.LocalPlayer.NickName;
            photonView.RPC("SetWinner", RpcTarget.AllViaServer, winner);
        }

        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }
    public void Replay()
    {
        Time.timeScale = 1;
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        base.OnLeftRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }


    [PunRPC]
    public void SetWinner(string text)
    {
        winner.text = text;

    }
}
