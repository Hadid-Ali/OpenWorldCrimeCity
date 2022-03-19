using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnPoint : MonoBehaviour
{
    public LayerMask layersToTarget;
    public float rayDistance = 10f;

    public bool isOccupied = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, this.transform.forward * this.rayDistance);
    }

    public Vector3 HitPoint
    {
       get
        {
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, this.rayDistance, this.layersToTarget) & !this.isOccupied)
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }
}
