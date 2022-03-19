using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class GameObjectPool
{
    [HideInInspector]
    public string name;
    public int poolSize = 5;
    public GameObject objectToPool;
  //  [SerializeField]
    private List<GameObject> pooledObjects = new List<GameObject>();

    public void Init()
    {
        for (int i = 0; i < this.poolSize; i++)
        {
            GameObject G = (GameObject)MonoBehaviour.Instantiate(this.objectToPool);
            this.PoolObject(G);
        }
    }

    public virtual void Destroy(GameObject G)
    {
        this.PoolObject(G);
    }

    public bool IsPoolFor(GameObject G)
    {
        return this.objectToPool.Equals(G);
    }

    public virtual GameObject Instantiate(Vector3 pos, Quaternion Rot)
    {
        GameObject G = this.GetObjectFromPool();
        G.transform.position = pos;
        G.transform.rotation = Rot;
        G.SetActive(true);

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
        if (this.PoolSize > 0)
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
