using UnityEngine;
//using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;

public class FaceBookManager : MonoBehaviour
{/*
    public Text userIDText;
    public Text FriendsText;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(()=>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    Debug.Log("couldnt initialize");
                }
            });
        }
        else
        {
            FB.ActivateApp();
        }

        LogIn();
    }

    public void LogIn() // for login button
    {
        var permission = new List<string>()
        {"public_profile","email","user_friends" };

        FB.LogInWithReadPermissions(permissions: permission, callback: OnLogIn);
    }

    public void LogOut() // logout button
    {
        FB.LogOut();
    }

    private void OnLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;
            userIDText.text = token.UserId;
        }
        else
        {
            Debug.Log("Couldnot login");
        }
    }

    public void Share()
    {
        FB.ShareLink(
            contentTitle:"Our Game",
            contentURL:new System.Uri("http://google.com"),
            contentDescription:"Here is My Game",
            callback:OnShare);
    }

    private void OnShare(IShareResult result) // for sharing button
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Share link error" + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("Share done");
        }
    }


    public void FaceBookGameRequest()
    {
        FB.AppRequest(title: "My Game", message: "Come Play These Game");
    }

    public void FaceBookInvite()
    {
        FB.Mobile.AppInvite(new System.Uri("http://google.com"));
    }

    public void GetFriendsPlayingTheseGame()
    {
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
          {
              var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
              var frienList = (List<object>)dictionary["data"];
              FriendsText.text = string.Empty;
              foreach (var dict in frienList)
              {
                  FriendsText.text += ((Dictionary<string, object>)dict)["name"];
              }
          });
    }*/
}
