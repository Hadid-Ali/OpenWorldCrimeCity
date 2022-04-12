using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{ 
    public float poolSize = 5f;
    public ObjectPoolEntity objectToPool;
    public Constant.GameEntity entity;

    private List<ObjectPoolEntity> pooledObjects = new List<ObjectPoolEntity>();

    private void Start()
    {
       for(int i=0;i<this.poolSize;i++)
        {
            ObjectPoolEntity poolObject = (ObjectPoolEntity)Instantiate(this.objectToPool);
            poolObject.Init(this);
            this.PoolObject(poolObject);
        }
    }

    public void Destroy(ObjectPoolEntity G)
    {
        this.PoolObject(G);
    }

    public bool IsPoolFor(ObjectPoolEntity G)
    {
        return this.objectToPool.Equals(G);
    }

    public ObjectPoolEntity Instantiate(Vector3 pos,Quaternion Rot,Transform parent)
    {
        ObjectPoolEntity G = this.GetObjectFromPool();
        G.gameObject.SetActive(true);

        Transform shellTransform = G.transform;

        shellTransform.position = pos;
        shellTransform.rotation = Rot;

        return G;
    }

    private void PoolObject(ObjectPoolEntity G)
    {
        G.transform.position = Vector3.zero;
        G.transform.rotation = Quaternion.identity;
        G.gameObject.SetActive(false);

        if (!pooledObjects.Contains(G))
            this.pooledObjects.Add(G);
    }

    private ObjectPoolEntity GetObjectFromPool()
    {
        if(this.PoolSize>0)
        {
            ObjectPoolEntity G = this.pooledObjects[0];
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
