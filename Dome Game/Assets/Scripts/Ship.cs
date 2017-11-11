using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public float speed = 25;
	public float rotationSpeed = 5;
	public SpriteRenderer sr;
	public Transform ship;
	public GameObject bulletPrefab;
    public bool isCurrentPlayer;
    protected Vector2 direction;

	void Start()
	{
		transform.SetPositionSphere(Dome.radiusClose);
	}

	// Update is called once per frame
	public virtual void FixedUpdate () 
	{
        if(isCurrentPlayer)
        {
            Move();
            Fire();
        }
	}
	
	virtual protected void Move()
	{
		direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

		if(direction.magnitude != 0)
		{
			ship.RotateWithDirection(direction,rotationSpeed,0);
			transform.MoveSphere(direction, speed * Time.deltaTime, Dome.radiusClose);
		}

	}
	
	void Fire()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
			bullet.GetComponent<Bullet>().Init(50,direction.normalized);
		}
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            Destroy(collision.collider.gameObject);
    }
}
