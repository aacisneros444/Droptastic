using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager Instance;
    private int gamesSinceLastAd;
    private int currentRewardedVideoRewardType; //0 = more time, 1 = more misses

    [Header("References")]
    public DropBall dropBall;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("REDACTED", false);
        StartCoroutine(startBannerAd());
    }


    IEnumerator startBannerAd()
    {
        while (!Advertisement.IsReady("banner"))
            yield return null;

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        ShowBannerAd();
    }

    public void IncrementGamesSinceLastAd()
    {
        gamesSinceLastAd++;
    }

    public void CheckShowAd()
    {
        if(gamesSinceLastAd >= 4)
        {
            ShowVideoAd();
            gamesSinceLastAd = 0;
        }
    }

    public void ShowBannerAd()
    {
        if (Advertisement.IsReady("banner"))
        {
            Advertisement.Banner.Show("banner");
        }
    }

    public void ShowVideoAd()
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }

    public void ShowRewardedVideoAd(int rewardType)
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Time.timeScale = 0f;
            dropBall.acceptingInput = false;
            Advertisement.Show("rewardedVideo");
            currentRewardedVideoRewardType = rewardType;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "rewardedVideo")
        {
            if (showResult == ShowResult.Finished)
            {
                if (currentRewardedVideoRewardType == 0)
                {
                    GameLevelManager.Instance.RewardedVideoComplete(0);
                }

                if(currentRewardedVideoRewardType == 1)
                {
                    GameLevelManager.Instance.RewardedVideoComplete(1);
                }
                dropBall.acceptingInput = true;
                Time.timeScale = 1f;
            }

            ShowBannerAd();
        }
        if(placementId == "video")
        {
            ShowBannerAd();
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }
}
