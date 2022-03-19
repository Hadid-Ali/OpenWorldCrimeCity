using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAGS.PLAYER))
        {
            this.OnObjectCollected();
        }
    }

    public virtual void OnObjectCollected()
    {
        Debug.LogError("Object Collect");
        this.gameObject.SetActive(false);
    }
}
