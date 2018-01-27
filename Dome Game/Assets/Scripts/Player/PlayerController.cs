using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : Singleton<PlayerController>
{

    public List<Player> PlayerList { private set; get; }

    public int PlayerCount
    {
        get
        {
            return PlayerList != null ? PlayerList.Count : 0;
        }
    }

    public Transform spawnZone;


    public GameObject InstantiatePlayer(GameObject prefab)
    {
        return InstantiatePlayer(prefab, PlayerCount + 1);
    }

    public GameObject InstantiatePlayer(GameObject prefab, NetworkConnection conn)
    {
        return InstantiatePlayer(prefab, conn.connectionId);
    }

    public GameObject InstantiatePlayer(GameObject prefab, int id)
    {
        if (PlayerList == null)
            PlayerList = new List<Player>();

        GameObject PlayerGameObject = Instantiate(prefab, spawnZone.transform.position, Quaternion.identity) as GameObject;
        Player newPlayer = PlayerGameObject.GetComponent<Player>();
        newPlayer.ID = id; 
        PlayerList.Add(newPlayer);
        return PlayerGameObject;
    }



   





}
