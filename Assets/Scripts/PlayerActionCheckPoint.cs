using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventWithDelay
{
    public UnityEvent eventToInvoke;
    public float durationBeforeInvoke;
}

public class PlayerActionCheckPoint : Checkpoint
{
    private PlayerCheckPointInterface checkPointEnteringPlayer;

    public string dataString;
    public CheckPointEventType checkPointType;

    [SerializeField]
    protected EventWithDelay eventWithDelay;

    public override void OnCheckPointEntered()
    {
        if (this.checkPointEnteringPlayer == null)
            this.checkPointEnteringPlayer = GameManager.instance.playerCheckPointHandler;

        this.checkPointEnteringPlayer.OnPlayerEnterCheckPOint(this.dataString, this.checkPointType);
        StartCoroutine(this.Coroutine_InvokeEvent(this.eventWithDelay));
    }

    protected IEnumerator Coroutine_InvokeEvent(EventWithDelay eventWithDelay)
    {
        yield return new WaitForSeconds(eventWithDelay.durationBeforeInvoke);
        eventWithDelay.eventToInvoke.Invoke();
    }
}
