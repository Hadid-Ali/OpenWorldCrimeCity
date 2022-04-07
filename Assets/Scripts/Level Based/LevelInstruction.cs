using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstruction : MonoBehaviour
{
    public int instructionIndex = 0;

    public float waitBeforeInstruction = 1f;

    private void OnEnable()
    {
        Invoke("GiveInstruction", this.waitBeforeInstruction);  
    }

    void GiveInstruction()
    {
        LevelData mission = this.GetComponentInParent<LevelData>();
        LevelInstructionObject instructionObject = mission.GetLevelInstruction(this.instructionIndex);

        if (instructionObject != null)
        {
            GameManager.instance.gameplayHUD.ShowNewInstruction(instructionObject.GetInstruction, instructionObject.GetReward);
        }
    }
}
