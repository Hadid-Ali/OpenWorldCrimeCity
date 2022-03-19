using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsManager : MonoBehaviour
{
    public List<ObjectPool> pools;

    public ObjectPool FindPoolByObject(GameObject G)
    {
        foreach (ObjectPool p in this.pools)
        {
            if (p.objectToPool.Equals(G))
                return p;
        }

        return null;
    }

    public ObjectPool FindPoolByType(Constant.GameEntity entity)
    {
        foreach (ObjectPool p in this.pools)
        {
            if (p.entity.Equals(entity))
                return p;
        }

        return null;
    }

}