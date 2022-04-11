using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NavigationBehaviour
{
    void AssignTarget(NavigationTarget navigationTarget);
}

public class HelicopterController : MonoBehaviour,NavigationBehaviour
{
    [SerializeField]
    protected float movementSpeed;

    [SerializeField]
    protected float rotationSpeed;

    public NavigationTarget currentFlightTarget;

    public float heightToStartFrom = 17f;

    protected Transform _flightTargetTransform;
    protected Transform _selfTransform;

    protected bool targetReached = false;

    private void Awake()
    {
        this._selfTransform = this.transform;
        this.SetTargetHeight();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        GameManager.instance.navigationBehaviour = this;
        this.OnHelicopterEnable();
    }

    private void OnDisable()
    {
        GameManager.instance.navigationBehaviour = null;
    }

    private void SetTargetHeight()
    {
        Vector3 currentPosition = this._selfTransform.position;
        this._selfTransform.position = new Vector3(currentPosition.x, this.heightToStartFrom, currentPosition.z);
    }

    public virtual void OnHelicopterEnable()
    {

    }

    public virtual void OnTargetReached()
    {
        Debug.LogError("Target Reached");
    }

    public virtual void OnAssignTarget()
    {

    }

    void NavigationBehaviour.AssignTarget(NavigationTarget navigationTarget)
    {
        this.currentFlightTarget = navigationTarget;
        this._flightTargetTransform = this.currentFlightTarget.transform;

        if (!this.HasReachedTarget)
            this.targetReached = false;

        this.OnAssignTarget();
    }


    private bool HasReachedTarget => Vector3.Distance(this._flightTargetTransform.position, this._selfTransform.position) <= 1f;

    private void Update()
    {
        if (!this.currentFlightTarget || this.HasReachedTarget)
        {
            if(this.currentFlightTarget)
            {
                if(!this.targetReached)
                {
                    this.targetReached = true;
                    this.OnTargetReached();
                }
            }
            return;
        }

        this._selfTransform.position = Vector3.MoveTowards(this._selfTransform.position, this._flightTargetTransform.position, this.movementSpeed * Time.deltaTime);
      
        this._selfTransform.rotation = Quaternion.Slerp(this._selfTransform.rotation,Quaternion.LookRotation(this._flightTargetTransform.position - this._selfTransform.position), this.rotationSpeed * Time.deltaTime);
    }

}
