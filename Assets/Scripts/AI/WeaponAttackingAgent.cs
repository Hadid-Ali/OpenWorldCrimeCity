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
        try
        {
            this.animatorController.SetWeapon(this.weapon.weaponType);
        }
        catch(System.Exception e)
        {
            if (this.animatorController == null)
                Debug.LogError("Animator Exception");
            if (this.weapon == null)
                Debug.LogError("Weapon");

            Debug.LogError("Exception " + e);
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
        if (this.isInAttackPose)
        {
            this.isInAttackPose = false;
            this.animatorController.SetAim(false);
        }
    }
}
