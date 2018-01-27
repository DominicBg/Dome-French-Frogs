using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (PlayerList == null)
            PlayerList = new List<Player>();

        GameObject PlayerGameObject = Instantiate(prefab, spawnZone.transform.position, Quaternion.identity) as GameObject;
        Player newPlayer = PlayerGameObject.GetComponent<Player>();
        PlayerList.Add(newPlayer);
        return PlayerGameObject;
    }

    private static void AddPlayer(Player newPlayer)
    {

    }




}
