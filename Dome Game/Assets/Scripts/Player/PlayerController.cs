using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : Singleton<PlayerController>
{
    public GameObject PlayerPrefab;
    public List<Player> PlayerList { private set; get; }

    public int PlayerCount
    {
        get
        {
            return PlayerList != null ? PlayerList.Count : 0;
        }
    }

    public Transform spawnZone;


    public GameObject InstantiatePlayer()
    {
        return InstantiatePlayer( PlayerCount + 1);
    }

    public GameObject InstantiatePlayer(NetworkConnection conn)
    {
        GameObject Player = InstantiatePlayer(conn.connectionId);
        return Player;
    }

    public GameObject InstantiatePlayer(int id)
    {
        if (PlayerList == null)
            PlayerList = new List<Player>();

        GameObject PlayerGameObject = Instantiate(PlayerPrefab, spawnZone.transform.position, Quaternion.identity) as GameObject;
        Player newPlayer = PlayerGameObject.GetComponent<Player>();
        newPlayer.ID = id; 
        PlayerList.Add(newPlayer);
        return PlayerGameObject;
    }



   





}
