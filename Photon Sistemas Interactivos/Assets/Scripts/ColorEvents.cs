using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class ColorEvents : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private const byte ColorEventCode = 1;
    
    public static ColorEvents Instance;

    public void Awake()
    {
        Instance = this;
    }
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void SetCustomColor(string myColor, int _ID)
    {
        object[] _data = new object[2];
        _data[0] = myColor;
        _data[1] = _ID;
        RaiseEventOptions eventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };

        PhotonNetwork.RaiseEvent(ColorEventCode, _data, eventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == ColorEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            ReceiveCustomColor(data);
        }
    }

    public void ReceiveCustomColor(object[] dataReceive)
    {
        Color _color = ToColor((string)dataReceive[0]);
        PhotonView[] listPhotonViews = FindObjectsOfType<PhotonView>();
        int objectID = (int)dataReceive[1];
        foreach (var item in listPhotonViews)
        {
            if(item.ViewID == objectID)
            {
               item.GetComponent<SpriteRenderer>().color = _color;
            }
        }
    }

    public Color ToColor(string color)
    {
        return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
    }
}
