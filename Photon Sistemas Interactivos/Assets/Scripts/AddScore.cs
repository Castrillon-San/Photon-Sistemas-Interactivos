using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Entrance" && photonView.IsMine)
        {
            GameController.canvaClone.GetComponentInChildren<Score>().score++;
        }
    }

}
