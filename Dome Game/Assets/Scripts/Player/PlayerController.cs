using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public List<Player> playersList = new List<Player>();
    public Player playerRef;

    public Transform spawnZone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MakePlayerSpawn()
	{
        foreach(Player pl in playersList)
        {
            if (pl.isCurrentPlayer)
                pl.isCurrentPlayer = false;
        }

        Player p = Instantiate(playerRef);
        p.transform.SetParent(spawnZone,false);
        p.isCurrentPlayer = true;
        playersList.Add(p);

        Debug.Log(p.isCurrentPlayer);
	}
}
