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


public class CutSceneCamera : MonoBehaviour
{
    public CutSceneCamera nextCutSceneCamera;

    public float cutSceneDuration;

    public List<TextDataForCutScene> cutSceneText;

    public TextDataForCutScene endingText;

    public List<UEventClass> eventsOnStart, eventsOnEnd;

    private void OnEnable()
    {
        GameManager.instance.mainCamera.gameObject.SetActive(false);
        GameManager.instance.gameplayHUD.gameplayControls.SetActive(false);
        for (int i=0;i<this.cutSceneText.Count;i++)
        {
            this.cutSceneDuration += this.cutSceneText[i].time;
        }

        GameManager.instance.mainCamera.gameObject.SetActive(false);

        for(int i=0;i<this.eventsOnStart.Count;i++)
        {
            StartCoroutine(this.eventsOnStart[i].EventCall());
        }

        Invoke("CompleteCutScene", this.cutSceneDuration);
        if (this.cutSceneText.Count > 0)
            GameManager.instance.gameplayHUD.ResetInstruction();
        StartCoroutine(this.TypeCutSceneText());

        if(!this.GetComponent<Camera>())
        {
            GameManager.instance.playerController.ToggleCutSceneCamera(true, this.transform.position, this.transform.eulerAngles);
        }
    }

    public IEnumerator TypeCutSceneText()
    {
        for(int i=0;i<this.cutSceneText.Count;i++)
        {
            GameManager.instance.gameplayHUD.TypeInstruction(this.cutSceneText[i].text,this.cutSceneText[i].time);
            yield return new WaitForSeconds(this.cutSceneText[i].time+1f);
        }
    }

    public void CompleteCutScene()
    {
        for(int i=0;i<this.eventsOnEnd.Count;i++)
        {
            StartCoroutine(this.eventsOnEnd[i].EventCall());
        }
        GameManager.instance.mainCamera.lockCamera = false;
        GameManager.instance.gameplayHUD.TypeInstruction(this.endingText.text, this.endingText.time);
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


    private void OnDisable()
    {
        if(this.nextCutSceneCamera)
        {
            this.nextCutSceneCamera.gameObject.SetActive(true);
        }

        else
        {

            GameManager.instance.mainCamera.gameObject.SetActive(true);
            GameManager.instance.gameplayHUD.gameplayControls.SetActive(true);
        }
    }
}
