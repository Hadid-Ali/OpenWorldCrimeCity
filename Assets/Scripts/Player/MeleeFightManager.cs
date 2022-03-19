using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFightManager : MonoBehaviour
{
    private PlayerController playerController;

    public LayerMask layerMask;
    public Transform fightRaycaster;

    public float fightDamageDistance;
    public float attackDamage = 5f;

    public bool canMeleeFight = true;

    // Use this for initialization
    void Start()
    {
        this.playerController = this.GetComponent<PlayerController>();
    }

    public void KickAttack()
    {

    }

    public void KickAnimationEvent(int i)
    {
        if (i == 0)
            this.SubtractKickCount();
        else if (i == 1)
            this.PostKickMethod();
        this.ApplyMeleeDamage();
    }

    public void SubtractKickCount()
    {

    }

    public void PostKickMethod()
    {

    }

    public void RestoreRootMotionComponent()
    {
        this.playerController.animatorController.Animator.applyRootMotion = true;
    }

    public void HalterRootMotionComponent()
    {
        this.playerController.animatorController.Animator.applyRootMotion = false;
    }

    void PostKick()
    {

    }

    public void ApplyMeleeDamage()
    {
        Vector3 impactPoint = Vector3.zero;

        CharacterController character = null;
        float damage = this.attackDamage ;

        bool hasEnemyNearby = false;

        if (this.playerController.nearByEnemy)
        {
            if (Vector3.Distance(this.playerController.nearByEnemy.transform.position,this.transform.position) <= this.fightDamageDistance + 1)
            {
                hasEnemyNearby = true;
            }
        }

        if (hasEnemyNearby)
        {
            character = this.playerController.nearByEnemy.GetComponent<CharacterController>();
            impactPoint = character.transform.position + new Vector3(0f, 1f, 0f);
        }

        else
        {
            RaycastHit hit;
            if (Physics.Raycast(this.fightRaycaster.position, this.fightRaycaster.transform.forward, out hit, this.fightDamageDistance, this.layerMask))
            {
                Debug.LogError(hit.transform.gameObject);

                character = hit.transform.gameObject.GetComponent<CharacterController>();
                impactPoint = hit.point;
            }
        }

        if(character)
        {
            Debug.LogError("Apply Melee Damage");
            SoundManager.instance.PlaySound(SoundType.MELEE_PUNCH_KICK);
            character.OnAttacked(this.attackDamage);
            Particles.Instance.ShowParticle(ParticleType.HIT, impactPoint);
        }
    }

    public void PunchAnimatorMethod(int i)
    {
        if (i == 0)
            this.SubractPunchCount();
        else if (i == 1)
            this.PostPunchMethod();
        this.ApplyMeleeDamage();
    }
    public void RotateToNearybyEnemy(GameObject overrIdeObject = null)
    {
        if (!this.playerController.nearByEnemy)
        {
            this.playerController.nearbyEnemyLocator.GetNextTarget();
        }

        if (this.playerController.nearByEnemy)
        {
            //if (Physics.Raycast(this.fightRaycaster.position, this.fightRaycaster.transform.forward, out RaycastHit hit, this.fightDamageDistance, this.layerMask))
            //{
            //    CharacterController npc;
            //    if (hit.transform.gameObject.TryGetComponent<CharacterController>(out npc))
            //    {
            //        this.playerController.nearByEnemy = npc;
            //    }
            //}

            if (this.DistanceFrom(this.playerController.nearByEnemy.gameObject) <= this.fightDamageDistance)
            {
                //this.playerController.characterController.ToggleJoyStickRotation(false);
                this.playerController.RotateTo(this.playerController.nearByEnemy.gameObject);
            }

            else
            {
                //this.playerController.characterController.ToggleJoyStickRotation(true);
            }
        }
        else
        {
          //  this.playerController.characterController.ToggleJoyStickRotation(true);
        }

    }
    public float DistanceFrom(GameObject tObject) => Vector3.Distance(tObject.transform.position, this.transform.position);

    public void PunchAttack()
    {
        if (!this.canMeleeFight)
            return;

        this.playerController.animatorController.ToggleIsFighting(true);
        this.playerController.animatorController.Animator.applyRootMotion = false;
        this.playerController.RotateToNearbyEnemy();

        if (this.playerController.animatorController.GetAnimatorPunchCount() < 1)
        {
            this.playerController.animatorController.SetComboIndex(Random.Range(0, 2));
            this.playerController.animatorController.SetPunchTrigger();
        }

        this.playerController.animatorController.SetPunchCount(this.playerController.animatorController.GetAnimatorPunchCount() + 1);
    }

    public void SubractPunchCount()
    {
        if (this.playerController.animatorController.GetAnimatorPunchCount() > 0)
        {
            this.playerController.animatorController.SetPunchCount(this.playerController.animatorController.GetAnimatorPunchCount() - 1);
        }

        this.playerController.animatorController.SetPunchTrigger(false);
    }

    public void PostPunchMethod()
    {
        this.playerController.animatorController.SetPunchTrigger(false);
        Invoke("RestoreRootMotionComponent", 0.5f);

        Invoke("PostPunch", 0.1f);
    }

    void PostPunch()
    {
        if (this.playerController.animatorController.GetAnimatorPunchCount() > 0)
            this.playerController.animatorController.SetPunchCount(0);
    }

}

