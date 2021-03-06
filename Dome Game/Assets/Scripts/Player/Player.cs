﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour, IComparable<Player>   {

    public int ID { protected set; get; }

    public string Name { protected set; get; }

    public int Score { protected set; get; }

    public PlayerInput PInput { protected set; get; }


    public virtual void Init(int id, string Name)
	{
		ID = id;
		this.Name = Name;
    }
	public void SetPlayerInput(PlayerInput playerInput)
	{
		PInput = playerInput;
	}

    protected virtual void Move(Vector2 dir) { }
    public virtual void MoveSteer(Vector2 dir) { }


    public virtual void FixedUpdate() {


    }

    public void Kill()
    {
        Score++;
        ScoreController.SetHighScore();
    }

    public virtual void Death()
    {
        PlayerController.RemovePlayer(this);
        Destroy(gameObject);
    }

    public int CompareTo(Player other)
    {
        return Score.CompareTo(other.Score);
    }
}
