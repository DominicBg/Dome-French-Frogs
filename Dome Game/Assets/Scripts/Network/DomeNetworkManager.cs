using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class DomeNetworkManager : NetworkManager {



	// Use this for initialization
	void Start () {

        if (Application.isEditor)
            StartServer();

        else
            StartClient();


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

    // Server
   public override void OnClientConnect(NetworkConnection conn) {       
 
         // Create message to set the player
         IntegerMessage msg = new IntegerMessage(0);      
  
         // Call Add player and pass the message
         ClientScene.AddPlayer(conn,0, msg);
     }
  
     
    // Server
     public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader ) { 
         
        // Read client message and receive index
         if (extraMessageReader != null) {
            // var stream = extraMessageReader.ReadMessage<IntegerMessage> ();
            // curPlayer = stream.value;
         }
        
         //Select the prefab from the spawnable objects list
         var playerPrefab = spawnPrefabs[0];

        // Create player object with prefab
        var player = PlayerController.Instance.InstantiatePlayer(playerPrefab);      
         
         // Add player object for connection
         NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        Debug.Log("Player Added to server");

    }



public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
        Debug.Log("Player Removed from server");
    }
}
