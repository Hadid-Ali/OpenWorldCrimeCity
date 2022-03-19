using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingAttackingAgent : AttackingAgent
{   
    public Vector2 attackRange = new Vector2(1, 5);

    public override void OnEnable()
    {
    }

    public override void Start()
    {
        base.Start();
        this.animatorController.ToggleAttack(true);
        this.AssignTarget(GameManager.instance.playerController);
    }


    public override void OnStateChanged(Character_STATES state)
    {
        base.OnStateChanged(state);

        switch(state)
        {
            case Character_STATES.ATTACK:
                this.animatorController.ToggleAttack(true);
                break;

            default:
                this.animatorController.ToggleAttack(false);
                break;
        }

    }

    public override void Attack()
    {
        string attack = "Attack" + (int)Random.Range(this.attackRange.x, this.attackRange.y);
        this.animatorController.SetAttackRange(attack);
    }
}
