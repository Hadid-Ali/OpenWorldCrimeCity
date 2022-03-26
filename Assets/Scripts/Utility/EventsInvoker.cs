using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventEntity
{
    public UnityEvent eventLookingToInvoke;
    public float timeToInvoke;
}


public class EventsInvoker : MonoBehaviour
{
    public List<EventEntity> eventsToInvoke;

    void Start()
    {
        for(int i=0;i<eventsToInvoke.Count;i++)
        {
            StartCoroutine(this.Coroutine_InvokeDelayedEvent(this.eventsToInvoke[i]));
        }
    }

    public IEnumerator Coroutine_InvokeDelayedEvent(EventEntity eventEntity)
    {
        yield return new WaitForSecondsRealtime(eventEntity.timeToInvoke);
        if (eventEntity.eventLookingToInvoke != null)
            eventEntity.eventLookingToInvoke.Invoke();
    }
}
