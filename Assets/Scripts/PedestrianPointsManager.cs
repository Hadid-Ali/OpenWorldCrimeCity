using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianPointsManager : MonoBehaviour
{
    public List<PedestrianPoint> pedestrianPoints = new List<PedestrianPoint>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<this.transform.childCount;i++)
        {
            if (this.transform.GetChild(i).GetComponent <PedestrianPoint>())
            {
                this.pedestrianPoints.Add(this.transform.GetChild(i).GetComponent<PedestrianPoint>());
            }
        }
    }

    public PedestrianPoint GetNearestPedestrianPoint(GameObject distanceObject)
    {
        Vector3 pos = distanceObject.transform.position;
        
        PedestrianPoint nearestPoint = this.pedestrianPoints[0];
        float nearestDistance = Vector3.Distance(nearestPoint.transform.position, pos);

        for(int i=0;i<this.pedestrianPoints.Count-1;i++)
        {
            float distance = Vector3.Distance(pos, this.pedestrianPoints[i].transform.position);
            
            if(distance < nearestDistance)
            {
                nearestPoint = this.pedestrianPoints[i];
                nearestDistance = distance;
            }
        }

        return nearestPoint;
    }
}
