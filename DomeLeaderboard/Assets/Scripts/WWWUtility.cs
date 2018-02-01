using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WWWUtility
{

    private const string GetLeaderboardURL = "//localhost/Dome/PHP/GetLeaderboard.php?TimeStamp=";
    private const string AddLeaderboardURL = "//localhost/Dome/PHP/AddToLeaderboard.php?JSON=";
    private const string PerformCustomQueryURL = "//localhost/Dome/PHP/CustomQuery.php?Query=";


    public static string GetDateTimeToMySQL(DateTime TimeStamp)
    {
        return TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static IEnumerator AddtoLeaderboard(LeaderboardNode leaderboardNode)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.

     
        string LeaderboardDataJSON = JsonUtility.ToJson(leaderboardNode);

        string post_url = AddLeaderboardURL + WWW.EscapeURL(LeaderboardDataJSON);

        Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);

        yield return hs_post; // Wait until the download is done

        if (hs_post.error.Length > 0)
        {
            Debug.Log("There was an error adding the names: " + hs_post.error);
        }
        else
        {
            Debug.Log("succeded");
        }

        yield break;
    }

    public static IEnumerator ExecuteCustomMYSQLQuery(string query)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.

        string post_url = PerformCustomQueryURL + query;

        Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);

        yield return hs_post; // Wait until the download is done

        if (hs_post.error.Length > 0)
        {
            Debug.Log("There was an error adding the names: " + hs_post.error);
        }
        else
        {
            Debug.Log("succeded");
        }

        yield break;
    }

    public static IEnumerator GetLeaderboard(DateTime TimeStamp, Action<LeaderboardNodeJS> LeaderboardCallBack)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.

        string post_url = GetLeaderboardURL + WWW.EscapeURL(GetDateTimeToMySQL(TimeStamp));

        Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);

        yield return hs_post; // Wait until the download is done

        if (hs_post.error.Length > 0)
        {
            LeaderboardCallBack(null);
            Debug.Log("There was an error adding the names: " + hs_post.error);
        }
        else
        {

            LeaderboardNodeJS LeaderboardNode = JsonUtility.FromJson<LeaderboardNodeJS>(hs_post.text);
            LeaderboardCallBack(LeaderboardNode);
        }

        yield break;
    }

}
