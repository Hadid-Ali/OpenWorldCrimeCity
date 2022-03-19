using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : GameEntity
{
    public Rigidbody rb;
    public bool isPlayerVehicle = false;

    public float vehicleHealth = 100f,explosionForce =90f,disableTime = 1f;
    public GameObject vehicleDestroyedMesh,vehicleObject;


    public bool CanAccident
    {
        get
        {
            if (!this.GetComponent<Rigidbody>())
                return false;
            Rigidbody R = this.GetComponent<Rigidbody>();
            if (R.isKinematic)
                return false;

            return R.velocity.magnitude > 2f;

        }
    }

    public virtual void Start()
    {

    }

    public virtual void ApplyDamage(float damag)
    {
        this.vehicleHealth -= damag;
        if (this.vehicleHealth <= 0 & !this.isDestroy)
            this.DestroyVehicle();
    }

    bool isDestroy = false;
    public virtual void DestroyVehicle()
    {
        if (this.isDestroy)
            return;

        this.isDestroy = true;
        Invoke("ChangeVehicle", 0.1f);
        if (this.rb)
            this.rb.AddForce(this.transform.up * this.explosionForce, ForceMode.VelocityChange);
        Particles.Instance.ShowParticle(ParticleType.EXPLOSION, this.transform.position);
        if(this.disableTime>0)
        Invoke("OnDisableV", this.disableTime);
    }

    public void ChangeVehicle()
    {

        if (this.vehicleDestroyedMesh)
        {
            if (this.vehicleObject)
                this.vehicleObject.SetActive(false);
            this.vehicleDestroyedMesh.SetActive(true);
        }
    }

    private void OnDisableV()
    {
        this.gameObject.SetActive(false);
    }

    public void StrikePerson(CharacterController controller)
    {
        controller.KillWithForce(this.transform.forward+new Vector3(0f,0.4f,0.2f), 3000f);
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag(Constant.TAGS.SEA_TRIGGER))
        {
            Debug.LogError("Vehicle in see");
            if(col.gameObject.Equals(this.gameObject))
            {

            }
            this.DestroyVehicle();
        }
    }

    public virtual void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag(Constant.TAGS.PEDESTRIAN))
        {
            if (col.gameObject.GetComponent<PedestrianSystem.PedestrianController>())
            {

                if (this.rb)
                    if (!this.rb.isKinematic & Mathf.Abs(this.rb.velocity.magnitude) > 2f)
                    {

                        this.StrikePerson(col.gameObject.GetComponent<PedestrianSystem.PedestrianController>());
                    }
            }
        }
    }
}
