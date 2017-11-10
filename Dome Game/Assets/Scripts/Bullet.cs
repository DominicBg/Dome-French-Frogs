using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float speed;
	Vector2 direction;

	public void Init(float speed, Vector2 direction)
	{
		this.speed = speed;
		this.direction = direction;

		transform.RotateWithDirection(direction,Mathf.Infinity);

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.MoveSphere(direction,speed*Time.deltaTime,Dome.radiusClose);
	}
}
