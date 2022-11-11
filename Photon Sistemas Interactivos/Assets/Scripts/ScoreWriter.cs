using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreWriter : MonoBehaviourPun, IPunObservable
{
    [NonSerialized] public bool _isWritingScore;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_isWritingScore);
        }
        else
        {
            _isWritingScore = (bool)stream.ReceiveNext();
        }
    }

    void Start()
    {
        if (photonView.IsMine) _isWritingScore = true;
    }

}
