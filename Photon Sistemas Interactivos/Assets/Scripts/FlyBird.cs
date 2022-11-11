using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FlyBird : MonoBehaviourPunCallbacks
{
    GameManager gameManager;
    public float velocity = 1;
    private Rigidbody2D rb;

    public Photon.Pun.PhotonTransformView MyProperty { get; set; }
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        ColorEvents.Instance.SetCustomColor(PhotonNetwork.LocalPlayer.CustomProperties["color"].ToString(), photonView.ViewID);

        //if (photonView.IsMine)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().color = PhotonNetwork.LocalPlayer.CustomProperties[]
        //}
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("ADFADSFASFASDFASDFASDFSADGSAGASDGASDFASDFADSASDFADSFSADFASDFASDFASDFFFA");
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector3.up * velocity;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector3.up * velocity;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        gameManager.GameOver();
    }

}
