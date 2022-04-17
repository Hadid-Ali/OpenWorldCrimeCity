using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRewardPanel : InGamePanel
{
    protected AdsManager _adsManager;

    [SerializeField]
    protected bool closeOnReward = true;

    void Awake()
    {
        this._adsManager = AdsManager.Instance;
    }

    public virtual void OnShowRewardedVideo()
    {

    }

    public virtual void OnRewardedVideoNotShown()
    {

    }

    public void WatchAdForReward(string placement)
    {
        this._adsManager.ShowRewardedVideo(OnReward, this.OnShowRewardedVideo, this.OnRewardedVideoNotShown, placement);
    }

    public virtual void OnReward()
    {   
        if(this.closeOnReward)
        {
            this.TogglePanel(false);
        }
    }
}
