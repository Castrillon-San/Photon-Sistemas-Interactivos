using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviourPun
{
    public float maxTime = 1;
    public float timer = 0;
    public GameObject pipe;
    public float height;

    void Update()
    {
        if (timer > maxTime)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                float randomNum = Random.Range(-height, height);
                photonView.RPC("SpawnPipe", RpcTarget.AllViaServer, randomNum);
                timer = 0;
            }

        }

        timer += Time.deltaTime;
    }

    [PunRPC]
    void SpawnPipe(float number)
    {
        GameObject newPipe = Instantiate(pipe);
        newPipe.transform.position = transform.position + new Vector3(0, number, 0);
        Destroy(newPipe, 15);
        timer = 0;
    }
}
