using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    private Rigidbody rb;

    public float force;
    public Vector2 dirMulitple = new Vector2(1, 1);

    private void OnEnable()
    {
        if (!this.rb)
            this.rb = this.GetComponent<Rigidbody>();

        this.transform.localRotation = Quaternion.identity;
        this.rb.AddForce(((this.transform.right * this.dirMulitple.x) + (this.transform.up * this.dirMulitple.y)) * this.force);
    }
}
