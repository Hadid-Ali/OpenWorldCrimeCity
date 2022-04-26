using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemManager : MonoBehaviour
{
    public static DialogueSystemManager instance;

    public GameObject dialoguesCanvas;

    public GameObject nextDialogueIcon;
    public GameObject dialogueInputObject;

    public GameObject phoneCallIconObject;

    private DialogueSequence currentDialogueSequence;
    private int currentDIalogueIndex = 0;

    private DialogueObject currentDialogue;

    [SerializeField]
    private Text dialogueText;

    private bool isDialogueSequenceInProcess;

    [SerializeField]
    private AudioSource dialoguesAudioSource;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
    }

    public void OnDialogueWritten()
    {
        this.nextDialogueIcon.SetActive(true);
    }

    public void DialogueInput()
    {
        if(SoundManager.instance)
        SoundManager.instance.PlaySound(SoundType.CLICK_SOUND); 

        if (this.dialoguesAudioSource.isPlaying)
            this.dialoguesAudioSource.Stop();
        
        if(GameManager.instance.gameplayHUD.isTextWriting)
        {
            GameManager.instance.gameplayHUD.StopTextWriting();
            this.dialogueText.text = this.currentDialogue.dialogueString;
        }
        else
        {
            this.ProceedToNextDialogue();
        }
    }

    private void CompleteSequence()
    {
        GameManager.instance.OnDialogueSequenceEnd(this.currentDialogueSequence.canShowAdAfter);
        this.currentDIalogueIndex = 0;
        this.currentDialogueSequence.gameObject.SetActive(false);
        this.currentDialogueSequence = null;
    }

    public void ProceedToNextDialogue()
    {
        if(this.currentDIalogueIndex < this.currentDialogueSequence.sequenceDialogues.Count)
        {
            this.ShowDialogue(this.currentDialogueSequence.sequenceDialogues[this.currentDIalogueIndex++]);
        }
        else
        {
            this.isDialogueSequenceInProcess = false;
            this.CompleteSequence();
        }
    }

    public void SetupSequence(DialogueSequence dialogueSequence)
    {
        this.dialogueText.text = "";
        this.currentDialogueSequence = dialogueSequence;
        if(dialogueSequence.isPhoneCall)
        {
            this.ShowPhoneCall();
        }

        else
        {
            this.ProceedToNextDialogue();
        }
    }

    public void ShowPhoneCall()
    {
        GameManager.instance.gameplayHUD.ToggleGameplayControls(false);
        this.dialogueInputObject.SetActive(false);
        this.phoneCallIconObject.SetActive(true);
    }

    public void HidePhoneCallobject()
    {
        this.phoneCallIconObject.SetActive(false);
    }

    public void ToggleDialogueCanvas(bool toggle,bool toggleControls)
    {
        this.StopAllCoroutines();
        this.dialoguesCanvas.SetActive(toggle);
        GameManager.instance.gameplayHUD.ToggleGameplayControls(toggleControls);
    }


    public void ShowDialogue(DialogueObject dialogueObject)
    {
        this.isDialogueSequenceInProcess = true;

        this.nextDialogueIcon.SetActive(false);
        this.dialogueInputObject.SetActive(true);

        this.currentDialogue = dialogueObject;

        if(dialogueObject.dialogueClip!=null)
        {
            this.dialoguesAudioSource.clip = dialogueObject.dialogueClip;
            this.dialoguesAudioSource.Play();
        }

        dialogueObject.EnableAndDisableObjects();

        GameManager.instance.gameplayHUD.onTextWritten += this.OnDialogueWritten;
      GameManager.instance.gameplayHUD.TypeText(this.dialogueText, dialogueObject.dialogueString, dialogueObject.timeForDialogueToWrite);
    }

}
