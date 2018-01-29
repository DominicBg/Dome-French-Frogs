﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnake : Player
{

    #region Variables

    [Header("Debug")]
    public bool isDebug = false;

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

	float currentSpeed;
    Vector2 currentDirection = Vector2.one;
    Vector2 velocity;
    Vector2 previousDirection;
    [SerializeField] List<TailPart> tailList = new List<TailPart>();

	[Header("Dash Param")]
	[SerializeField] float dashSpeedIncrease = 25;
	[SerializeField] float dashCooldown = 2;
	[SerializeField] float dashDuration = 1;
	bool dashReady = true; 
    #endregion

    //LE SPAWN EST CALL DANS LE DEBUG START

    #region DEBUG
    //DEBUG
    void Start()
    {
        if (isDebug)
            Spawn(1, new PlayerGameControllerInput(this));
	
    }
    void UpdatePositionSphere()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            transform.SetPositionSphere(Dome.instance.radiusClose);
    }
    #endregion


    public override void FixedUpdate()
    {
        if (PInput != null)
        {
            PInput.FixedUpdate();
            MoveSteer(PInput.XY);
            PressActionButton();

            UpdateTail();

            //Debug
			//Debug.Log("Player id : " + ID + " control type " + PInput.InputType);
            UpdatePositionSphere();
            if (Input.GetKeyDown(KeyCode.X))
                AddTailPart();
        }

    }

    public override void Spawn(int id, PlayerInput playerInput)
    {
        ID = id;
        transform.SetPositionSphere(Dome.instance.radiusClose);
        PInput = playerInput;
    }

    public override void PressActionButton()
    {
		if(currentSpeed > moveSpeed)
			currentSpeed -= (dashSpeedIncrease / dashDuration) * Time.deltaTime;
		else
			currentSpeed = moveSpeed;

		//TRISTAN
		if(Input.GetButtonDown("Shoot"+ID) && dashReady)
			Dash();
    }

	void Dash()
	{
		currentSpeed = moveSpeed + dashSpeedIncrease;
		StartCoroutine(DashCoolDownDelay());
	}

	IEnumerator DashCoolDownDelay()
	{
		dashReady = false;
		yield return new WaitForSeconds(dashCooldown);
		dashReady = true;
	}
    override public void MoveSteer(Vector2 dir)
    {
        previousDirection = currentDirection;
        //X input = rotation
        currentDirection = GameMath.RotateVector(-dir.x * Time.fixedDeltaTime * rotationSpeed * 10, currentDirection);
        currentDirection.Normalize();
        //SetVelocity(currentDirection);

        spriteTransform.RotateWithDirection(currentDirection, 25);
        //transform.MoveSphere(spriteTransform.right, moveSpeed * Time.fixedDeltaTime);
		transform.MoveSphere(spriteTransform.right, currentSpeed * Time.fixedDeltaTime);
    }

	//Currently disabled
    void SetVelocity(Vector2 dir)
    {
        //Y input = speed
        if (dir.magnitude == 0 || Input.GetButton("Speed" + ID))
        {
            velocity -= velocity * friction * Time.deltaTime;
        }
        else
        {
            if (dir.magnitude > 1)
                dir.Normalize();

            velocity -= velocity * friction * Time.deltaTime;

            velocity += dir * acceleration * Time.deltaTime;
            if (velocity.magnitude > maxVelocity)
                velocity = velocity.normalized * maxVelocity;
        }
    }
    void UpdateTail()
    {
        if (tailList.Count == 0)
            return;

		//Move first tail part with the head
        MoveTailLerp(transform, spriteTransform, tailList[0], 0);

		//Move everyother tail part
        for (int i = tailList.Count - 1; i > 0; i--)
        {
            TailPart prevTail = tailList[i - 1];
            TailPart currentTail = tailList[i];
            MoveTailLerp(prevTail.tr, prevTail.spriteTr, currentTail, i);
        }
    }

    void MoveTailLerp(Transform prevTail, Transform prevSpriteTransform, TailPart currentTail, int i)
    {
        Vector3 deltaPos = prevTail.position - currentTail.tr.position;
        currentTail.tr.position = Vector3.MoveTowards(
								currentTail.tr.position,
                                prevTail.position - deltaPos.normalized * tailDistance,
								Time.fixedDeltaTime * currentSpeed * 2);
		
		currentTail.tr.rotation = Quaternion.LookRotation(deltaPos,currentTail.tr.up);
        currentTail.tr.SetPositionSphere(Dome.instance.radiusClose);		
    }

    public void AddTailPart()
    {
        Vector3 tailPos;
        if (tailList.Count == 0)
            tailPos = transform.position - spriteTransform.up * tailDistance;
        else
            tailPos = tailList[tailList.Count - 1].tr.position - tailList[tailList.Count - 1].spriteTr.up * tailDistance;

        GameObject tailPart = Instantiate(tailPrefab, tailPos, transform.rotation);
        tailPart.transform.SetPositionSphere(Dome.instance.radiusClose);
        tailPart.transform.rotation = transform.rotation;

        tailList.Add(
            new TailPart(tailPart.transform, tailPart.transform.GetChild(0), currentDirection)
            );
    }

	///SpriteTR maybe useless
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
