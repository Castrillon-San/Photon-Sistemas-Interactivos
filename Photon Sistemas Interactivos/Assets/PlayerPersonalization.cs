using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPersonalization : MonoBehaviour
{
    [SerializeField] List<Color> colors = new List<Color>();
    

    public void SelectColor(int index)
    {
        Color color = colors[index];
        Debug.Log(color.ToString());

        //var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "color", colors[index] } };
        //PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
    }
}
