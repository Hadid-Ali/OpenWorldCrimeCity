using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EnemyAdvanced : Enemy
{   
    public UnityEvent onAlerted, onHalfHealth,onKilled;
    public float attackTime = 5f, defenseTime = 5f;

    [SerializeField]
    private float presentAttackTime = 0f;
    [SerializeField]
    private float presentDefenseTime = 0f;

    public GameObject objectsToShowOnDefense, holsterObjectsToShowOnDefense;

    private Coroutine routine;

    public override void AttackState()
    {
        base.AttackState();
    }


    public void ToggleDefense(bool b)
    {
        if (this.objectsToShowOnDefense)
            this.objectsToShowOnDefense.SetActive(b);

        if (this.holsterObjectsToShowOnDefense)
            this.holsterObjectsToShowOnDefense.SetActive(!b);


    }

    public override void OnStateChanged(Character_STATES state)
    {
        base.OnStateChanged(state);
        switch(state)
        {
            case Character_STATES.ATTACK:
                this.ToggleDefense(false);
                if (this.previouState == Character_STATES.DEFENSE & this.presentDefenseTime > 0)
                {
                    Debug.LogError("Defense");
                    this.SwitchState(Character_STATES.DEFENSE);
                }
               else
                {
                //    if (this.routine == null)
                //    {
                        if (this.presentAttackTime <= 0)
                            this.presentAttackTime = this.attackTime;
                        this.presentAttackTime = this.attackTime;
                        this.routine = StartCoroutine(this.AttackTimeRoutine());
                 //   }
                }

               
                break;

            case Character_STATES.DEFENSE:
              //  if (this.routine == null)
             //   {
                    if (this.presentDefenseTime <= 0)
                        this.presentDefenseTime = this.defenseTime;
                    this.routine = StartCoroutine(this.DefenseTimeRoutine());
            //    }
                this.ToggleDefense(true);
                break;

            case Character_STATES.CHASE:

                this.ToggleDefense(false);
                if (this.routine != null)
                    StopCoroutine(this.routine);

                break;
        }
    }

    public override void OnAttacked(float damage)
    {
        if (this.state == Character_STATES.DEFENSE)
            return;
        base.OnAttacked(damage);
    }

    public override void DefenseState()
    {
        base.DefenseState();
    }

    IEnumerator AttackTimeRoutine()
    {
        while (this.presentAttackTime > 0)
        {
            Debug.LogError("Attack Routine");
            yield return new WaitForSeconds(1f);
            this.presentAttackTime -= 1f;

            if (this.presentAttackTime <= 0)
            {
                this.presentDefenseTime = this.defenseTime;
                this.SwitchState(Character_STATES.DEFENSE);
            }

        }
    }

    IEnumerator DefenseTimeRoutine()
    {
        while (this.presentDefenseTime > 0)
        {
            Debug.LogError("Defense Routine");   
            yield return new WaitForSeconds(1f);
            this.presentDefenseTime -= 1f;

            if (this.presentDefenseTime <= 0)
            {
                this.presentAttackTime = this.attackTime;
                this.SwitchState(Character_STATES.ATTACK);
            }
        }
    }
}
