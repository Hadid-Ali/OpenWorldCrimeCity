using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Enemy : WeaponAttackingAgent
{   
    public EnemyWaves wave;

    public UnityEvent eventOnBeingKilled;


   void StartEnemy()
    {

    }

    public void InstantAlert()
    {
        this.AssignTarget(GameManager.instance.playerController);
    }

    public override void OnAttacked(float damage,GameObject attacker)
    {
        if (this.wave)
        {
            this.wave.AlertWave();
        }
        base.OnAttacked(damage, attacker);
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
