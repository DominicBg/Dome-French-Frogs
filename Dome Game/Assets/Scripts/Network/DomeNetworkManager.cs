using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class DomeNetworkManager : NetworkManager {



	// Use this for initialization
	void Start () {
        StartServer();
	}
	

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server started");
    }

    public override void OnStopServer()
    {
        base.OnStartServer();
        Debug.Log("Server stopped");
    }



public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
        Debug.Log("Player Removed from server");
    }
}
