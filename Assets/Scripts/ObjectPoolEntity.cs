using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolEntity : MonoBehaviour
{
    private ObjectPool _connectedPool;

    public void Init(ObjectPool parentPool)
    {
        this._connectedPool = parentPool;
    }

    public void DestroyEntity()
    {
        this._connectedPool.Destroy(this);
    }
}
