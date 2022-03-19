using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{ 
    public float poolSize = 5f;
    public GameObject objectToPool;
    public Constant.GameEntity entity;
    private List<GameObject> pooledObjects = new List<GameObject>();

    private void Start()
    {
       for(int i=0;i<this.poolSize;i++)
        {
            GameObject G = (GameObject)Instantiate(this.objectToPool);
            this.PoolObject(G);
        }
    }

    public void Destroy(GameObject G)
    {
        this.PoolObject(G);
    }

    public bool IsPoolFor(GameObject G)
    {
        return this.objectToPool.Equals(G);
    }

    public GameObject Instantiate(Vector3 pos,Quaternion Rot)
    {
        GameObject G = this.GetObjectFromPool();
        G.SetActive(true);
        G.transform.position = pos;
        G.transform.rotation = Rot;

        return G;
    }

    private void PoolObject(GameObject G)
    {
        G.transform.position = Vector3.zero;
        G.transform.rotation = Quaternion.identity;
        G.SetActive(false);

        if (!pooledObjects.Contains(G))
            this.pooledObjects.Add(G);
    }

    private GameObject GetObjectFromPool()
    {
        if(this.PoolSize>0)
        {
            GameObject G = this.pooledObjects[0];
            this.pooledObjects.Remove(G);
            return G;
        }
        return null;
    }

    public int PoolSize
    {
        get
        {
            return this.pooledObjects.Count;
        }
    }
}
