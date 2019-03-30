using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class PlayGamesController : MonoBehaviour
{/*
    public Text debugText;
    public Animator DebugTextAnimator;

    void Awake()
    {
        AuthonticateUser();
    }

    private void AuthonticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                UnlockAchivment(GPGSIds.achievement_welcome);
                debugText.text = ("Logged in");
            }
            else
            {
                DebugTextAnimator.Play("DebugTextShowAnimation");
            }
        });
    }

    public void OnConnectClick()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                //debugText.text = ("Logged in");
            }
            else
            {
                DebugTextAnimator.Play("DebugTextShowAnimation");
            }
        });
    }

    public static void PostToLeaderBoard(int newHighScore)
    {
        Social.ReportScore(newHighScore, GPGSIds.leaderboard_highest_score, (bool success) =>
        {
            if (success)
            {
                Debug.Log("New lederboardScore");
            }
            else
            {
                Debug.Log("Failed to leaderboard");
            }
        });
    }

    public void ShowLeaderBoard()
    {
        AudioManager.instance.Play("button");
        //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_highest_score);
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            CheckInterntConnection();
        }
    }

    public void UnlockAchivment(string achievmentID)
    {
        Social.ReportProgress(achievmentID, 100f, (bool succcess) =>
          {
              if (succcess)
              {
                  Debug.Log("Achivment Unlocked");
              }
          });
    }

    public void ShowAchievmentBoard()
    {
        AudioManager.instance.Play("button");
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            CheckInterntConnection();                 
        }
    }

    public void CheckInterntConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            DebugTextAnimator.Play("DebugTextShowAnimation");
        }
        else
        {
            OnConnectClick();
        }
    }*/
}
