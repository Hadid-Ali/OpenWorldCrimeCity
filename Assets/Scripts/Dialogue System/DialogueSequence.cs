using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class SimpleDialogue
{
    [Multiline,TextArea(3,10)]
    public string dialogueString;
    public AudioClip dialogueClip;

    public float dialoguePitch = 0.9f;
    public float timeForDialogueToWrite = 2f;

    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;

    public void EnableAndDisableObjects()
    {
        if (this.objectsToEnable.Length > 0)
        {
            for (int i = 0; i < this.objectsToEnable.Length; i++)
            {
                this.objectsToEnable[i].SetActive(true);
            }
        }

        if (this.objectsToDisable.Length > 0)
        {
            for (int i = 0; i < this.objectsToDisable.Length; i++)
            {
                this.objectsToDisable[i].SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class DialogueObject : SimpleDialogue
{
    public CharacterIdentity dialogueNarrator;
}

public class DialogueSequence : MonoBehaviour
{
    public List<DialogueObject> sequenceDialogues;

    public float waitBeforeDialogues = 0f;

    private int index = 0;

    public bool isPhoneCall = true;
    public bool isLevelStartSequence;

    public UnityEvent eventOnSequenceComplete;

    public bool shouldEnableControlsAfter = true;

    private DialogueSystemManager dialogueSystemManager;

    public Transform playerPoint;

    private void Start()
    {
        if (this.dialogueSystemManager == null)
            this.dialogueSystemManager = DialogueSystemManager.instance;
    }

    private void OnEnable()
    {
        Invoke("StartDialogueSequence", this.waitBeforeDialogues);
    }

    public void StartDialogueSequence()
    {
        if(this.playerPoint)
        {
            GameManager.instance.playerController.SituatePlayerAt(this.playerPoint);
        }

        this.dialogueSystemManager.ToggleDialogueCanvas(true, false);
        this.dialogueSystemManager.SetupSequence(this);
    }

    private void OnDisable()
    {
        this.eventOnSequenceComplete.Invoke();
        this.dialogueSystemManager.ToggleDialogueCanvas(false,this.shouldEnableControlsAfter);

        if(this.isLevelStartSequence)
        {
            GameManager.instance.currentMission.PositionPlayerForMission();
            GameManager.instance.gameplayHUD.ToggleGameplayControls(true);
        }
    }
}
