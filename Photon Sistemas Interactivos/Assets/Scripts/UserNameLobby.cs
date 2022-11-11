using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserNameLobby : MonoBehaviour
{
    void Awake()
    {
        GetComponent<TMPro.TMP_Text>().text = Photon.Pun.PhotonNetwork.NickName;
    }

}
