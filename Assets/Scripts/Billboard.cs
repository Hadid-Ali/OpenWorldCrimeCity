using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject mainCamera;

    public Vector3 offsetVector = new Vector3(0, 0, 0);

    private void Start()
    {
        this.mainCamera = GameManager.instance.mainCamera.gameObject;
    }

    private void Update()
    {
        this.transform.LookAt(this.mainCamera.transform);

        Vector3 vector = this.transform.eulerAngles;
        this.transform.eulerAngles = new Vector3(vector.x*this.offsetVector.x, vector.y * this.offsetVector.y, vector.z * this.offsetVector.z);
    }
}
