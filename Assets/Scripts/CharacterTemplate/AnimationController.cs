using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _anim;

    public Animator Animator
    {
        get
        {
            if (!this._anim)
                this._anim = this.GetComponent<Animator>();
            return this._anim;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        this._anim = this.GetComponent<Animator>();
    }

    public void DieAnimate(bool toggleFlag)
    {
        this._anim.SetBool("IsDead",toggleFlag);
    }

    public void ReloadWeapon()
    {
        this._anim.SetTrigger("Reload");
    }

    public void SetSpeed(float speed)
    {
        this.Animator.SetFloat(Constant.ANIMATOR_STRINGS.SPEED, speed);
    }

    public void SetAim(bool b)
    {
        this.Animator.SetBool(Constant.ANIMATOR_STRINGS.AIM, b);
    }


    public int GetAnimatorKickCount()
    {
        return this._anim.GetInteger(Constant.ANIMATOR_STRINGS.KICK_COUNT);
    }

    public void SetAnimatorKickCount(int value)
    {
        this._anim.SetInteger(Constant.ANIMATOR_STRINGS.KICK_COUNT, value);
    }
    public void SetHitTaken()
    {
        this.Animator.SetTrigger(Constant.ANIMATOR_STRINGS.ISHIT);
    }

    public void SetToggleScared(bool b)
    {
        this.Animator.SetBool(Constant.ANIMATOR_STRINGS.SCARED, b);
    }

    public void SetKickTrigger(bool isSet = true)
    {
        if (isSet)
            this._anim.SetTrigger(Constant.ANIMATOR_STRINGS.KICK);
        else
            this._anim.ResetTrigger(Constant.ANIMATOR_STRINGS.KICK);
    }

    public int GetAnimatorPunchCount()
    {
        return this._anim.GetInteger(Constant.ANIMATOR_STRINGS.PUNCH_COUNT);
    }


    public void Attack()
    {
        this.Animator.SetTrigger(Constant.ANIMATOR_STRINGS.ATTACK);
    }

    public void SetPunchCount(int value)
    {
        //  Debug.LogError(value);
        this._anim.SetInteger(Constant.ANIMATOR_STRINGS.PUNCH_COUNT, value);
    }

    public void SetPunchTrigger(bool isSet = true)
    {
        if (isSet)
            this._anim.SetTrigger(Constant.ANIMATOR_STRINGS.PUNCH);
        else
            this._anim.ResetTrigger(Constant.ANIMATOR_STRINGS.PUNCH);
    }

    public void SetComboIndex(int index)
    {
        this._anim.SetInteger(Constant.ANIMATOR_STRINGS.COMBO_INDEX, index);
    }

    public void ToggleIsFighting(bool isToggle)
    {
        this._anim.SetBool(Constant.ANIMATOR_STRINGS.IS_FIGHTING, isToggle);
    }

    void FalseAllWeapons()
    {
        this._anim.SetBool(Constant.ANIMATOR_STRINGS.RIFLE, false);
        this._anim.SetBool(Constant.ANIMATOR_STRINGS.PISTOL, false);
        this._anim.SetBool(Constant.ANIMATOR_STRINGS.MELEE, false);
        this._anim.SetBool(Constant.ANIMATOR_STRINGS.EQUIP, false);
        this._anim.SetBool(Constant.ANIMATOR_STRINGS.AIM, false);
    }

    public void SetWeapon(WeaponType weaponType)
    {
        this.ToggleIsFighting(false);
        this.FalseAllWeapons();
        string weapon = "Is" + weaponType.ToString();
        switch(weapon)
        {
            case "IsNone":
                break;

            default:
                this.Animator.SetBool(weapon, true);
                this.Animator.SetBool(Constant.ANIMATOR_STRINGS.EQUIP, true);
                break;
        }
    }

    public void ToggleAttack(bool b)
    {
        this.Animator.SetBool(Constant.ANIMATOR_STRINGS.ATTACK, b);
    }

    public void SetTurn(float turn)
    {
        this.Animator.SetFloat(Constant.ANIMATOR_STRINGS.TURN, turn);
    }

    public Transform GetAnimatorBone(HumanBodyBones bone)
    {
        return this.Animator.GetBoneTransform(bone);
    }

    public void SetCrouch(bool b)
    {
        this.Animator.SetBool(Constant.ANIMATOR_STRINGS.CROUCH, b);
    }

    public void SetAttackRange(string t)
    {
        this.Animator.SetTrigger(t);
    }

    public void SetOnGround(bool b)
    {
        this.Animator.SetBool(Constant.ANIMATOR_STRINGS.ONGROUND, b);
    }
    
}
