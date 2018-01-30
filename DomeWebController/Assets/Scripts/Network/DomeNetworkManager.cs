using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class DomeNetworkManager : NetworkManager
{
    private static NetworkObjectManager NetworkClient;

    public static NetworkObjectManager GetNetworkClient()
    {
        if (NetworkClient == null)
        {
            NetworkObjectManager[] NetworkObjectsArray = GameObject.FindObjectsOfType<NetworkObjectManager>() as NetworkObjectManager[];

            for (int i = 0; i < NetworkObjectsArray.Length; i++)
            {
                if (NetworkObjectsArray[i].isLocalPlayer)
                {
                    return NetworkObjectsArray[i];
                }
            }
            Debug.LogError("Network Client not found");
            return null;
        }
        else
        {
            return NetworkClient;
        }

    }

    // Use this for initialization
    void Start()
    {
        StartClient();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Connected");
    }


}