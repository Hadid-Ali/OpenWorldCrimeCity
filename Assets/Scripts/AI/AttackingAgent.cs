using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartAnimationType
{
    Idle,
    Talking,
    TalkingOnPhone,
    Smoking,
}

public class AttackingAgent : NavigationAgentController
{
    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    protected CharacterController targetController;


    [SerializeField]
    private LayerMask attackingLayers;

    [SerializeField]
    protected SphereCollider _detectionCollider;

    public Constant.TAGS_ENUM targetTag;

    public Character_STATES state = Character_STATES.IDLE;
    public Character_STATES previouState;

    public float detectionRadius = 13f
        , attackDistance = 4f,
        attackRate = 0.3f,
        attackDamage = 20f,
        runSpeed = 2f;

    [Range(0.1f,2f)]
    private float detectionRoutineWait = 0.1f;

    private float attackMoment = 0f;
    private bool isInActive = true;

    [SerializeField]
    protected Transform _raycastLookPoint;

    private WaitForSeconds _waitForSeconds;

    [SerializeField]
    private StartAnimationType startingAnimation = StartAnimationType.Idle;

    [SerializeField]
    private GameObject objectToShowOnStart;

    public void ToggleStartAnimation(bool toggle)
    {
        this.animatorController.ToggleAnimation(this.startingAnimation.ToString(), toggle);

        if (this.objectToShowOnStart != null)
            this.objectToShowOnStart.SetActive(toggle);
    }

    public void OnValidate()
    {
        if (this._detectionCollider)
        {
            if (!this._detectionCollider.isTrigger)
                this._detectionCollider.isTrigger = true;

            this._detectionCollider.radius = this.detectionRadius;
        }
    }

    public void ToggleDetectionCollider(bool toggle)
    {
        this._detectionCollider.enabled = toggle;
    }

    public override void OnEnable()
    {
        if (this.targetObject != null)
            this.AssignTarget(this.targetObject);

        this.ToggleStartAnimation(true);
    }

    void ChasinSwitch()
    {
        this.StopAttack();
        this.SwitchState(Character_STATES.CHASE);
    }

    public virtual void AIStatesEngine()
    {
        switch (this.state)
        {
            case Character_STATES.CHASE:
                this.ChaseState();
                break;

            case Character_STATES.ATTACK:
                this.AttackState();
                break;

            case Character_STATES.DEFENSE:
                this.DefenseState();
                break;
        }
    }

    public override void KillWithForce(Vector3 dir, float ragdForce)
    {
        base.KillWithForce(dir, ragdForce);
        this.SwitchState(Character_STATES.DIE);
    }

    public virtual void DefenseState()
    {
        this.StopNavigationAndLookAtPlayer(2f);
    }

    public virtual void OnStateChanged(Character_STATES state)
    {

    }

    public virtual void DamageEnemy()
    {
        Particles.Instance.ShowParticle(ParticleType.HIT, this.targetObject.transform.position + Vector3.up * Random.Range(1f, 1.5f));
        this.targetController.OnAttacked(this.attackDamage, this.gameObject);
    }

    private void Update()
    {
        if (this.isInActive | this.isDead)
            return;

        this.AIStatesEngine();
    }

    public virtual void ChaseState()
    {
        if (this.IsTargetOnAttackingDistance())
        {
            this.SwitchState(Character_STATES.ATTACK);
            return;
        }

        if (!this.aiController.enabled)
            this.aiController.enabled = true;

        this.aiController.stoppingDistance = this.attackDistance;
        this.aiController.autoBraking = true;

        if (this.aiController.isStopped)
        {
            this.ResumeNavigation();
        }
        if (this.targetObject)
        {
            this.FollowTarget(this.targetObject);
        }
        this.RunForNavigation(this.runSpeed);

    }

    public void SwitchState(Character_STATES state)
    {
        this.previouState = this.state;
        this.state = state;
        this.OnStateChanged(state);
    }

    public virtual void Attack()
    {
        if (this.isHit)
            return;

        this.animatorController.Attack();
        //if(!IsInvoking("DamageEnemy"))
        //Invoke("DamageEnemy", this.damageWait);
    }

    public virtual void StopAttack()
    {

    }

    public virtual void StopNavigationAndLookAtPlayer(float wait)
    {
        this.StopNavigation();
        if (!this.IsTargetOnAttackingDistance())
        {
            Invoke("ChasinSwitch", wait);
            return;
        }

        this.RotateTo(this.targetObject.transform);
    }

    private bool IsNormal => this.state != Character_STATES.ATTACK | this.state != Character_STATES.CHASE;

    public virtual void OnTriggerEnter(Collider col)
    {
        if (this.targetController == null | this.IsNormal)
            this.OnObjectEnterAgentTrigger(col.gameObject);
    }

    public virtual void OnObjectEnterAgentTrigger(GameObject objectEnter)
    {
        if(objectEnter.CompareTag(this.targetTag.ToString()))
        {
            this.targetObject = objectEnter;
            this.ToggleDetectionCollider(false);
            StartCoroutine(this.Coroutine_FindTarget());
        }
    }

    public override void OnAttacked(float damage, GameObject attacker)
    {
        base.OnAttacked(damage, attacker);

        if(attacker.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            this.AssignTarget(controller);
        }
    }

    public virtual void AttackState()
    {
        this.StopNavigationAndLookAtPlayer(1f);
        if (Time.time > this.attackMoment)
        {
            Debug.LogError("AttackState " + this.gameObject);
            //attack
            this.Attack();
            this.attackMoment = Time.time + this.attackRate;
        }
    }



    public virtual void OnTargetFound()
    {
        this.ToggleDetectionCollider(false);
        if (this.IsTargetOnAttackingDistance())
        {
            this.StopNavigation();
            this.SwitchState(Character_STATES.ATTACK);
        }
        else
        {
            this.SwitchState(Character_STATES.CHASE);
        }
        this.isInActive = false;
        this.OnAgentAlert();
    }

    public virtual void OnAgentAlert()
    {
        this.ToggleStartAnimation(false);
    }

    public virtual void AssignTarget(CharacterController charController)
    {
        this.targetController = charController;
        this.targetObject = charController.gameObject;
        this.OnTargetFound();
    }

    public IEnumerator Coroutine_FindTarget()
    {
        this._waitForSeconds = new WaitForSeconds(this.detectionRoutineWait);
        this.state = Character_STATES.LOOKING_FOR_TARGET;
        bool isTargetFound = false;

        Transform targetTransform = this.targetObject.transform;
        while(!isTargetFound)
        {
            Ray ray = new Ray(this._raycastLookPoint.position, targetTransform.position - this._raycastLookPoint.position);

            if(Physics.Raycast(ray,out RaycastHit raycastHit,100f,this.attackingLayers))
            {
                if(raycastHit.transform.CompareTag(this.targetTag.ToString()))
                {
                    this.AssignTarget(raycastHit.transform.gameObject);
                    yield break;
                }
            }

            yield return this._waitForSeconds;
        }
        
    }

    public virtual void AssignTarget(GameObject targetGameObject)
    {
        this.targetObject = targetGameObject;
        this.AssignTarget(this.targetObject.GetComponent<CharacterController>());
        this.ToggleDetectionCollider(false);
    }

    public bool IsTargetOnAttackingDistance()
    {
        if (!this.targetObject)
            return false;

        return Vector3.Distance(this.transform.position, this.targetObject.transform.position) <= this.attackDistance;
    }
}

  
