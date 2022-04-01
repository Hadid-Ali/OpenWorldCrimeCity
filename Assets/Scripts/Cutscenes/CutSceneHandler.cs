using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CutSceneHandler : MonoBehaviour
{
    public CutSceneData[] data;
    [Space]
    public Transform cameraTransform, secondCam;

    private IEnumerator routine;

    public delegate void OnCutSceneEnd();
    public static event OnCutSceneEnd OnCutSceneEnd_Event;

    public GameObject enableObjectAfterCutsceneEnd;

    private void Start()
    {
        routine = ShowCutScene();
        StartCoroutine(routine);
    }

    public void StopCutScene()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
    }

    private IEnumerator ShowCutScene()
    {
        int index = 0, dialogueIndex= 0;
        CutSceneData targetData;
        while (index < data.Length)
        {
            targetData = data[index];
            yield return new WaitForEndOfFrame();
            if (targetData.showSecondCamera && IsShowingSecondCamera())
            {
                continue;
            }
            if (targetData.hideSecondCamera && IsHidingSecondCamera())
            {
                continue;
            }
            if (!CheckIfCameraReached(targetData.targetPos.position))
            {
                if (targetData.moveCameraInstantly)
                {
                    cameraTransform.position = targetData.targetPos.position;
                    cameraTransform.rotation = targetData.targetPos.rotation;
                }
                else
                {
                    MoveCameraToTarget(targetData.targetPos, targetData.cameraMoveSpeed);
                }
            }
            else
            {
                if(dialogueIndex < targetData.dialoguesData.Length)
                {
                    var dialogueData = targetData.dialoguesData[dialogueIndex];
                    GameplayHUD.Instance.dialogueIcon.sprite = dialogueData.icon;
                    GameplayHUD.Instance.dialogueText.text = dialogueData.text;
                    GameplayHUD.Instance.dialoguesPanel.SetActive(true);
                    yield return new WaitForSecondsRealtime(dialogueData.delayForNext);
                    dialogueIndex ++;
                }
                else
                {
                    index++;
                    dialogueIndex = 0;
                    GameplayHUD.Instance.dialoguesPanel.SetActive(false);
                    yield return new WaitForSeconds(2);
                }
            }
        }

        routine = null;
        if (OnCutSceneEnd_Event != null)
        {
            OnCutSceneEnd_Event();
        }
        cameraTransform.gameObject.SetActive(false);
        if(enableObjectAfterCutsceneEnd)
            enableObjectAfterCutsceneEnd.SetActive(true);
    }

    private bool IsShowingSecondCamera()
    {
        Rect cam1 = cameraTransform.GetComponent<Camera>().rect;
        Rect cam2 = secondCam.GetComponent<Camera>().rect;
        if (cam1.x >= -0.5f)
        {
            cameraTransform.GetComponent<Camera>().rect = new Rect(cam1.x - 0.01f,0,1,1);
            secondCam.GetComponent<Camera>().rect = new Rect(cam2.x - 0.01f,0,1,1);
            return true;
        }
        return false;
    }

    private bool IsHidingSecondCamera()
    {
        Rect cam1 = cameraTransform.GetComponent<Camera>().rect;
        Rect cam2 = secondCam.GetComponent<Camera>().rect;
        if (cam1.x <= 0)
        {
            cameraTransform.GetComponent<Camera>().rect = new Rect(cam1.x + 0.01f, 0, 1, 1);
            secondCam.GetComponent<Camera>().rect = new Rect(cam2.x + 0.01f, 0, 1, 1);
            return true;
        }
        return false;
    }

    private void MoveCameraToTarget(Transform target,float speed)
    {
        cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, target.position, speed * Time.deltaTime);
        cameraTransform.eulerAngles = Vector3.MoveTowards(cameraTransform.eulerAngles, target.eulerAngles, 20 * speed * Time.deltaTime);
    }

    private bool CheckIfCameraReached(Vector3 target)
    {
        return Vector3.Distance(cameraTransform.position,target) < 0.01f;
    }
}

[System.Serializable]
public class CutSceneData
{
    public Transform targetPos;
    public bool moveCameraInstantly = false, showSecondCamera = false, hideSecondCamera = false;
    public float waitForDialogue, cameraMoveSpeed = 1;
    public DialogueData[] dialoguesData;
}

[System.Serializable]
public class DialogueData
{
    public Sprite icon;
    [TextArea]
    public string text;
    public AudioClip dialogueClip;
    public float delayForNext;
}