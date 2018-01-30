using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public int currentScore;
    public Text playerNameTxt, scoreText;
 
    public bool isLeading;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetValues(string name)
    {
        playerNameTxt.text = name;
    }

    public void GainPoints()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }
}
