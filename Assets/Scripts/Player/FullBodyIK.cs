using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBodyIK : MonoBehaviour
{
    public Transform[] ikTargets;

    public float IKWeight;

    [SerializeField]
    private Animator anim;
    private CharacterController controller;
    [SerializeField]
    private bool applyIk = false;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<CharacterController>())
        {
            this.controller = this.GetComponent<CharacterController>();
            this.anim = this.controller.animatorController.Animator;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!this.applyIk)
            return;

        if(this.ikTargets[0])
        this.SetIKGoal(AvatarIKGoal.LeftHand, this.ikTargets[0], 1f, 1f);

        if(this.ikTargets[1])
        this.SetIKGoal(AvatarIKGoal.RightHand, this.ikTargets[1], 1f, 1f);

        if (this.ikTargets[2])
            this.SetIKHint(AvatarIKHint.LeftElbow, this.ikTargets[2], 1f);

        if (this.ikTargets[3])
            this.SetIKHint(AvatarIKHint.RightElbow, this.ikTargets[3], 1f);

        if (this.ikTargets[4])
        this.SetIKGoal(AvatarIKGoal.LeftFoot, this.ikTargets[4], 1f, 1f);
        
        if(this.ikTargets[5])
        this.SetIKGoal(AvatarIKGoal.RightFoot, this.ikTargets[5], 1f, 1f);

        if(this.ikTargets[6])
            this.SetIKHint(AvatarIKHint.LeftKnee, this.ikTargets[6], 1f);

        if(this.ikTargets[7])
            this.SetIKHint(AvatarIKHint.RightKnee, this.ikTargets[7], 1f);
    }

    public void SetIKTargets(params Transform [] ikTargets)
    {
        if (ikTargets.Length < 0)
            return;

        this.ikTargets = ikTargets;
        this.ToggleIK(true);
    }

    public void ToggleIK(bool b)
    {
        this.applyIk = b;
        if (!b)
            System.Array.Clear(this.ikTargets, 0, this.ikTargets.Length);
    }

    public void SetIKGoal(AvatarIKGoal ikPart, Transform target,float posWeight,float rotWeight)
    {
        this.anim.SetIKPositionWeight(ikPart, posWeight);
        this.anim.SetIKPosition(ikPart, target.position);

        this.anim.SetIKRotationWeight(ikPart, rotWeight);
        this.anim.SetIKRotation(ikPart, target.rotation);

    }

    public void SetIKHint(AvatarIKHint ikpart,Transform target,float posWeight)
    {
        this.anim.SetIKHintPositionWeight(ikpart, posWeight);
        this.anim.SetIKHintPosition(ikpart, target.position);
    }
}
