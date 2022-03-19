using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : GameEntity
{
    public float destroyTime = 3f;

    private Collider[] colliders;

    void Start()
    {
        Invoke("OnDestroy", this.destroyTime);
    }

    private void OnEnable()
    {
        this.colliders = this.GetComponentsInChildren<Collider>();

        for(int i=0;i<this.colliders.Length;i++)
        {
            if(this.colliders[i].isTrigger)
            {
                this.colliders[i].isTrigger = false;
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.ragdollsPool.DestroyRagdoll(this.gameEntityName, this.gameObject);
    }
}
