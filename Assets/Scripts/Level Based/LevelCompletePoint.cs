using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletePoint : RewardPoint
{
    public float waitBeforeLevelComplete = 3f;

    public override void OnRewardEarn()
    {
        GameManager.instance.EarnReward(this.reward, true,this.waitBeforeLevelComplete);
    }
}
