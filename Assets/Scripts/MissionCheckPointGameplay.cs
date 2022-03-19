using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionCheckPointGameplay : Checkpoint
{
    public UnityEvent unityEvent;

    public override void OnCheckPointEntered()
    {
        if (this.unityEvent != null)
            this.unityEvent.Invoke();

        this.gameObject.SetActive(false);
    }

    public override void OnCheckPointExited()
    {
    }
}
