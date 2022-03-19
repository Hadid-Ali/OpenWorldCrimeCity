using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class LevelTimer : MonoBehaviour
{
    public float timerInMinutes, timerInSeconds;
    public Text timerText;

    public UnityEvent onTimerComplete;

    private void OnEnable()
    {
        StartCoroutine(this.RoutineTimer());
    }

    public string TimerPrecssion(float time)
    {
        string timerPrecssion = time >= 10 ? time.ToString() : "0" + time.ToString();
        return timerPrecssion;
    }

    public bool IsTimerFinished
    {
        get
        {
            return this.timerInMinutes == 0 & this.timerInSeconds == 0;
        }
    }
    
    public IEnumerator RoutineTimer()
    {
        while(!this.IsTimerFinished)
        {
            this.timerText.text = this.TimerPrecssion(this.timerInMinutes) + ":" + this.TimerPrecssion(this.timerInSeconds);
            yield return new WaitForSeconds(1f);
            if(this.timerInSeconds>0)
            {
                this.timerInSeconds--;
            }

            else
            {
                this.timerInMinutes--;
                this.timerInSeconds = 59;
            }

            if(this.IsTimerFinished)
            {
                if(this.onTimerComplete!=null)
                {
                    this.onTimerComplete.Invoke();
                }
            }
        }
    }
}
