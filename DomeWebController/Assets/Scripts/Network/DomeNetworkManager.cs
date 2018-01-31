using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class DomeNetworkManager : NetworkManager
{
    private static NetworkObjectManager NetworkClient;

    public static UnityEvent OnClientConnected = new UnityEvent(),
        OnClientDisconnected = new UnityEvent(),
        OnClientConnectionFailed = new UnityEvent();

   

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


    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected.Invoke();
        Debug.Log("Connected");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        StartCoroutine(TryClientReconnect());
        Debug.Log("Disconnected");
    }

    public void ConnectionError(NetworkMessage netMsg)
    {
        print("connection error");
        print(netMsg);
    }

    


    IEnumerator TryClientReconnect()
    {
        StartClient();

        yield return new WaitForSeconds(0.5f);

        if (GetNetworkClient() == null)
            OnClientDisconnected.Invoke();

        yield break;
    }

    public static IEnumerator TryConnect(Action<bool> IsAbleToConnect)
    {
        DomeNetworkManager.singleton.StartClient();
        yield return new WaitForSeconds(DomeNetworkManager.singleton.connectionConfig.ConnectTimeout / 1000);

        NetworkObjectManager[] NetworkObjectsArray = GameObject.FindObjectsOfType<NetworkObjectManager>() as NetworkObjectManager[];


        if (NetworkObjectsArray.Length <= 0)
        {
            IsAbleToConnect(false);
            DomeNetworkManager.singleton.StopClient();
        }
        else
        {
            IsAbleToConnect(true);
        }

        yield break;
    }
}