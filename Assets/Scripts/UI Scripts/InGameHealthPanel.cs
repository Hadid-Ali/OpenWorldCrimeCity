using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHealthPanel : InGameRewardPanel
{
    public override void OnReward()
    {
        base.OnReward();
        GameManager.instance.playerController.FIllHalfHealth();
    }
}
