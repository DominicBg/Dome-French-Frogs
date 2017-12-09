using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	enum ControlType {Direction,Steering};
	[SerializeField] ControlType controlType = ControlType.Direction;


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
	protected Vector2 currentDirection = Vector2.one;


	void Start()
	{
		transform.SetPositionSphere(Dome.instance.radiusClose);
		ship.RotateWithDirection(currentDirection,rotationSpeed);
	}

	public virtual void FixedUpdate () 
	{
		Vector2 inputJoyDir = new Vector2(Input.GetAxis("Horizontal"+id),Input.GetAxis("Vertical"+id));

		if (controlType == ControlType.Direction)
		{
			Move(inputJoyDir);
		}
		else
		{
			MoveSteer (inputJoyDir);
		}
		Fire();
		UpdatePositionSphere();

	}

	virtual protected void Move(Vector2 dir)
	{
		if(dir.magnitude != 0)
		{
			if (dir.magnitude > 1)
				dir.Normalize ();
			ship.RotateWithDirection(dir,rotationSpeed,0);
			transform.MoveSphere(dir, speed * Time.deltaTime, Dome.instance.radiusClose);
		}
		currentDirection = dir;
	}
	virtual protected void MoveSteer(Vector2 dir)
	{
		//X = rotation
		currentDirection = GameMath.RotateVector(-dir.x  * Time.deltaTime * rotationSpeed * 10,currentDirection);
		ship.RotateWithDirection(currentDirection,25);

		//Y = speed
		float isA = (Input.GetButton("Speed"+id) ? 0 : 1 );
		//transform.MoveSphere(currentDirection, speed * Time.deltaTime  * isA dir.y, Dome.instance.radiusClose);
		transform.MoveSphere(currentDirection, speed * Time.deltaTime  * isA, Dome.instance.radiusClose);

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
