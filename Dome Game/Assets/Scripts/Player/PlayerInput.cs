using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerInput
{

    public float X { protected set; get; }
    public float Y { protected set; get; }
    public Vector2 XY { get { return new Vector2(X, Y); } }
    public Player Owner { protected set; get; }
    public EInputType InputType { protected set; get; }

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

    }

    public override void FixedUpdate()
    {

    }
}

public class PlayerGameControllerInput : PlayerInput
{

    public PlayerGameControllerInput(Player owner) : base(owner)
    {
        InputType = EInputType.GAMECONTROLLER;
    }

    public override void FixedUpdate()
    {
        Set(Input.GetAxis("Horizontal" + Owner.ID), Input.GetAxis("Vertical" + Owner.ID));
    }
}

