using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionScoring : MonoBehaviour
{
    public int requiredScoreToCallEvent;
    [SerializeField]
    int currentScore = 0;

    public UnityEvent unityEvent;

    public void IncreaseScore()
    {
        this.currentScore++;
        if(this.currentScore>=this.requiredScoreToCallEvent)
        {
            if (this.unityEvent != null)
                this.unityEvent.Invoke();
        }
    }
}
