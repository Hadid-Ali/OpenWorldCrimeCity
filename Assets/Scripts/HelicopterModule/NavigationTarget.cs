using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTarget : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.instance.AssignNavigationTarget(this);
    }
}
