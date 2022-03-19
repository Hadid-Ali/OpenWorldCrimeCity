using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Enemy : WeaponAttackingAgent
{   

    public EnemyWaves wave;
    public bool waitForBeingAttacked = false;

    public UnityEvent eventOnBeingKilled;


   void StartEnemy()
    {

    }

    public override void OnEnable()
    {
        if(!this.waitForBeingAttacked)
        {
            base.OnEnable();
        }
    }

    public override void OnAttacked(float damage)
    {
        if (this.wave)
        {
            this.wave.AlertWave();
        }
        base.OnAttacked(damage);
    }

    public override void KillWithForce(Vector3 dir, float ragdForce)
    {
        base.KillWithForce(dir, ragdForce);

        if (this.wave)
            this.wave.WaveEnemyKilled();

        if (this.eventOnBeingKilled != null)
            this.eventOnBeingKilled.Invoke();
    }
}
