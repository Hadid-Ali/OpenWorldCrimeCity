using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public Color32 sphereColor;
    public float sphereRadius;

    public bool renderGizmos = true;

    private void OnDrawGizmos()
    {
        if (!this.renderGizmos)
            return;

        Gizmos.color = this.sphereColor;
        Gizmos.DrawSphere(this.transform.position, this.sphereRadius);
    }
}
