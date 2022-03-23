using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationAgentController : CharacterController
{
    public NavMeshAgent aiController;

    public float navigationSpeed = 2f;
    public float speedFactorForAnim = 3f;  
    
    public float iterationsLimitForTarget = 4;

    public override void OnEnable()
    {
        base.OnEnable();

        this.aiController = this.GetComponent<NavMeshAgent>();
        this.aiController.speed = this.navigationSpeed;
    }

    public void FollowTarget(GameObject G)
    {
        this.FollowTarget(G.transform);
    }

    public void RunForNavigation(float speed = 1f)
    {
        this.animatorController.SetSpeed(1f);
        this.aiController.speed = speed*this.navigationSpeed;
    }

    public void NormalizeNavigation()
    {
        this.animatorController.SetSpeed(this.navigationSpeed / this.speedFactorForAnim);
        this.aiController.speed = this.navigationSpeed;
    }

    public override void KillWithForce(Vector3 dir, float ragdForce)
    {
        base.KillWithForce(dir, ragdForce);
        this.aiController.enabled = false;
    }
    public void StopNavigation()
    {
        if(this.aiController.enabled)
        {
            this.aiController.isStopped = true;
            this.aiController.enabled = false;
        }
        this.animatorController.SetSpeed(0f);
    }

    private void OnDisable()
    {
        
    }

    public void NavigateToPoint(Vector3 v)
    {
        if (!this.aiController.enabled)
            this.aiController.enabled = true;

        this.aiController.SetDestination(v);
        this.ResumeNavigation();

        this.animatorController.SetSpeed(this.navigationSpeed / this.speedFactorForAnim);
    }

    public void ResumeNavigation()
    {
        if (!this.aiController.enabled)
            this.aiController.enabled = true;
        this.aiController.isStopped = false;
        this.aiController.speed = this.navigationSpeed;
    }
    

    void ResetIsHit()
    {
        if (this.isHit)
            this.isHit = false;
    }

    public override void OnAttacked(float damage)
    {
        this.animatorController.SetHitTaken();
        this.isHit = true;
        Invoke("ResetIsHit", 1f);
        base.OnAttacked(damage);
    }
    

    public void FollowTarget(Transform target)
    {
        if (!target)
            return;
        
        if (this.aiController.enabled)
            if (!this.aiController.isStopped)
            this.aiController.SetDestination(target.position);
    }
}
