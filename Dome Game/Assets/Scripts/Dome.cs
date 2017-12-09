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
	public static void MoveSphere(this Transform tr, Vector2 direction, float speed, float radius)
	{
		/*
		float sphereCorrector = Mathf.Cos((tr.position.y/radius) * Mathf.PI*.5f) + 1;
		tr.position += new Vector3(0,direction.y,0) * speed / sphereCorrector;

		if(tr.position.y > radius-Dome.instance.offTop)
			tr.position = tr.position.SetY(radius-1);
		tr.RotateAround(center,Vector3.up,direction.x * speed );

		SetPositionSphere(tr,radius);
		*/


		float sphericCoef = 1 + Mathf.Sin((tr.position.y/radius) * Mathf.PI * 0.5f);
		Debug.DrawRay(tr.position, center - tr.position, Color.blue);

		if((tr.position.y <= radius - Dome.instance.offTop && direction.y > 0) ||
			(tr.position.y >= Dome.instance.limitBottom && direction.y < 1))
			tr.RotateAround(center,tr.right,direction.y * speed);

		tr.RotateAround(center,Vector3.up,direction.x * speed * sphericCoef);
		//tr.RotateAround(center,Vector3.up,direction.x * speed );


		/*
		tr.position += (tr.right * direction.x) + (tr.up * direction.y) * speed;
		tr.SetPositionSphere (radius);
		*/

		tr.LookAt(center);
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

		tr.LookAt(center);
	}


	public static void RotateWithDirection(this Transform tr, Vector2 dir, float speed, float offSet)
	{
		float nonZeroY = (dir.y == 0) ? Mathf.Epsilon : dir.y;
		float z = Mathf.Atan2(dir.x , nonZeroY) * Mathf.Rad2Deg;
		Quaternion desired = Quaternion.Euler(new Vector3(0,0,z + offSet));

		tr.localRotation = Quaternion.RotateTowards(tr.localRotation,desired,speed);
	}

	public static void RotateWithDirection(this Transform tr, Vector2 dir, float speed)
	{
		RotateWithDirection(tr,dir,speed,0);
	}
}
