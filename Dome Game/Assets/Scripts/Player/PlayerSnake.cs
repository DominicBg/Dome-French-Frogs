using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnake : Player
{

    #region Variables

    [Header("Debug")]
    public bool isDebug = false;

    [Header("Components")]
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField] Transform spriteTransform;
    Transform emptyParent;

    [Header("Prefabs")]
    [SerializeField]
    PlayerTail tailPrefab;

    [Header("Speed Parameters")]
    [SerializeField]
    float moveSpeed = 25;
    [SerializeField] float breakingSpeed = 10;
    [SerializeField] float rotationSpeed = 5;

    [Header("Tail Parameters")]
    [SerializeField]
    float tailDistance = 5;

    EPlayerSnakeMovement CurrentSnakeMovement;
    EDomeLayer CurrentDomeLayer;


    float currentSpeed;
    Vector2 currentDirection = Vector2.one;
    float currentDomeRadius;


    [SerializeField] List<PlayerTail> tailList = new List<PlayerTail>();

    [Header("Dash Parameters")]
    [SerializeField]
    float dashSpeedMultiplier = 2f;
    [SerializeField] float dashCooldown = 2;
    [SerializeField] float dashDuration = 1;
    bool dashReady = true;

    [Header("Dive Parameters")]
    [SerializeField]
    float diveDuration = 2;
    bool diveReady = true;
    #endregion

    //LE SPAWN EST CALL DANS LE DEBUG START

    #region DEBUG
    //DEBUG
    void Start()
    {
        if (isDebug)
            Spawn(1, new PlayerGameControllerInput(this), "testmonsieur");
    }

    void UpdatePositionSphere()
    {
        currentDomeRadius = Mathf.MoveTowards(currentDomeRadius, Dome.GetRadiusByDomeLayer(CurrentDomeLayer), 30 * Time.deltaTime);
        transform.SetPositionSphere(currentDomeRadius);
    }
    #endregion


    public override void FixedUpdate()
    {
        if (PInput != null)
        {
            PInput.FixedUpdate();
            MoveSteer(PInput.XY);
            UpdateSpeed();
            UpdateTail();
            UpdatePositionSphere();

            if (Input.GetKeyDown(KeyCode.X))
                AddTailPart();

            if (Input.GetKeyDown(KeyCode.L))
                Dive();
        }
    }

    public override void Spawn(int id, PlayerInput playerInput, string name)
    {
        ID = id;
        PInput = playerInput;
        Name = name;

        PInput.LeftActionButton.AddListener(Dash);
        PInput.TopActionButton.AddListener(Dive);

        CurrentSnakeMovement = EPlayerSnakeMovement.REGULAR;
        CurrentDomeLayer = EDomeLayer.LAYER0_CLOSE;

        currentDomeRadius = Dome.GetRadiusByDomeLayer(CurrentDomeLayer);
        transform.SetPositionSphere(currentDomeRadius);

        emptyParent = new GameObject("Player" + ID).transform;
        transform.SetParent(emptyParent, false);
    }

  

    private void UpdateSpeed()
    {
        float destSpeed = 0;
        float ClampedSpeed = Mathf.Lerp(moveSpeed, breakingSpeed, Mathf.Abs(PInput.Y));

        switch (CurrentSnakeMovement)
        {

            case EPlayerSnakeMovement.REGULAR:
                destSpeed = (PInput.Y < 0) ? ClampedSpeed : moveSpeed;
                break;
            case EPlayerSnakeMovement.DASH:
                destSpeed = moveSpeed * dashSpeedMultiplier;
                break;
            case EPlayerSnakeMovement.DIVE:
                destSpeed = (PInput.Y < 0) ? ClampedSpeed : moveSpeed;
                break;
            default:
                destSpeed = moveSpeed;
                break;
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, destSpeed, 30 * Time.deltaTime);

    }

    void Dash()
    {
        if (dashReady)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    void Dive()
    {
        if (diveReady)
        {
            StartCoroutine(DiveCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        diveReady = false;
        dashReady = false;

        CurrentSnakeMovement = EPlayerSnakeMovement.DASH;

        yield return new WaitForSeconds(dashDuration);

        CurrentSnakeMovement = EPlayerSnakeMovement.REGULAR;

        yield return new WaitForSeconds(dashCooldown - dashDuration);

        diveReady = true;
        dashReady = true;
    }

    IEnumerator DiveCoroutine()
    {
        dashReady = false;
        diveReady = false;

        CurrentDomeLayer = EDomeLayer.LAYER1_FAR;
        CurrentSnakeMovement = EPlayerSnakeMovement.DIVE;

        yield return new WaitForSeconds(diveDuration);

        CurrentDomeLayer = EDomeLayer.LAYER0_CLOSE;
        CurrentSnakeMovement = EPlayerSnakeMovement.REGULAR;

        diveReady = true;
        dashReady = true;
    }



    override public void MoveSteer(Vector2 dir)
    {
        //X input = rotation
        currentDirection = GameMath.RotateVector(-dir.x * Time.fixedDeltaTime * rotationSpeed * 10, currentDirection);
        currentDirection.Normalize();
        spriteTransform.RotateWithDirection(currentDirection, 25);
        transform.MoveSphere(spriteTransform.right, currentSpeed * Time.fixedDeltaTime);
    }

    void UpdateTail()
    {
        if (tailList.Count == 0)
            return;

        //Move first tail part with the head
        MoveTailLerp(transform, tailList[0].transform);

        //Move everyother tail part
        for (int i = tailList.Count - 1; i > 0; i--)
        {
            Transform prevTail = tailList[i - 1].transform;
            Transform currentTail = tailList[i].transform;
            MoveTailLerp(prevTail, currentTail);
        }
    }

    void MoveTailLerp(Transform prevTail, Transform currentTail)
    {
        Vector3 deltaPos = prevTail.position - currentTail.position;
        currentTail.position = Vector3.MoveTowards(
                                currentTail.position,
                                prevTail.position - deltaPos.normalized * tailDistance,
                                Time.fixedDeltaTime * currentSpeed * 2);

        currentTail.rotation = Quaternion.LookRotation(deltaPos, currentTail.up);
        currentTail.SetPositionSphere(Dome.instance.radiusClose);
    }

    public void AddTailPart()
    {
        Vector3 tailPos;
        if (tailList.Count == 0)
            tailPos = transform.position - spriteTransform.up * tailDistance;
        else
            tailPos = tailList[tailList.Count - 1].transform.position - tailList[tailList.Count - 1].transform.up * tailDistance;

        PlayerTail tailPart = Instantiate(tailPrefab, tailPos, transform.rotation);
        tailPart.transform.SetPositionSphere(Dome.instance.radiusClose);
        tailPart.transform.rotation = transform.rotation;

        tailPart.isLast = true;
        tailPart.playerRef = this;

        tailList.Add(tailPart);
        if (tailList.Count > 1)
            tailList[tailList.Count - 2].isLast = false;

        tailPart.transform.SetParent(emptyParent, false);
    }

    public override void Death()
    {
        base.Death();

        Destroy(emptyParent.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tail"))
        {
            PlayerTail tail = other.gameObject.GetComponent<PlayerTail>();

            if (tail.isLast)
            {
                Kill();
                tail.playerRef.Death();
            }
            else
                Death();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSnake p = other.gameObject.GetComponent<PlayerSnake>();
            p.Death();
            Death();
        }
    }
}
