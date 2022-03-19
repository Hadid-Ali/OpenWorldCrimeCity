using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
      //  Debug.LogError(col.gameObject);
        if(col.CompareTag(Constant.TAGS.PEDESTRIAN))
        {
            PedestrianSystem.PedestrianController ped = col.GetComponent<PedestrianSystem.PedestrianController>();

            if(ped)
            {
                if(ped.navigationTarget.Equals(this.gameObject))
                {
                    ped.OnTargetPointReached();
                }
            }
        }
    }
}
