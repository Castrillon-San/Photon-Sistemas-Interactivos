using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSystem : MonoBehaviourPun
{


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && Input.GetKeyUp(KeyCode.Return))
        {
            photonView.RPC("SetMine", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void SetMine()
    {
        Debug.Log("Colocando una mina");
        //GameObject.Instantiate
    }
}
