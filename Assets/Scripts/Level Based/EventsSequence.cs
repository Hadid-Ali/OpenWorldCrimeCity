using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsSequence : MonoBehaviour
{
    [SerializeField]
    protected List<EventWithDelay> eventsWithDelay;

    private void OnEnable()
    {
        for(int i=0;i<this.eventsWithDelay.Count;i++)
        {
            StartCoroutine(UtilityMethods.Coroutine_InvokeEvent(this.eventsWithDelay[i]));
        }
    }
}
