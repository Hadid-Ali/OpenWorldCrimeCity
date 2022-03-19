using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollsPool : EntitiesPool
{
    public GameObject CreateRagdoll(Constant.GameEntity gameEntityName, Vector3 position, Quaternion rotation)
    {
        return this.CreateEntity(gameEntityName, position, rotation);
    }

    public void DestroyRagdoll(Constant.GameEntity gameEntity, GameObject obj)
    {
        this.DestroyEntity(gameEntity, obj);
    }
}
