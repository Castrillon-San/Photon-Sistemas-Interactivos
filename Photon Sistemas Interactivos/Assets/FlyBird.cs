using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlyBird : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    GameManager gameManager;
    public float velocity = 1;
    private Rigidbody2D rb;

    public Photon.Pun.PhotonTransformView MyProperty { get; set; }
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log(info.Sender.NickName + "Se ha iniciado");
        Debug.Log((string)info.photonView.InstantiationData[0]);
    }
}
