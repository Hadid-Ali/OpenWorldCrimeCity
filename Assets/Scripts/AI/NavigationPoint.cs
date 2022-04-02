using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class NavigationPoint : MonoBehaviour
{
    [SerializeField]
    private StartAnimationType animationOnReach = StartAnimationType.Idle;

    [SerializeField]
    private NavigationAgentController characterToAttract;

}
