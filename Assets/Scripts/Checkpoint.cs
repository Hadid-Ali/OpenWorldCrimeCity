using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isLevelCheckPoint = false;
    [SerializeField]
    bool isEntered = false;

    public string expectedTag = Constant.TAGS.PLAYER;

    private void OnDisable()
    {
        this.OnCheckPointExited();
    }

    public virtual void OnCheckPointEntered()
    {
        GameManager.instance.gameplayHUD.ShowActionButton(this.Action);
    }

    public virtual void Action()
    {

    }


    public virtual void OnTriggerEnter(Collider col)
    {
        Debug.LogError(col.gameObject);

        bool isConditionMatched = col.CompareTag(this.expectedTag);

        if(!isConditionMatched)
        {
            if(col.transform.parent)
            {
                isConditionMatched = col.transform.parent.CompareTag(this.expectedTag);
            }
        }
        if (isConditionMatched)
        {
            if (isEntered)
                return;

            if (!isEntered)
                isEntered = true;
            this.OnCheckPointEntered();
        }
    }

    public virtual void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(Constant.TAGS.PLAYER))
        {
            if (isEntered)
                isEntered = false;
            this.OnCheckPointExited();
        }
    }

    public virtual void OnCheckPointExited()
    {
        GameManager.instance.gameplayHUD.HideActionButton();
    }
    
}
