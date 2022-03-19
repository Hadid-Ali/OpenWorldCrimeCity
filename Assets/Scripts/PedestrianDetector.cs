using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianDetector : MonoBehaviour
{

    public Rigidbody rb;
    public List<GameObject> pedestrians = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log(other.gameObject);
        if(other.CompareTag(Constant.TAGS.PEDESTRIAN)) //gameObject.GetComponent<PedestrianController>())
        {
          //  Debug.LogError("PEdestrian Collision");
            this.rb.isKinematic = true;
            this.pedestrians.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAGS.PEDESTRIAN))
        {
            this.rb.isKinematic = false;
        }
    }
}
