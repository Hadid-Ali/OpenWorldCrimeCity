using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class SimpleDialogue
{
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
    public CharacterIdentity narratorA, narratorB;

    public float waitBeforeDialogues = 0f;

    private int index = 0;

    public UnityEvent eventOnSequenceComplete;

    public bool isLevelStartSequence;
    public bool shouldEnableControlsAfter = true;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Invoke("StartDialogueSequence", this.waitBeforeDialogues);
    }

    public void StartDialogueSequence()
    {
        DialoguesCanvas.instance.ToggleDialogueCanvas(true, false);
        DialoguesCanvas.instance.SetupSequence(this);
    }

    private void OnDisable()
    {
        this.eventOnSequenceComplete.Invoke();
        DialoguesCanvas.instance.ToggleDialogueCanvas(false,this.shouldEnableControlsAfter);

        if (this.isLevelStartSequence)
            GameManager.instance.InitGameplay();
    }
}
