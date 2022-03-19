using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PedestrianSystem
{
    public class PedestrianRemover : MonoBehaviour
    {   
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Constant.TAGS.PEDESTRIAN))
            {
                PedestrianController pedestrian = other.GetComponent<PedestrianController>();
                GameManager.instance.pedestrianManager.RemovePedestrian(pedestrian);
            }
        }
    }
}
