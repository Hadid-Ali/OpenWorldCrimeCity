using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void SetCameraShake()
    {
        this._animator.SetTrigger("Shake");
    }
}
