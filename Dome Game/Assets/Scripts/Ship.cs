using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	[Header("Components")]
	public SpriteRenderer sr;
	public Transform ship;

	[Header("Param")]
	public float speed = 25;
	public float rotationSpeed = 5;

	[Header("Bullets")]
	public GameObject bulletPrefab;
	public float bulletSpeed;  

	[Header("id")]
	public int id;

	//Protected/private
	protected Vector2 currentDirection;


	void Start()
	{
		transform.SetPositionSphere(Dome.instance.radiusClose);
	}

	public virtual void FixedUpdate () 
	{
		Vector2 dir = new Vector2(Input.GetAxis("Horizontal"+id),Input.GetAxis("Vertical"+id));

		Move(dir);
		Fire();
		UpdatePositionSphere();

	}

	virtual protected void Move(Vector2 dir)
	{
		if(dir.magnitude != 0)
		{
			ship.RotateWithDirection(dir,rotationSpeed,0);
			transform.MoveSphere(dir, speed * Time.deltaTime, Dome.instance.radiusClose);
		}
		currentDirection = dir;
	}
	
	void Fire()
	{
		if(Input.GetButtonDown("Shoot"+id))
		{
			GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
			bullet.GetComponent<Bullet>().Init(bulletSpeed,currentDirection.normalized,id);
		}
	}

	void UpdatePositionSphere()
	{
		if(Input.GetKeyDown(KeyCode.Z))
			transform.SetPositionSphere(Dome.instance.radiusClose);
	}
    public void OnTriggerEnter(Collider collider)
    {
		if (collider.CompareTag("Bullet") && collider.GetComponent<Bullet>().id != id)
		{    
			Destroy(collider.gameObject);
			Destroy(gameObject);
		}
    }
}
