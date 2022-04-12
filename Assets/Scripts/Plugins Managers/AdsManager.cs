using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BannerPosition
{
    Top = 0,
    Bottom = 1,
    TopLeft = 2,
    TopRight = 3,
    BottomLeft = 4,
    BottomRight = 5,
    Center = 6
}

public abstract class AdsManager : MonoBehaviour
{

    public static AdsManager Instance;
    public Action OnBannerLoaded;

    private void Awake()
    {
        Instance = this;
    }


    public abstract void InitializePlugin();

    public abstract void RequestBannerAd(BannerPosition bannerPosition = BannerPosition.Bottom);
    public abstract void RequestInterstitial();
    public abstract void RequestRewardedVideo();
    public abstract void RequestRewardedInterstitial();

    public virtual bool IsBannerAdReady => false;
    public virtual bool IsRewardedAdReady => false;

    public virtual bool IsInterstitialAdReady => false;

    public abstract void CloseBanner();

    public abstract void ShowBannerAd(Action actionOnBannerShow);
    public abstract void ShowInterstitial();
    public abstract void ShowRewardedVideo(Action rewardedDelegate, Action onRewardedVideoShown, Action onRewardedVideoNotShown, string rewardedPlacement);
    public abstract void ShowRewardedInterstitial(Action rewardedDelegate, Action onRewardedInterstitialShown, Action onRewardedInterstitialNotShown);

}
