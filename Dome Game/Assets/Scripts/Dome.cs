using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dome : MonoBehaviour
{
	public static Dome instance;
	void Awake()
	{
		instance = this;
	}
	public float radiusClose = 25;
	public float radiusFar = 50;
	public float offTop = 2;
	public float limitBottom = -10;
}

public static class DomeStatic {

	public enum Dimension{Close, Far};
	public static Vector3 center = Vector3.zero;

	/// <summary>
	/// Moves the transform inside a sphere.
	/// </summary>
	/// <param name="tr">Tr.</param>
	/// <param name="direction">The direction to move</param>
	/// <param name="speed">The Speed, consider multiplying your speed by Time.deltaTime</param>
	/// <param name="radius">The radius of the sphere.</param>
	public static void MoveSphereSprite(this Transform tr, Transform spriteTr, Vector2 direction, float speed, float radius)
	{
		/*
		float sphereCorrector = Mathf.Cos((tr.position.y/radius) * Mathf.PI*.5f) + 1;
		tr.position += new Vector3(0,direction.y,0) * speed / sphereCorrector;

		if(tr.position.y > radius-Dome.instance.offTop)
			tr.position = tr.position.SetY(radius-1);
		tr.RotateAround(center,Vector3.up,direction.x * speed );

		SetPositionSphere(tr,radius);
		*/
	



		//float sphericCoef = 1 + Mathf.Sin((tr.position.y/radius) * Mathf.PI * 0.5f);
		Debug.DrawRay(tr.position, center - tr.position, Color.blue);

        /*
		if(tr.position.y >= Dome.instance.limitBottom)
			tr.RotateAround(center,tr.right,direction.y * speed);

			
		//if((tr.position.y <= radius - Dome.instance.offTop && direction.y > 0) ||
		 //  (tr.position.y >= Dome.instance.limitBottom && direction.y < 1))
		//	tr.RotateAround(center,tr.right,direction.y * speed);
			
		tr.RotateAround(center,Vector3.up,direction.x * speed);
		//tr.RotateAround(center,Vector3.up,direction.x * speed );
		*/



        /*
		tr.position += (tr.right * direction.x) + (tr.up * direction.y) * speed;
		tr.SetPositionSphere (radius);
		*/

        //Vector3 sphere = GameMath.CartesianToSpherical(tr.position);
        //tr.position = GameMath.SphericalToCartesian(sphere);


        /*
        //x2 + y2 = r2

        float xzMag = GameMath.MagnitudeXZ(tr.position);
		//Ajust speed by position on the sphere
		float speedCoef = 1 + (Mathf.Epsilon + Mathf.Cos((xzMag / radius).Maximum(1) * Mathf.PI * 0.5f));
		speed = speed * speedCoef;
		float x = tr.position.x + (direction.x * speed);
		float z = tr.position.z + (-direction.y * speed);
		
		if(xzMag > radius)
		{
			//XZ normalization
			x = (x/xzMag) * radius;
			z = (z/xzMag) * radius;
			xzMag = radius;
		}
		//Mathf.Cos(t*Mathf.PI*0.5f)
		//float y = Mathf.Lerp(radius,0,t) * Mathf.Cos(t*Mathf.PI*0.5f);
		//float y = radius * Mathf.Cos(t*Mathf.PI*0.5f);

		//float y = Mathf.Cos(t*Mathf.PI*0.5f) * radius;
		float y = Mathf.Sqrt(radius*radius - xzMag*xzMag);
		//float y = Mathf.Sin(theta) * radius;

		tr.position = new Vector3(x,y,z);

		Debug.DrawRay(tr.position, tr.up * 25, Color.green);
        */

        //Spherical Coordonate
        /*
		float r, theta, phi;
		GameMath.CartesianToSpherical(tr.position,out r,out theta,out phi);
		theta += direction.y * speed;
		phi += direction.x * speed;
		tr.position = GameMath.SphericalToCartesian(r,theta,phi);
		*/


        //tr.position = GameMath.SphericalRotation(tr.position, direction.x * speed, direction.y * speed);
        //tr.LookAt(center);
        Debug.DrawRay(tr.position, spriteTr.right * 10 , Color.magenta);
        Debug.DrawRay(tr.position, spriteTr.up * 10, Color.magenta);

        tr.RotateAround(center, spriteTr.right, speed);
        //TODO 
        //limiter le y


        //tr.LookAt(center,tr.up);
    }

	public static void SetPositionSphere(this Transform tr, float radius)
	{
		if(tr.position.y < Dome.instance.limitBottom)
			tr.position = tr.position.SetY(Dome.instance.limitBottom);
		if(tr.position.y > radius-Dome.instance.offTop)
			tr.position = tr.position.SetY(radius-1);

		Vector3 diff = tr.position - center;
		float ratio = diff.magnitude / radius;
		tr.position = center + (diff / ratio);

		tr.LookAt(center, tr.up);
	}


	public static void RotateWithDirection(this Transform tr, Vector2 dir, float speed, float offSet)
	{
		//float nonZeroY = (dir.y == 0) ? Mathf.Epsilon : dir.y;
		float z = Mathf.Atan2(dir.x , dir.y) * Mathf.Rad2Deg;
		Quaternion desired = Quaternion.Euler(new Vector3(0,0,z + offSet));
		tr.localRotation = Quaternion.RotateTowards(tr.localRotation,desired,speed);
	}

	public static void RotateWithDirection(this Transform tr, Vector2 dir, float speed)
	{
		RotateWithDirection(tr,dir,speed,0);
	}
}
