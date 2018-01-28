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
        return InstantiatePlayer( PlayerCount + 1, false);
    }

    public GameObject InstantiatePlayer(NetworkInstanceId netId)
    {
        return InstantiatePlayer((int)netId.Value, true);
    }

    public GameObject InstantiatePlayer(int id, bool isNetworkInput)
    {
        if (PlayerList == null)
            PlayerList = new List<Player>();

        GameObject PlayerGameObject = Instantiate(PlayerPrefab, spawnZone.transform.position, Quaternion.identity) as GameObject;
        Player newPlayer = PlayerGameObject.GetComponent<Player>();

        PlayerInput p;
        if (isNetworkInput)
            p = new PlayerNetworkInput(newPlayer);
        else
            p = new PlayerGameControllerInput(newPlayer);


        newPlayer.Spawn(id,p);
        PlayerList.Add(newPlayer);
        return PlayerGameObject;
    }



   





}
