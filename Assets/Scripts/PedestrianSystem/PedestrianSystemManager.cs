using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PedestrianSystem
{
    [System.Serializable]
    public class PedestrianWaypointHolder
    {
        public PedestrianWayPoint wayPoint;
        public bool isOccupied;
    }

    public class PedestrianSystemManager : MonoBehaviour
    {
       // public PedestrianPointsManager pedestrianPointsManager;

        public GameObject[] PedestrianPrefabs;

        public List<GameObject> activePedestrians;
        public List<GameObject> inActivePedestrians;

        public List<PedestrianWaypointHolder> waypoints;

        public int pedestrianPoolSize = 10;

        void Awake()
        {
            for (int i = 0; i < this.pedestrianPoolSize; i++)
            {
                GameObject P = (GameObject)Instantiate(this.PedestrianPrefabs[Random.Range(0, this.PedestrianPrefabs.Length)], Vector3.zero, Quaternion.identity);
                this.DeActivatePedestrian(P);
            }
        }

        public void RemovePedestrian(PedestrianController ped)
        {
            this.DeActivatePedestrian(ped.gameObject);
            GameManager.instance.pedestrianSpawner.pedestriansToSpawn++;
        }

        public void DeActivatePedestrian(GameObject G)
        {
            G.SetActive(false);
            this.inActivePedestrians.Add(G);
            if (this.activePedestrians.Contains(G))
                this.activePedestrians.Remove(G);
        }

        public IEnumerator SpawnPedestrian(float wait,Vector3 position)
        {
            yield return new WaitForSeconds(wait);
            this.SpawnPedestrian(position);
        }

        public void SpawnPedestrian(Vector3 v)
        {
            GameObject P = this.GetPedestrian();
            if (P)
            {
                P.transform.position = v;
              //  P.GetComponent<PedestrianController>().navigationTarget = this.pedestrianPointsManager.GetNearestPedestrianPoint(this.gameObject).transform;
                P.SetActive(true);
                this.activePedestrians.Add(P);
                
            }
        }

        public PedestrianWaypointHolder GetWayPoint()
        {
            PedestrianWaypointHolder waypoint= null;
            //for (int i= 0; i < this.waypoints.Count; i++)
            //{
            //    if(!this.waypoints[i].isOccupied)
            //    {
            //        waypoint = this.waypoints[i];
            //        break;
            //    }
            //}

            //if (waypoint.Equals(null))
            //{
                waypoint = this.waypoints[Random.Range(0, this.waypoints.Count)];
            //}

            return waypoint;
        }

        public GameObject GetPedestrian()
        {
            if (this.inActivePedestrians.Count > 0)
            {
                GameObject P = this.inActivePedestrians[0];
                this.inActivePedestrians.Remove(P);
                return P;
            }
            return null;
        }
    }
}