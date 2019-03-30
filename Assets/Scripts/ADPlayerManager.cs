using UnityEngine;
using UnityEngine.Advertisements;

public class ADPlayerManager : MonoBehaviour
{
    public int losestoSeeAD = 3;
    private int  currentlosses = 0;

    public static ADPlayerManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Advertisement.Initialize("3005065");
       // Debug.Log(Advertisement.isInitialized);
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
    }

    public void CheckToPlayAD()
    {
        currentlosses++;
        if (currentlosses >= losestoSeeAD)
        {
            
            currentlosses = 0;
            ShowAd();
        }
    }
}
