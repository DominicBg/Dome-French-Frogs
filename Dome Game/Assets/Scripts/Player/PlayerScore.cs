using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour {

    public TextMeshProUGUI playerNameTxt, scoreText;

    public Player CurrentPlayer { private set; get; }
 
  

	// Use this for initialization
	void Start () {
        gameObject.SetActive(true);
	}
	
    public void SetName()
    {
        playerNameTxt.text = CurrentPlayer.Name;
    }

    public void SetScore()
    {
        scoreText.text = CurrentPlayer.Score.ToString();
    }

    public void SetPlayer(Player p)
    {
        CurrentPlayer = p;
        SetName();
        SetScore();
    }

  
}
