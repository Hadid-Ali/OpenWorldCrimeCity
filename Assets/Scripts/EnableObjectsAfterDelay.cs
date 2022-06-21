using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectsAfterDelay : MonoBehaviour
{
    public GameObject[] ObjectsToEnable;
    public float[] enableTime;

    public GameObject[] ObjectsToDisable;
    public float[] disableTime;

    void Start()
    {
        for (int i = 0; i < ObjectsToEnable.Length; i++)
        {
            float t = 0;
            if (i < enableTime.Length)
            {
                t = enableTime[i];
            }
            StartCoroutine(EnablerDisabler(ObjectsToEnable[i], t, true));
        }

        for (int i = 0; i < ObjectsToDisable.Length; i++)
        {
            float t = 0;
            if (i < disableTime.Length)
            {
                t = disableTime[i];
            }
            StartCoroutine(EnablerDisabler(ObjectsToDisable[i], t, false));
        }
    }

    IEnumerator EnablerDisabler(GameObject ob, float time, bool enable)
    {
        yield return new WaitForSeconds(time);
        ob.SetActive(enable);
    }
}