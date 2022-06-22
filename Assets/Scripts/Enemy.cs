using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : WeaponAttackingAgent
{   
    public EnemyWaves wave;

    public UnityEvent eventOnBeingKilled;
    public float destroyTime = 2f;

    public GameObject enemyIndicator;

    public GameObject healthBarParent;
    public Image healthBarImage;

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

        if (GameplayHUD.Instance.IsHeadShot)
        {
            GameplayHUD.Instance.IsHeadShot = false;
            damage = totalhealth;
        }

        base.OnAttacked(damage, attacker);

        if(this.healthBarParent)
        {
            if (!this.healthBarParent.activeSelf)
                this.healthBarParent.SetActive(true);

            if(this.healthBarImage)
            {
                this.healthBarImage.fillAmount = this.health / this.totalhealth;
            }
        }
    }

    public override void KillWithForce(Vector3 dir, float ragdForce)
    {
        if (this.isDead)
            return;

        base.KillWithForce(dir, ragdForce);

        GameManager.instance.totalKills++;

        if (this.wave)
            this.wave.WaveEnemyKilled();

        if (this.eventOnBeingKilled != null)
            this.eventOnBeingKilled.Invoke();

        if (this.enemyIndicator)
            this.enemyIndicator.SetActive(false);

        Destroy(this.gameObject,this.destroyTime);
    }
}
