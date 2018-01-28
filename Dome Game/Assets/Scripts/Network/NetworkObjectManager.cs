using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
        // Create player object with prefab
        Player = PlayerController.Instance.InstantiatePlayer((int)ID);
        Player.transform.SetParent(gameObject.transform);

    }

    public void FixedUpdate()
    {

    }


    [Command]
    public void CmdSendVectorData(Vector3 _data)
    {
        Debug.Log(_data + "," + connectionToClient.connectionId);
        Player.GetComponent<Player>().MoveSteer(_data);
    }

    [Command]
    public void CmdSendStringrData(string _data)
    {

    }




}
