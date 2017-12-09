using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int id;
	float speed;
	Vector2 direction;
	[SerializeField] float lifeTime = 1;

	public void Init(float speed, Vector2 direction, int id)
	{
		this.speed = speed;
		this.direction = direction.normalized;
		this.id = id;

		transform.RotateWithDirection(direction,Mathf.Infinity);
		Destroy (gameObject,lifeTime);
	}

	void FixedUpdate ()
	{
		transform.MoveSphere(direction,speed*Time.deltaTime,Dome.instance.radiusClose);
	}

	void OnDestroy()
	{
		//Particle etc
	}
}
