using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab, playerOneCanva, playerTwoCanva;
    [SerializeField] Transform playerOnePosition, playerTwoPosition;

    public static GameObject canvaClone;
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Falta Prefab");
        }
        else
        {
            Transform spawnPoint = (PhotonNetwork.IsMasterClient) ? playerOnePosition : playerTwoPosition;

            GameObject spawnCanvaPoint = (PhotonNetwork.IsMasterClient) ? playerOneCanva : playerTwoCanva;

            object[] initData = new object[1];

            initData[0] = "Data instanciacion";

            GameObject _clone = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0, initData);
            _clone.GetComponent<SpriteRenderer>().color = ToColor(PhotonNetwork.LocalPlayer.CustomProperties["color"].ToString());

            canvaClone = PhotonNetwork.Instantiate(spawnCanvaPoint.name, spawnCanvaPoint.transform.position, Quaternion.identity, 0, initData);

        }
    }

    public Color ToColor(string color)
    {
        return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
    }

}
