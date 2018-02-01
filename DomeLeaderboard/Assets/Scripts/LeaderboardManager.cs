using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        /*


        //Add element to leaderboard
        LeaderboardNode l = new LeaderboardNode("Bougie", 20, WWWUtility.GetDateTimeToMySQL(new DateTime(2017, 1, 18)));
        StartCoroutine(WWWUtility.AddtoLeaderboard(l));

        //Get Leaderboard array from leaderboard
        StartCoroutine(WWWUtility.GetLeaderboard(new DateTime(2017,1,1), GetLeaderboard));

      */

        //Perform Custom MYSQL Query
        StartCoroutine(WWWUtility.ExecuteCustomMYSQLQuery("DELETE FROM `dome_db`")); 




    }

    void GetLeaderboard(LeaderboardNodeJS Leaderboard)
    {
        if (Leaderboard != null)
        {
            Leaderboard.Print();
        }
        else
        {
            Debug.Log("Leaderboard is null");
        }
    }


}
