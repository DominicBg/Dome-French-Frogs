using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnake : Player {

	#region Variables
	[Header("Components")]
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Transform spriteTransform;

	[Header("Prefabs")]
	[SerializeField] GameObject tailPrefab;
	
	[Header("Param")]
	[SerializeField] float tailDistance = 5;

	[SerializeField] float moveSpeed = 25;
	[SerializeField] float rotationSpeed = 5;
	[SerializeField] float friction = 5;
	[SerializeField] float acceleration = 10f;
	[SerializeField] float maxVelocity = 10;

	Vector2 currentDirection = Vector2.one;
	Vector2 velocity;
	Vector2 previousDirection;
	[SerializeField] List<TailPart> tailList = new List<TailPart>();

	#endregion

	//LE SPAWN EST CALL DANS LE DEBUG START

	#region DEBUG
	//DEBUG
	void Start()
	{
		Spawn(1);
	}
	void UpdatePositionSphere()
	{
		if (Input.GetKeyDown(KeyCode.Z))
			transform.SetPositionSphere(Dome.instance.radiusClose);
	}
	#endregion


	public override void FixedUpdate ()
	{
		Vector2 inputJoyDir = new Vector2(Input.GetAxis("Horizontal"+ID),Input.GetAxis("Vertical"+ID));
		//MoveSteer (inputJoyDir);
		PressActionButton();
		UpdateTail();

		//Debug
		UpdatePositionSphere();
		if(Input.GetKeyDown(KeyCode.Z))
			AddTailPart();
	}
	
	protected override void Spawn (int id)
	{
		ID = id;
		transform.SetPositionSphere(Dome.instance.radiusClose);
	}

	public override void PressActionButton ()
	{

	}


	override public void MoveSteer(Vector2 dir)
	{
		previousDirection = currentDirection;
		//X = rotation
		currentDirection = GameMath.RotateVector(-dir.x  * Time.fixedDeltaTime * rotationSpeed * 10, currentDirection);
		currentDirection.Normalize();
		//SetVelocity(currentDirection);
		
		spriteTransform.RotateWithDirection(currentDirection, 25);
		transform.MoveSphere(spriteTransform.right, moveSpeed * Time.fixedDeltaTime);
	}
	
	void SetVelocity(Vector2 dir)
	{		
		//Y = speed
		if(dir.magnitude == 0 || Input.GetButton("Speed"+ID))
		{	
			velocity -= velocity*friction*Time.deltaTime;
		}
		else
		{
			if (dir.magnitude > 1)
				dir.Normalize ();
			
			velocity -= velocity*friction*Time.deltaTime;
			
			velocity += dir * acceleration * Time.deltaTime;
			if(velocity.magnitude > maxVelocity)
				velocity = velocity.normalized * maxVelocity;
			
			//spriteTransform.RotateWithDirection(dir,rotationSpeed);
			
			//currentDirection = dir;
		}
	}
	void UpdateTail()
	{
		if(tailList.Count == 0)
			return;

		tailList[0].direction = previousDirection;
		MoveTail(tailList[0]);

		for (int i = tailList.Count-1; i >= 1; i--) 
		{
			TailPart currentTail = tailList[i];
			currentTail.direction = tailList[i-1].direction;
			//currentTail.direction = currentDirection;
			MoveTail(currentTail);
		}
	}
	void MoveTail(TailPart tail)
	{
		tail.spriteTr.RotateWithDirection(tail.direction, 25);
		tail.tr.MoveSphere(tail.spriteTr.right, moveSpeed * Time.fixedDeltaTime);
	}

	[ContextMenu("Add tail part")]
	public void AddTailPart()
	{	
		Vector3 tailPos = transform.position - (tailList.Count+1) * spriteTransform.up * tailDistance;
		GameObject tailPart = Instantiate(tailPrefab,tailPos,transform.rotation);
		tailPart.transform.SetPositionSphere(Dome.instance.radiusClose);

		//tailPart.transform.position = transform.position;
		tailPart.transform.rotation = transform.rotation;
		tailPart.transform.GetChild(0).rotation = spriteTransform.rotation;

		tailList.Add(
			new TailPart(tailPart.transform,tailPart.transform.GetChild(0),currentDirection)
			);
	}

	[System.Serializable]
	class TailPart
	{
		public TailPart(Transform tr, Transform spriteTr, Vector3 direction)
		{
			this.tr = tr;
			this.spriteTr = spriteTr;
			this.direction = direction;
		}
		public Transform tr;
		public Transform spriteTr;
		public Vector3 direction;
	}
}
