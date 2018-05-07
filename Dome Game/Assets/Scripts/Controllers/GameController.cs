using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField] float delaySpawnTails;
	[SerializeField] TailUpgrade tailUpgradePrefab;
	void Start()
	{
		InvokeRepeating("SpawnNewTails", delaySpawnTails, delaySpawnTails);
	}

	void SpawnNewTails()
	{
		TailUpgrade tailUpgradeInstance = Instantiate(tailUpgradePrefab, transform.position, Quaternion.identity);
		tailUpgradeInstance.transform.SetRandomSpherePosition(EDomeLayer.LAYER0_CLOSE);
	}
}
