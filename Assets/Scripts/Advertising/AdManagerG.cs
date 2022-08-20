using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManagerG : MonoBehaviour
{
    public static AdManagerG Instance;

    private int gamesSinceLastAd;

    [Header("References")]
    public DropBall dropBall;

    [Header("Ad Objects")]
    public InterstitialAd interstitial;
    public RewardBasedVideoAd rewardBasedVideo;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MobileAds.Initialize((initStatus) => {
            rewardBasedVideo = RewardBasedVideoAd.Instance;
            RequestInterstitialAd();
            RequestRewardBasedVideo();
            // Called when an ad is shown.
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // Called when the ad is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // Called when the user should be rewarded for watching a video.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        });
    }

    private void RequestInterstitialAd()
    {

#if UNITY_ANDROID
        string adUnitId = "REDACTED";
#elif UNITY_IPHONE
        string adUnitId = "REDACTED";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        //Add event handlers
        interstitial.OnAdOpening += HandleOnAdOpened;
        interstitial.OnAdClosed += HandleOnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    private void HandleOnAdOpened(object sender, EventArgs args)
    {
        gamesSinceLastAd = 0;
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        RequestInterstitialAd();
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
        dropBall.acceptingInput = false;
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        RequestRewardBasedVideo();
        Time.timeScale = 1f;
        dropBall.acceptingInput = true;
    }

    private void HandleRewardBasedVideoRewarded(object sender, Reward e)
    {
        GameLevelManager.Instance.RewardedVideoComplete(0);
    }

    public void ShowInterstitialAd()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            RequestInterstitialAd();
        }
    }

    public void ShowRewardedVideoAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            RequestRewardBasedVideo();
        }
    }

    public void CheckShowAd()
    {
        if (gamesSinceLastAd >= 4)
        {
            ShowInterstitialAd();            
        }
    }

    public void IncrementGamesSinceLastAd()
    {
        gamesSinceLastAd++;
    }
}
