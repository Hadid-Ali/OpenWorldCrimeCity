using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TextDataForCutScene
{
    public string text;
    public float time;
}

[System.Serializable]
public class UEventClass
{
    public UnityEvent unityEvent;
    public float timetoCall;

    public IEnumerator EventCall()
    {
        yield return new WaitForSeconds(this.timetoCall);
        if (this.unityEvent != null)
            this.unityEvent.Invoke();
    }
}


public class CutSceneCamera : DisableControls
{
    public CutSceneCamera nextCutSceneCamera;

    public float cutSceneDuration;

    public List<UEventClass> eventsOnStart, eventsOnEnd;

    public override void OnEnable()
    {
        base.OnEnable();

        for(int i=0;i<this.eventsOnStart.Count;i++)
        {
            StartCoroutine(this.eventsOnStart[i].EventCall());
        }

        if (this.cutSceneDuration > 0)
            Invoke("CompleteCutScene", this.cutSceneDuration);

        if(!this.GetComponent<Camera>())
        {
            GameManager.instance.playerController.ToggleCutSceneCamera(true, this.transform.position, this.transform.eulerAngles);
        }
    }

    public void CompleteCutScene()
    {
        for(int i=0;i<this.eventsOnEnd.Count;i++)
        {
            StartCoroutine(this.eventsOnEnd[i].EventCall());
        }
        GameManager.instance.mainCamera.lockCamera = false;
  //      GameManager.instance.gameplayHUD.TypeInstruction(this.endingText.text, this.endingText.time);
        if (!this.GetComponent<Camera>())
        {
            GameManager.instance.playerController.ToggleCutSceneCamera(false, this.transform.position, this.transform.eulerAngles);
        }
        Invoke("Disable", 0.1f);
    }

    void Disable()
    {

        this.gameObject.SetActive(false);
    }
    
    public IEnumerator ManipulateGameObject(GameObject g,float time,bool status)
    {
        yield return new WaitForSeconds(time);
        g.SetActive(status);
    }


    public override void OnDisable()
    {
        if(this.nextCutSceneCamera)
        {
            this.nextCutSceneCamera.gameObject.SetActive(true);
        }

        else
        {
            base.OnDisable();

        }
    }
}
