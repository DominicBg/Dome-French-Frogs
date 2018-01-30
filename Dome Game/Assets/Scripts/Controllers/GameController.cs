using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController instance;
    public static GameController GetInstance() { return instance; }

    [SerializeField]
    PlayerScore prefabScore;
    [SerializeField]
    int highestScore;
    [SerializeField]
    Text highScoreText;
    [SerializeField]
    Transform scoreLeaderboardTransform;

    [SerializeField]
    List<PlayerScore> scoreList = new List<PlayerScore>();

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InstanciateNewScore(Player p)
    {
        PlayerScore score = Instantiate(prefabScore);
        score.transform.SetParent(scoreLeaderboardTransform,false);
        score.SetValues("TestMonsieur");
        score.gameObject.SetActive(true);
        p.scoreRef = score;
        scoreList.Add(score);
       // p.scoreRef 
    }

    public void SetHighScore()
    {
        foreach(PlayerScore score in scoreList)
        {
            if(score.currentScore > highestScore)
            {
                highestScore = score.currentScore;
                highScoreText.text = score.playerNameTxt.text + ": " + score.currentScore;
                score.isLeading = true;
            }
        }
    }

    public void RemoveScore(PlayerScore score)
    {
        scoreList.Remove(score);
        Destroy(score.gameObject);

    }
}
