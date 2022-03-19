using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PedestrianSystem
{
    public class PedestrianSpawner : MonoBehaviour
    {
        public float radius = 95f;

        public int pedestrianCount = 7;
        public int pedestriansToSpawn = 7;

        public float waitBeforePedestrianSpawn = 1f;

        public float unspawnedCalls = 0f;
        public float singleGoIterations = 3;

        [SerializeField]
        private float hitDistane = 1f;

        void Start()
        {
        }

        public IEnumerator SpawnPedestrians(float number,float wait)
        {
            yield return new WaitForSeconds(wait);
            this.unspawnedCalls += number;
            for (int i = 0; i < this.pedestrianCount; i++)
            {
                Vector3 pos;
                bool gotPosition = this.RandomPoint(out pos);
                if (gotPosition)
                {
                    if (this.unspawnedCalls > 0)
                        unspawnedCalls--;
                    GameManager.instance.pedestrianManager.SpawnPedestrian(pos);
                    yield return new WaitForSeconds(wait);
                    //StartCoroutine(GameManager.instance.pedestrianManager.SpawnPedestrian(this.waitBeforePedestrianSpawn, pos));
                }
            }
            if (this.unspawnedCalls > 0)
            {
                if(this.singleGoIterations<3)
                {
                    this.singleGoIterations++;
                    StartCoroutine(this.SpawnPedestrians(this.unspawnedCalls, wait));
                }
                else
                {
                    this.singleGoIterations = 0;
                }
            }
            else
            {
                this.singleGoIterations = 0;
            }
            yield return null;
        }

        public bool RandomPoint(out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * this.radius;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, this.hitDistane, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }

        [SerializeField]
        private List<GameObject> points = new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(Constant.TAGS.PEDESTRIANPOINT))
            {
                if(this.pedestriansToSpawn > 0)
                {
                    this.points.Add(other.transform.gameObject);
                    GameManager.instance.pedestrianManager.SpawnPedestrian(other.transform.position);
                    this.pedestriansToSpawn--;
                    other.gameObject.SetActive(false);
                    if(this.points.Count>=this.pedestriansToSpawn)
                    {
                        for(int i=0;i<this.points.Count;i++)
                        {
                            this.points[i].gameObject.SetActive(true);
                        }

                        //this.points.Clear();
                    }
                }
            }
        }
    }
}