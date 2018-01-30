using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreController : Singleton<ScoreController>
{

    [SerializeField]
    PlayerScore prefabScore;

    [SerializeField]
    int highestScore;

    [SerializeField]
    Text highScoreText;

    [SerializeField]
    Transform scoreLeaderboardTransform;

    public static List<PlayerScore> Leaderboard { private set; get; }

    private void Start()
    {
        Leaderboard = new List<PlayerScore>();
        PlayerController.OnInstantiatePlayer.AddListener(InstantiateScore);
        PlayerController.OnRemovePlayer.AddListener(RemoveScore);
    }

    void InstantiateScore(Player player)
    {
        PlayerScore newScore = InstantiateScore();
        newScore.SetPlayer(player);

        Leaderboard.Add(newScore);
    }

    void RemoveScore(Player player)
    {
        for (int i = 0; i < Leaderboard.Count; i++)
        {
            if (Leaderboard[i].CurrentPlayer.Name == player.Name)
            {
                PlayerScore RemovedPlayerScore = Leaderboard[i];
                Leaderboard.Remove(Leaderboard[i]);
                Destroy(RemovedPlayerScore.gameObject);
            }

        }

    }


    public static PlayerScore InstantiateScore()
    {
        PlayerScore score = Instantiate(Instance.prefabScore);
        score.transform.SetParent(Instance.scoreLeaderboardTransform, false);
        score.gameObject.SetActive(true);
        return score;
    }

    public static void SetHighScore()
    {
        List<Player> ScoreList = PlayerController.GetPlayerListByScore;

        for (int i = 0; i < Leaderboard.Count; i++)
        {
            Leaderboard[i].SetPlayer(ScoreList[i]);
        }

    }

}
