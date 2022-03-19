using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterSpawner : MonoBehaviour
{
    public float distance = 10f;
    public bool renderRadius = false;

    private void OnDrawGizmos()
    {
        if (!this.renderRadius)
            return;
        Gizmos.DrawSphere(this.transform.position, this.distance);
    }


    public Vector3 NearbyPositionAtLayer()
    {

        Vector3 sp = UnityEngine.Random.insideUnitSphere * this.distance;

        sp += this.transform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(sp, out hit, this.distance, NavMesh.GetAreaFromName(Constant.LAYERS.ENVTERRAIN));
        return hit.position;

    }
    public Vector3 PointNearCharacter()
    {

        Vector3 sp = UnityEngine.Random.insideUnitSphere * this.distance / 5;

        sp += this.transform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(sp, out hit, this.distance, NavMesh.GetAreaFromName(Constant.LAYERS.ENVTERRAIN));
        return hit.position;
    }

    #region CodeToComment
    public CharacterSpawnPoint[] points;

    public CharacterSpawnPoint GetSpawnPoint
    {
        get
        {
            for (int i = 0; i < this.points.Length; i++)
            {
                if (this.points[i].HitPoint != Vector3.zero & !this.points[i].isOccupied)
                {
                    if (i > 0 & this.points[i].isOccupied)
                        this.points[i].isOccupied = false;

                    return this.points[i];
                }
            }
            return null;
            //return this.NearbyPositionAtLayer();
        }
    }
    #endregion

}
