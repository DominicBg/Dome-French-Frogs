using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour   {

    [SyncVar]
    public int ID;

    protected virtual void Spawn(int id) { }
    protected virtual void Move(Vector2 dir) { }
    protected virtual void MoveSteer(Vector2 dir) { }
    protected virtual void PressActionButton() { }

    public virtual void FixedUpdate() {


    }








}
