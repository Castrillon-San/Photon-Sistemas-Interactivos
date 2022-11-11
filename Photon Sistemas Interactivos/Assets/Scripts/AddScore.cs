using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviourPun
{
    private bool entered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Entrance" && photonView.IsMine)
        {
            if (collision.isTrigger && !entered)
            {
                GameController.canvaClone.GetComponent<Score>().score++;
                Debug.Log(GameController.canvaClone.GetComponent<Score>().score);
                entered = true;
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        entered = false;
    }

}
