using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab, playerOneCanva, playerTwoCanva;
    [SerializeField] Transform playerOnePosition, playerTwoPosition;
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

            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0, initData).gameObject.GetComponent<SpriteRenderer>().color = Color.green;

            PhotonNetwork.Instantiate(spawnCanvaPoint.name, spawnCanvaPoint.transform.position, Quaternion.identity, 0, initData);
        }
    }

}
