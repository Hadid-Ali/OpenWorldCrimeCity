using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingAgent : NavigationAgentController
{
    public GameObject targetObject;
    public CharacterController targetController;

    [SerializeField]
    List<List<GameObject>> listContainingLists;
    [SerializeField]
    Collider[] detectedColliders;

    public LayerMask attackingLayers;
    public LayerMask[] priorityBasedLayerMask;

    public Character_STATES state = Character_STATES.IDLE;
    public Character_STATES previouState;

    public float detectionRadius = 13f
        , attackDistance = 4f,
        attackRate = 0.3f,
        attackDamage = 20f,
        runSpeed = 2f;
    private float attackMoment = 0f;
    private bool isInActive = true;

    public override void OnEnable()
    {
        //this.FindTarget();
        Invoke("FindTarget", 1f);
    }

    public void OnDisable()
    {
        if (IsInvoking("FindTarget"))
            CancelInvoke("FindTarget");
    }

    void ChasinSwitch()
    {
        this.StopAttack();
        this.SwitchState(Character_STATES.CHASE);
    }

    [ContextMenu("Alert Enemy")]
    public virtual void InstantAlert()
    {
        if (this.targetController == null)
            this.AssignTarget(GameManager.instance.playerController);
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
        if (!this.targetController)
        {
            this.SwitchState(Character_STATES.LOOKING_FOR_TARGET);
            this.FindTarget();
            return;
        }
        Particles.Instance.ShowParticle(ParticleType.HIT, this.targetObject.transform.position + Vector3.up * Random.Range(1f, 1.5f));
        this.targetController.OnAttacked(this.attackDamage);
    }

    private void Update()
    {
        if (this.isInActive)
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



    public virtual void AttackState()
    {
    
        #region CachedCode
        /*
        this.StopNavigation();
        if (!this.IsTargetOnAttackingDistance())
        {
            this.StopAttack();
            this.SwitchState(Character_STATES.CHASE);
            return;
        }

        this.RotateTo(this.targetObject.transform);
        */
        #endregion
        this.StopNavigationAndLookAtPlayer(1f);
        if (Time.time>this.attackMoment)
        {
            Debug.LogError("AttackState "+this.gameObject);
            //attack
            this.Attack();
            this.attackMoment = Time.time + this.attackRate;
        }
    }



    public virtual void OnTargetFound()
    {
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
    }

    public virtual void AssignTarget(CharacterController charController)
    {
        this.targetController = charController;
        this.targetObject = charController.gameObject;
        this.OnTargetFound();
    }

    public virtual void OnNoTargetFound()
    {
        Vector3 v = Vector3.zero;
        if (!this.targetObject)
            v = GameManager.instance.playerController.playerSpawner.PointNearCharacter();
        else
        if (this.targetController.GetComponent<CharacterSpawner>())
            this.targetController.GetComponent<CharacterSpawner>().PointNearCharacter();

        if (v != Vector3.zero)
            this.NavigateToPoint(v);

        if (!IsInvoking("FindTarget"))
            Invoke("FindTarget", 0.4f);
        
    }


    public virtual void FindTarget()
    {
        this.state = Character_STATES.LOOKING_FOR_TARGET;

        this.animatorController.SetSpeed(0);
        this.animatorController.SetTurn(0);

        detectedColliders = Physics.OverlapSphere(this.transform.position, this.detectionRadius, this.attackingLayers);

        bool istargetFound = false;
        listContainingLists = new List<List<GameObject>>();

        for(int i=0;i<this.priorityBasedLayerMask.Length;i++)
        {
            listContainingLists.Add(this.ObjectsInLayer(detectedColliders, this.priorityBasedLayerMask[i]));
        }


        for (int i = 0; i <listContainingLists.Count;i++)
        {
            if(listContainingLists[i].Count>0)
            {
                istargetFound = true;
                this.targetObject = this.listContainingLists[i][Random.Range(0, this.listContainingLists[i].Count)];
                break;
            }
        }

        if(this.targetObject)
            if(this.targetObject.GetComponent<CharacterController>())
            {
                this.targetController = this.targetObject.GetComponent<CharacterController>();
            }

        if (istargetFound)
            this.OnTargetFound();

        else
            this.OnNoTargetFound();


    }
    
    public bool IsTargetOnAttackingDistance()
    {
        if (!this.targetObject)
            return false;

        return Vector3.Distance(this.transform.position, this.targetObject.transform.position) <= this.attackDistance;
    }

    public List<GameObject> ObjectsInLayer(Collider [] objects,LayerMask layer)
    {
        List<GameObject> G = new List<GameObject>();

        for(int i=0;i<objects.Length;i++)
        {
            if ((layer & (1 << objects[i].gameObject.layer)) != 0)
            {
             //   Debug.LogError(objects[i].gameObject);
                G.Add(objects[i].gameObject);
            }
        }

        return G;            
    }
}
