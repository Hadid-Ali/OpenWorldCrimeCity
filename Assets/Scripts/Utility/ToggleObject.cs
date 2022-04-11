using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public bool toggle;

    void Start()
    {
        if (this.objectToToggle)
            this.objectToToggle.SetActive(this.toggle);
    }

}
