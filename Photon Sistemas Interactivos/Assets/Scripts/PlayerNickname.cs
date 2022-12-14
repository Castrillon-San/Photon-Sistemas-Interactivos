using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNickname : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";

    void Start()
    {
        string defaultName = string.Empty;
        TMPro.TMP_InputField _inputField = GetComponent<TMPro.TMP_InputField>();

        if(_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

}
