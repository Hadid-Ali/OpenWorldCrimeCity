using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private Transform _transform;
    private Transform _mainCameraTransform;

    public bool useDistantScale = false;

    [SerializeField]
    private float minScale = 0.3f;

    [SerializeField]
    private float  maxScale = 1.5f;


    [SerializeField]
    private float scaleDivider = 5f;


    private void Start()
    {
        this._transform = this.transform;
        this._mainCameraTransform = GameManager.instance.cameraManager._mainCamera.transform;

    }

    private void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(this._mainCameraTransform.position - this._transform.position);

        this._transform.eulerAngles = new Vector3(this._transform.eulerAngles.x, targetRotation.eulerAngles.y, this._transform.eulerAngles.z);

        if (!this.useDistantScale)
            return;


        //Assuming All Scales axis are Equal
        float scaleValue = this.minScale;

        scaleValue = Vector3.Distance(this._mainCameraTransform.position, this._transform.position)/this.scaleDivider;
        scaleValue = Mathf.Clamp(scaleValue, this.minScale, this.maxScale);

        this._transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
