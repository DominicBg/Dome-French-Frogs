using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardNode  {

    public string Name;
    public int Score;
    public string DateTimeStamp;

    public LeaderboardNode(string name, int score, string dateTimeStamp)
    {
        Name = name;
        Score = score;
        DateTimeStamp = dateTimeStamp;
    }

}

[System.Serializable]
public class LeaderboardNodeJS
{
    public LeaderboardNode[] LeaderboardNodeArray;

    public void Print()
    {
        for(int i = 0; i < LeaderboardNodeArray.Length; i++)
        {
            Debug.Log("Name : " + LeaderboardNodeArray[i].Name);
        }
    }
}
