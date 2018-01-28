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
	/// Rotate around in a spheric motion
	/// </summary>
	/// <param name="tr">The transform</param>
	/// <param name="transformRight">The right vector of your transform.</param>
	/// <param name="speed">The Speed, consider multiplying your speed by Time.deltaTime</param>
	public static void MoveSphere(this Transform tr,Vector3 transformRight, float speed)
	{
		tr.RotateAround(center, transformRight, speed);
        //TODO 
        //limiter le y
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
