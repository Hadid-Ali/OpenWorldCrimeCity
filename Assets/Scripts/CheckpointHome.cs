using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHome : Checkpoint
{
    public override void OnCheckPointEntered()
    {
        GameManager.instance.gameplayHUD.ToggleHomeControls(true);
    }

    public override void OnCheckPointExited()
    {
        GameManager.instance.gameplayHUD.ToggleHomeControls(false);
    }
}
