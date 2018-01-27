using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour   {

    public int ID { protected set; get; }

    protected virtual void Spawn(int id) { }
    protected virtual void Move(Vector2 dir) { }
    protected virtual void MoveSteer(Vector2 dir) { }
    protected virtual void PressActionButton() { }

    public virtual void FixedUpdate() { }




}
