using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InputActionButton
{
    public UnityEvent OnPress;

    public int ID { private set; get; }

    public void Press()
    {
        if (IsPressing())
            OnPress.Invoke();
    }

    public void AddListener(Action listener )
    {
        OnPress.AddListener(()=>listener());
    }

    protected virtual bool IsPressing()
    {
        return false;
    }

    public InputActionButton(int id)
    {
        ID = id;
        OnPress = new UnityEvent();
    }


}


public class ActionButtonTopGameController : InputActionButton
{
    public ActionButtonTopGameController(int id) : base(id)
    {

    }

    protected override bool IsPressing()
    {
        return Input.GetButtonDown("Dive" + ID);
    }

}


public class ActionButtonTopNetwork : InputActionButton
{
    public ActionButtonTopNetwork(int id) : base(id)
    {

    }

    protected override bool IsPressing()
    {
        return true;
    }

}


public class ActionButtonLeftNetwork : InputActionButton
{
    public ActionButtonLeftNetwork(int id) : base(id)
    {

    }

    protected override bool IsPressing()
    {
        return true;
    }

}

public class ActionButtonLeftGameController: InputActionButton
{
    public ActionButtonLeftGameController(int id) : base(id)
    {

    }

    protected override bool IsPressing()
    {
         return Input.GetButtonDown("Dash" + ID);
    }

}

