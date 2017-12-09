using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

	public List<Player> playersList = new List<Player>();
    public Player playerRef;

    public Transform spawnZone;
	private int countPlayer = 0;

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

		p.id = countPlayer;
		countPlayer++;
	}
}
