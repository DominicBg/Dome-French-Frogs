using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailUpgrade : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			col.GetComponent<PlayerSnake>().AddTailPart();
			Destroy(gameObject);
		}
	}
}
