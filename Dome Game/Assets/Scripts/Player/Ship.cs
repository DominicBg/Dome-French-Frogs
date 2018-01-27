using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Player {

    enum ControlType {Direction,Steering};
	[SerializeField] ControlType controlType = ControlType.Direction;

    [Header("Debug")]
    public bool isDebug = false;

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
    public int id = 1;



    //Protected/private
    protected Vector2 currentDirection = Vector2.one;

    private void Start()
    {
        if(isDebug)
        ID = id;
    }

    public override void FixedUpdate () 
	{
        if (!isLocalPlayer)
            return;


		Vector2 inputJoyDir = new Vector2(Input.GetAxis("Horizontal"+ID),Input.GetAxis("Vertical"+ID));

		if (controlType == ControlType.Direction)
		{
			Move(inputJoyDir);
		}
		else
		{
			MoveSteer (inputJoyDir);
		}
        PressActionButton();
		UpdatePositionSphere();

	}

   
	
    public void OnTriggerEnter(Collider collider)
    {
		if (collider.CompareTag("Bullet") && collider.GetComponent<Bullet>().id != ID)
		{    
			Destroy(collider.gameObject);
			Destroy(gameObject);
		}
    }

    protected override void Spawn(int id)
    {
        ID = id;
        transform.SetPositionSphere(Dome.instance.radiusClose);
        ship.RotateWithDirection(currentDirection, rotationSpeed);
    }

    protected override void Move(Vector2 dir)
    {
        if (dir.magnitude != 0)
        {
            if (dir.magnitude > 1)
                dir.Normalize();
            ship.RotateWithDirection(dir, rotationSpeed, 0);
            transform.MoveSphereSprite(ship, dir, speed * Time.deltaTime, Dome.instance.radiusClose);
        }
        currentDirection = dir;
    }

    protected override void MoveSteer(Vector2 dir)
    {
        //X = rotation
        currentDirection = GameMath.RotateVector(-dir.x * Time.deltaTime * rotationSpeed * 10, currentDirection);
        ship.RotateWithDirection(currentDirection, 25);

        //Y = speed
        //float isA = (Input.GetButton("Speed"+id) ? 0 : 1 );
        //transform.MoveSphere(currentDirection, speed * Time.deltaTime  * isA dir.y, Dome.instance.radiusClose);
        transform.MoveSphereSprite(ship, dir, speed * Time.deltaTime, Dome.instance.radiusClose);
    }

    void UpdatePositionSphere()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            transform.SetPositionSphere(Dome.instance.radiusClose);
    }

    protected override void PressActionButton()
    {
        if (Input.GetButtonDown("Shoot" + ID))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Init(bulletSpeed, currentDirection.normalized, ID);
        }
    }
}
