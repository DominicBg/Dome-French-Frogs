using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour   {

    public int ID { protected set; get; }
    public PlayerInput PInput { protected set; get; }
    public PlayerScore scoreRef;

    public virtual void Spawn(int id, PlayerInput inputType){

    }


    protected virtual void Move(Vector2 dir) { }
    public virtual void MoveSteer(Vector2 dir) { }
    public virtual void PressActionButton() { }

    public virtual void FixedUpdate() {


    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Tail")
        {
            Debug.Log("coucoiu");
            PlayerTail tail = other.gameObject.GetComponent<PlayerTail>();

            if (tail.isLast)
            {
               Kill();
               tail.playerRef.Death();
            }
            else
                Death();

        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("coucoiu2");
            PlayerSnake p = other.gameObject.GetComponent<PlayerSnake>();
            p.Death();
            Death();
        }
    }

    public void Kill()
    {
        scoreRef.GainPoints();
        GameController.GetInstance().SetHighScore();

    }

    public virtual void Death()
    {
        PlayerController.GetInstance().GetPlayerList().Remove(this);

        GameController.GetInstance().RemoveScore(scoreRef);
        Destroy(gameObject);
    }



}
