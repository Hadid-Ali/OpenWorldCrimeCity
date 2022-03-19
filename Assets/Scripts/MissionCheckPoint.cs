using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CheckPointType
{
    Mission_Start,
    Mission_Complete,
}

public class MissionCheckPoint : Checkpoint
{
    public CheckPointType checkPointType;
    public float waitBeforeMissionComplete;

    public override void OnCheckPointEntered()
    {
        switch (this.checkPointType)
        {
            case CheckPointType.Mission_Start:

                break;

            case CheckPointType.Mission_Complete:
                Debug.LogError("Mission ");
                Invoke("MissionComplete", this.waitBeforeMissionComplete);
                break;
        }

        this.gameObject.SetActive(false);
    }

    public override void OnCheckPointExited()
    {
        base.OnCheckPointExited();
    }


    public virtual void MissionComplete()
    {
        GameManager.instance.currentMission.MissionComplete();
    }
}
