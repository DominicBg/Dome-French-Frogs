using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlayerController : Singleton<PlayerController>
{

    [Header("Spawn Parameters")]
	[SerializeField] private bool PlugAndPlayMode = true;
	[SerializeField] private int PlugAndPlayPlayerCount = 2;
	[SerializeField] private float delayRespawn;

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private Transform spawnZone;

    [Header("Player Parameters")]

    [SerializeField] private int NbMaxPlayers = 32;

    [SerializeField] private List<Player> PlayerList;

    public static List<Player> GetPlayerListByScore
    {
        get
        {
            List<Player> SortedList = GetPlayerList;
            SortedList.Sort();
            return SortedList;
        }
    }

    public static List<Player> GetPlayerList
    {
        get { return Instance.PlayerList; }
    }

    public int PlayerCount
    {
        get
        {
            return PlayerList != null ? PlayerList.Count : 0;
        }
    }

    public static UnityPlayerEvent OnInstantiatePlayer = new UnityPlayerEvent(), 
        OnRemovePlayer = new UnityPlayerEvent();

	void Start()
	{
		if(PlugAndPlayMode)
		{	
			for (int i = 0; i < PlugAndPlayPlayerCount; i++) 
			{
				InstantiatePlayer();
			}
		}
	}

	[ContextMenu("Test spawn")]
    public void TestSpawn()
    {
        InstantiatePlayer();
    }

    public GameObject InstantiatePlayer()
    {
        return InstantiatePlayer(PlayerCount + 1, EInputType.GAMECONTROLLER);
    }

    public GameObject InstantiatePlayer(NetworkInstanceId netId)
    {
        return InstantiatePlayer((int)netId.Value, EInputType.NETWORK);
    }

	//A refactor, pour le demo de Guigui (pour le respawn)
	public GameObject InstantiatePlayer(int id, PlayerInput inputType)
    {
        if (PlayerList == null)
            PlayerList = new List<Player>();

        GameObject PlayerGameObject = Instantiate(PlayerPrefab, spawnZone.transform.position, Quaternion.identity) as GameObject;
		PlayerGameObject.transform.SetRandomSpherePosition(EDomeLayer.LAYER0_CLOSE);

        Player newPlayer = PlayerGameObject.GetComponent<Player>();


		newPlayer.Init(id, inputType, "Player " + PlayerCount.ToString());

        PlayerList.Add(newPlayer);

        OnInstantiatePlayer.Invoke(newPlayer);
        return PlayerGameObject;
    }

	public GameObject InstantiatePlayer(int id, EInputType inputType)
	{
		if (PlayerList == null)
			PlayerList = new List<Player>();
		
		GameObject PlayerGameObject = Instantiate(PlayerPrefab, spawnZone.transform.position, Quaternion.identity) as GameObject;
		PlayerGameObject.transform.SetRandomSpherePosition(EDomeLayer.LAYER0_CLOSE);
		
		Player newPlayer = PlayerGameObject.GetComponent<Player>();
		
		PlayerInput pInput = PlayerInputFactory.GetInput(inputType, newPlayer);
		
		newPlayer.Init(id, pInput, "TestMonsieur" + PlayerCount.ToString());
		
		PlayerList.Add(newPlayer);
		
		OnInstantiatePlayer.Invoke(newPlayer);
		return PlayerGameObject;
	}

    public static void RemovePlayer(Player removedPlayer)
    {
        GetPlayerList.Remove(removedPlayer);
        OnRemovePlayer.Invoke(removedPlayer);
    }

	public void RespawnPlayer(int id, PlayerInput inputType)
	{
		StartCoroutine(RespawnCoroutine(id, inputType));
	}

	IEnumerator RespawnCoroutine(int id, PlayerInput inputType)
	{
		yield return new WaitForSeconds(delayRespawn);
		InstantiatePlayer(id, inputType);
	}
}
