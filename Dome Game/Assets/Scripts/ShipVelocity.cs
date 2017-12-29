using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVelocity : Ship {

	Vector2 velocity;
	public float friction = 5;
	public float acceleration = 10f;
	public float maxVel = 10;


	override protected void Move(Vector2 dir)
	{
		SetVelocity(dir);

		transform.MoveSphere(dir+velocity,speed * Time.deltaTime, Dome.instance.radiusClose);
	}
	override protected void MoveSteer(Vector2 dir)
	{

		//X = rotation
		currentDirection = GameMath.RotateVector(-dir.x  * Time.deltaTime * rotationSpeed * 10,currentDirection);
		currentDirection.Normalize();
		SetVelocity(currentDirection);

		//ship.RotateWithDirection(currentDirection,25);

		//transform.MoveSphere(currentDirection, speed * Time.deltaTime  * isA dir.y, Dome.instance.radiusClose);
		transform.MoveSphere(velocity, speed * Time.deltaTime, Dome.instance.radiusClose);		
	}

	void SetVelocity(Vector2 dir)
	{		
		//Y = speed
		if(dir.magnitude == 0 || Input.GetButton("Speed"+id))
		{	
			velocity -= velocity*friction*Time.deltaTime;
		}
		else
		{
			if (dir.magnitude > 1)
				dir.Normalize ();
			
			velocity -= velocity*friction*Time.deltaTime;
			
			velocity += dir * acceleration * Time.deltaTime;
			if(velocity.magnitude > maxVel)
				velocity = velocity.normalized * maxVel;
			
			ship.RotateWithDirection(dir,rotationSpeed);
			
			currentDirection = dir;
		}
	}
}
