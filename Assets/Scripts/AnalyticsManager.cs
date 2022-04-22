using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{

    public static void DesignEvent(string prefix,string eventData)
    {
        AppMetrica.Instance.ReportEvent(eventData);
    }
}
