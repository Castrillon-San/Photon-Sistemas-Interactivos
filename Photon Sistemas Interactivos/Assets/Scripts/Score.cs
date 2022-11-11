using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviourPun, IPunObservable
{
    public int score = 0;
    private Text _text;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(score);
        }
        else if(stream.IsReading)
        {
            score = (int)stream.ReceiveNext();
        }
    }

    void Awake()
    {
        score = 0;
        _text = GetComponentInChildren<Text>();
    }

    void Update()
    {
       _text.text = score.ToString();
    }
}
