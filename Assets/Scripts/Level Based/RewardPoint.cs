using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPoint : MonoBehaviour
{
    public int reward = 500;

    void OnEnable()
    {
        this.OnRewardEarn();
    }

    public virtual void OnRewardEarn()
    {
        GameManager.instance.EarnReward(this.reward, false);
    }
}
