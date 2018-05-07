using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class PlayerInput
{

    public float X { protected set; get; }
    public float Y { protected set; get; }
    public Vector2 XY { get { return new Vector2(X, Y); } }
    public Player Owner { protected set; get; }
    public EInputType InputType { protected set; get; }

    public InputActionButton TopActionButton { protected set; get; }
    public InputActionButton LeftActionButton { protected set; get; }

    public abstract void FixedUpdate();

    public void Set(float x, float y)
    {
        X = x;
        Y = y;
    }

    public PlayerInput(Player owner)
    {
        X = 0;
        Y = 0;
        Owner = owner;
    }


}

public class PlayerNetworkInput : PlayerInput
{

    public PlayerNetworkInput(Player owner) : base(owner)
    {
        InputType = EInputType.NETWORK;
        TopActionButton = new ActionButtonTopNetwork(owner.ID);
        LeftActionButton = new ActionButtonLeftNetwork(owner.ID);
    }

    public override void FixedUpdate()
    {

    }

}

public class PlayerGameControllerInput : PlayerInput
{
	protected Rewired.Player input;

    public PlayerGameControllerInput(Player owner) : base(owner)
    {
        InputType = EInputType.GAMECONTROLLER;
        TopActionButton = new ActionButtonTopGameController(owner.ID);
        LeftActionButton = new ActionButtonLeftGameController(owner.ID);

		input = Rewired.ReInput.players.GetPlayer(owner.ID);
    }

    public override void FixedUpdate()
    {
        TopActionButton.Press();
        LeftActionButton.Press();
		Set(input.GetAxis("Horizontal"), input.GetAxis("Vertical"));
    }

}

