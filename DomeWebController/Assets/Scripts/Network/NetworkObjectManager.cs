using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObjectManager : NetworkBehaviour
{

    [SyncVar]
    public uint ID;

    [SyncVar]
    public string CustomizationString;

    public GameObject Player = null;

    public void Start()
    {
        ID = netId.Value;
    }

    public void FixedUpdate()
    {
        if(isLocalPlayer)
            CmdSendVectorData(netId.Value * new Vector3(0.5f, 0.3f, 0));
    }

 
    [Command]
    public void CmdSendVectorData(Vector3 _data)
    {

    }

    [Command]
    public void CmdSendStringrData(string _data)
    {

    }


}
