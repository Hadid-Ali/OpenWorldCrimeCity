using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstruction : MonoBehaviour
{
    public int instructionIndex = 0;

    private void OnEnable()
    {
        Mission mission = this.GetComponentInParent<Mission>();
        LevelInstructionObject instructionObject = mission.GetLevelInstruction(this.instructionIndex);

        if (instructionObject != null)
        {
            GameManager.instance.gameplayHUD.ShowNewInstruction(instructionObject.GetInstruction, instructionObject.GetReward);
        }
    }
}
