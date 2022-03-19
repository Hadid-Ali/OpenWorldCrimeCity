using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilMethods : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static GameObject GetNearestPedestrianPoint(GameObject distanceObject,GameObject [] points)
    {
        Vector3 pos = distanceObject.transform.position;

        GameObject nearestPoint = points[0];
        float nearestDistance = Vector3.Distance(nearestPoint.transform.position, pos);

        for (int i = 0; i < points.Length - 1; i++)
        {
            float distance = Vector3.Distance(pos, points[i].transform.position);

            if (distance < nearestDistance)
            {
                nearestPoint = points[i];
                nearestDistance = distance;
            }
        }
        return nearestPoint;
    }
}
