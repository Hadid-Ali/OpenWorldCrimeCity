using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : ObjectPoolEntity
{

    private Rigidbody rb;

    public float force;
    public Vector2 dirMulitple = new Vector2(1, 1);

    [SerializeField]
    private float destroyTime = 1.2f;

    private void OnEnable()
    {
        if (!this.rb)
            this.rb = this.GetComponent<Rigidbody>();

        this.transform.localRotation = Quaternion.identity;
        this.rb.AddForce(((this.transform.right * this.dirMulitple.x) + (this.transform.up * this.dirMulitple.y)) * this.force);

        Invoke("DestroyEntity", this.destroyTime);
    }

    private void OnDisable()
    {
        this.rb.velocity = this.rb.angularVelocity = Vector3.zero;
    }
}
