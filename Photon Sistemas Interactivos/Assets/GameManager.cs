using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
    }

    public void GameOver()
    {
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
        PhotonNetwork.AutomaticallySyncScene=false;
        base.OnLeftRoom();
        PhotonNetwork.LeaveRoom();
    }
}
