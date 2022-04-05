using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackingAgent : AttackingAgent
{
    public Weapon weapon;
    public bool isInAttackPose = false;

    public override void Start()
    {
        base.Start();
    }

    public override void OnAgentAlert()
    {
        base.OnAgentAlert();
        if (this.weapon)
        {
            if (!this.weapon.gameObject.activeSelf)
                this.weapon.gameObject.SetActive(true);

            this.animatorController.SetWeapon(this.weapon.weaponType);
        }
    }

    public override void Attack()
    {
        if (this.weapon != null)
        {

            if (this.isHit)
                return;

            if (!this.isInAttackPose)
            {
                this.isInAttackPose = true;
                this.animatorController.SetAim(true);
            }

            this.weapon.Shoot();
            this.DamageEnemy();

        }
        else
        {
            base.Attack();
        }
    }

    public override void StopAttack()
    {
        Debug.LogError("Stop Attack");
        if (this.isInAttackPose)
        {
            this.isInAttackPose = false;
            this.animatorController.SetAim(false);
        }
    }
}
