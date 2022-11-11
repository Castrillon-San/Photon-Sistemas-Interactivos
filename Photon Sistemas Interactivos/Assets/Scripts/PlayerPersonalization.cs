using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerPersonalization : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Color> colors = new List<Color>();
    public GameObject prefabCustom;

    [SerializeField] Color prueba;

    public void SelectColor(int index)
    {
        Color color = colors[index];
        //Debug.Log(color.ToString());

        var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "color", gameObject.GetComponent<TMPro.TMP_Dropdown>().options[index].text } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);

    }

    public Color ToColor(string color)
    {
        return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    //{
    //    if (changedProps.ContainsKey("color"))
    //    {
    //        prefabCustom.GetComponent<SpriteRenderer>().color = ToColor(PhotonNetwork.LocalPlayer.CustomProperties["color"].ToString());
    //    }

    //}
}
