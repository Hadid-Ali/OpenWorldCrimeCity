using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRewardPanel : InGamePanel
{
    protected AdCalls _adsManager;

    [SerializeField]
    protected bool closeOnReward = true;

    void Awake()
    {
        this._adsManager = AdCalls.instance;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this._adsManager.CreateAndLoadRewardedAd();
    }

    public virtual void OnShowRewardedVideo()
    {

    }

    public virtual void OnRewardedVideoNotShown()
    {

    }


    public void WatchAdForReward(string placement)
    {
        this._adsManager.RewardVideo(OnReward);
    }

    public virtual void OnReward()
    {   
        if(this.closeOnReward)
        {
            this.TogglePanel(false);
        }
    }
}
