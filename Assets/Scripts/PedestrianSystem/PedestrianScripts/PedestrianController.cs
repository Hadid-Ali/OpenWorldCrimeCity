using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PedestrianSystem
{
    public class PedestrianController : NavigationAgentController
    {
        public Transform navigationTarget,previousNavigationTarget;

        public float navigationWait = 0.1f;
        public float normalizationtime = 2f;

        public LayerMask layer;
        public float checkRadius = 200f;

        public Collider[] points;

        public AttackingAgent attackBehaviour;

        public override void Start()
        {
            base.Start();
        }

        private void OnDisable()
        {
        }

        public void GetTargetWayPoint()
        {
            points = Physics.OverlapSphere(this.transform.position, this.checkRadius, this.layer);

            this.navigationTarget = this.points[Random.Range(0, Random.Range(0, this.points.Length))].transform;
            this.animatorController.SetSpeed(this.navigationSpeed / this.speedFactorForAnim);
        }

        public IEnumerator GetNextTargetPoint()
        {
            while(true)
            {
                this.previousNavigationTarget = this.navigationTarget;
                this.GetTargetWayPoint();
                if(this.previousNavigationTarget)
                { 
                    if(this.previousNavigationTarget != this.navigationTarget)
                    {
                        break;
                    }

                }
                yield return new WaitForSeconds(1f);
            }
        }

        public override void KillWithForce(Vector3 dir, float ragdForce)
        {
            base.KillWithForce(dir, ragdForce);
            GameManager.instance.pedestrianManager.RemovePedestrian(this);
            this.MissionEventOnKill();
        }

        public virtual void MissionEventOnKill()
        {
            if (GameManager.instance.currentMission.isPedestrianAttackMission)
            {
                GameManager.instance.currentMission.OnEnemyNeutralized();
            }
        }

        public void OnTargetPointReached()
        {
            Debug.LogError("On Target reached for " + this.gameObject);
            this.NormalizeNavigation();
            StartCoroutine(this.GetNextTargetPoint());
        }

        public override void OnEnable()
        {
            base.OnEnable();
            this.GetTargetWayPoint();
            StartCoroutine(this.NavigateToTarget());
        }

        public override void OnAttacked(float damage)
        {
            base.OnAttacked(damage);
            this.ReactionOnAttacked();
        }

        public virtual void ReactionOnAttacked()
        {
            this.StopNavigation();
            bool isMissionRestriction = false;
            if(GameManager.instance.currentMission)
            {
                isMissionRestriction = GameManager.instance.currentMission.isPedestrianAttackMission;
            }
            if (this.attackBehaviour!=null)
            {
                StopAllCoroutines();
                this.attackBehaviour.enabled = true;
                this.enabled = false;
                //Destroy(this);
                return;
            }

            this.animatorController.SetToggleScared(true);

            if(IsInvoking("ResumeNormalRoutine"))
            {
                CancelInvoke("ResumeNormalRoutine");
            }
            Invoke("ResumeNormalRoutine", this.normalizationtime);
        } 

        public virtual void ResumeNormalRoutine()
        {
            this.ResumeNavigation();
            this.animatorController.SetToggleScared(false);
            this.RunForNavigation(3f);
        }
        

        public void StartResumeNavigation(Transform target)
        {
            this.navigationTarget = target;
        }
        

        public float DistanceToTarget
        {
            get
            {
                return Vector3.Distance(this.navigationTarget.position, this.transform.position);
            }
        }


        public IEnumerator NavigateToTarget()
        {
            while (true)
            {
                if (this.navigationTarget)
                {
                    this.FollowTarget(this.navigationTarget);
                    if(this.DistanceToTarget<=1f)
                    {
                        this.OnTargetPointReached();
                    }
                }

                yield return new WaitForSeconds(this.navigationWait);
            }
        }
    }
}