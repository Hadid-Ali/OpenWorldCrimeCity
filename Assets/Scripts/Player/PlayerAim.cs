using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform mainCamera;
    public Transform spine;
    

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButton(1))
        {
            Vector3 camAngle = UtilityMethods.ClampedAngle(this.mainCamera.eulerAngles);

            this.transform.eulerAngles = new Vector3(0, camAngle.y, 0);
            this.spine.transform.localEulerAngles = new Vector3(camAngle.x, 0, 0);

        }
    }
}
