using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVelocity : Ship {

	Vector2 velocity;
	public float friction = 5;
	public float acceleration = 10f;
	public float maxVel = 10;


	override protected void Move()
	{
		Vector2 dir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		if(dir.magnitude == 0)
		{	
			velocity -= velocity*friction*Time.deltaTime;
		}
		else
		{
			velocity -= velocity*friction*Time.deltaTime;

			velocity += dir * acceleration * Time.deltaTime;
			if(velocity.magnitude > maxVel)
				velocity = velocity.normalized * maxVel;

			ship.RotateWithDirection(dir,rotationSpeed,0);

			direction = dir;
		}

		transform.MoveSphere(dir+velocity,speed * Time.deltaTime, Dome.radiusClose);
	}
}
