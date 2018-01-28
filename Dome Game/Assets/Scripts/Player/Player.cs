using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour   {

    public int ID { protected set; get; }
    public PlayerInput PInput { protected set; get; }

    public virtual void Spawn(int id, PlayerInput inputType){

    }


    protected virtual void Move(Vector2 dir) { }
    public virtual void MoveSteer(Vector2 dir) { }
    public virtual void PressActionButton() { }

    public virtual void FixedUpdate() {


    }








}
