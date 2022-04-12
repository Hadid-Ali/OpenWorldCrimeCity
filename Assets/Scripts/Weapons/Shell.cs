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

    private Transform _SelfTransform;

    private void Awake()
    {
        this._SelfTransform = this.transform;
    }

    private void OnEnable()
    {
        if (!this.rb)
            this.rb = this.GetComponent<Rigidbody>();

        this.transform.eulerAngles = new Vector3(0f, GameManager.instance.playerController.EulerAngles.y, 0f);

        this.rb.AddForce(((this.transform.right * this.dirMulitple.x) + (this.transform.up * this.dirMulitple.y)) * this.force);

        Invoke("DestroyEntity", this.destroyTime);
    }

    private void OnDisable()
    {
        this.rb.velocity = this.rb.angularVelocity = Vector3.zero;
    }
}
