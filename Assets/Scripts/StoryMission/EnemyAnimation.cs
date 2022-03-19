using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    None = -90,
    RifleIdle = 0,
    PistolIdle = 1,
    Disappointed = 2,
    Thinking = 3,
    TerrifiedScar = 4,
    WalkingAnimation = 5,
    RunningAnimation = 6
   
}


public class EnemyAnimation : MonoBehaviour
{
    public EnemyState enemyState;
    public EnemyState layerState;
    public Animator anim;
    public float animSpeed = 1f;

    private void OnEnable()
    {
        this.anim.speed = this.animSpeed;
        this.SetState((int)this.enemyState);
        if((int)this.layerState >=0)
        {
            this.SetLayerState((int)this.layerState);
        }
    }

    public void SetLayerState(int i)
    {
        this.anim.SetInteger("LayerState", i);
    }

    public void SetState(int i)
    {
        this.anim.SetInteger("State", i);
    }
}
