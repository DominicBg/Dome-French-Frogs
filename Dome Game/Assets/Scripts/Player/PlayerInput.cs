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


    public UnityEvent OnPressActionButton;

    public abstract bool IsPressingActionButton();

    public void PressActionButton()
    {
        if (IsPressingActionButton())
            OnPressActionButton.Invoke();
    }



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
        OnPressActionButton = new UnityEvent();
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

    public override bool IsPressingActionButton()
    {
        return true;
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
        PressActionButton();
        Set(Input.GetAxis("Horizontal" + Owner.ID), Input.GetAxis("Vertical" + Owner.ID));
    }

    public override bool IsPressingActionButton()
    {
        return Input.GetButtonDown("Shoot" + Owner.ID);
    }
}

