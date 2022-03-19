using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolEntity : GameObjectPool
{
    public Constant.GameEntity entityName;
}

public class EntitiesPool : MonoBehaviour
{
    public List<PoolEntity> ragdollEntities = new List<PoolEntity>();

    private void Start()
    {
        for (int i = 0; i < this.ragdollEntities.Count; i++)
        {
            this.ragdollEntities[i].Init();
        }
    }

    public virtual GameObject CreateEntity(Constant.GameEntity gameEntityName, Vector3 position, Quaternion rotation,CharacterDelegate cd =null)
    {
        PoolEntity entityPool = this.ragdollEntities.Find(x => x.entityName.Equals(gameEntityName));

        GameObject obj = entityPool.Instantiate(position, rotation);

        if(obj.GetComponent<CharacterController>())
        {
            obj.GetComponent<CharacterController>().OnCharacterKilled += cd;
        }

        return obj;
    }

    public virtual void DestroyEntity(Constant.GameEntity gameEntity, GameObject obj)
    {
        PoolEntity entityPool = this.ragdollEntities.Find(x => x.entityName.Equals(gameEntity));
        entityPool.Destroy(obj);
    }
}
