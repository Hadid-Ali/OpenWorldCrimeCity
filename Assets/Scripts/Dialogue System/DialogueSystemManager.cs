using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemManager : MonoBehaviour
{
    public static DialogueSystemManager instance;

    public GameObject dialoguesCanvas;

    public Text[] characterNamesTexts;
    public Text[] dialoguesTexts;

    public GameObject nextDialogueIcon;

    public Image[] narratorImages;
    public Image[] narratorBGImages;

    private DialogueSequence currentDialogueSequence;
    private int currentDIalogueIndex = 0;

    private DialogueObject currentDialogue;
    private Dictionary<CharacterIdentity, int> dialogueHashTable;
    private Text currentdialogueText;
    private GameObject currentBGImage;
    private GameObject currentNarratorNameObject;
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


    public void OnDialogueWritten()
    {
        this.nextDialogueIcon.SetActive(true);
    }

    public void DialogueInput()
    {
        if(SoundManager.instance)
        SoundManager.instance.PlaySound(SoundType.CLICK);

        if (this.dialoguesAudioSource.isPlaying)
            this.dialoguesAudioSource.Stop();
        
        if(GameManager.instance.gameplayHud.isTextWriting)
        {
            GameManager.instance.gameplayHud.StopTextWriting();
            this.currentdialogueText.text = this.currentDialogue.dialogueString;
        }
        else
        {
            this.ProceedToNextDialogue();
        }
    }

    private void CompleteSequence()
    {
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
        CharacterIdentity narratora = dialogueSequence.narratorA;
        CharacterIdentity narratorb = dialogueSequence.narratorB;

        this.currentDialogueSequence = dialogueSequence;
        this.dialogueHashTable = new Dictionary<CharacterIdentity, int>();

        this.dialogueHashTable.Add(narratora, 0);
        this.dialogueHashTable.Add(narratorb, 1);

        this.characterNamesTexts[0].text = narratora.ToString();
        this.characterNamesTexts[1].text = narratorb.ToString();

        this.narratorImages[0].sprite = this.narratorBGImages[0].sprite = this.dialogueNarrators.Find(x => x.characterIdentity == narratora).characterAvatar;
        this.narratorImages[1].sprite = this.narratorBGImages[1].sprite = this.dialogueNarrators.Find(x => x.characterIdentity == narratorb).characterAvatar;

        this.ProceedToNextDialogue();
    }


    public void ToggleDialogueCanvas(bool toggle,bool toggleControls)
    {
        this.StopAllCoroutines();
        this.dialoguesCanvas.SetActive(toggle);
        GameManager.instance.gameplayHud.ToggleGameplayControls(toggleControls);
    }


    public void ShowDialogue(DialogueObject dialogueObject)
    {
        this.isDialogueSequenceInProcess = true;
        this.nextDialogueIcon.SetActive(false);
        if (this.currentNarrator != dialogueObject.dialogueNarrator)
        {
            this.currentNarrator = dialogueObject.dialogueNarrator;
        }
        this.currentDialogue = dialogueObject;

        if (this.currentNarratorNameObject)
            this.currentNarratorNameObject.SetActive(false);

        int index = this.dialogueHashTable[dialogueObject.dialogueNarrator];

        this.currentNarratorNameObject = this.characterNamesTexts[index].gameObject;
        this.currentNarratorNameObject.SetActive(true);

        if (this.currentdialogueText)
            this.currentdialogueText.gameObject.SetActive(false);

        this.currentdialogueText = this.dialoguesTexts[index];

        this.currentdialogueText.gameObject.SetActive(true);

        if (this.currentBGImage)
            this.currentBGImage.SetActive(true);

        this.currentBGImage = this.narratorBGImages[index].gameObject;
        this.currentBGImage.SetActive(false);

        if(dialogueObject.dialogueClip!=null)
        {
            this.dialoguesAudioSource.clip = dialogueObject.dialogueClip;
            this.dialoguesAudioSource.pitch = dialogueObject.dialoguePitch;
            this.dialoguesAudioSource.Play();
        }

        dialogueObject.EnableAndDisableObjects();

        GameManager.instance.gameplayHud.onTextWritten += this.OnDialogueWritten;
      GameManager.instance.gameplayHud.TypeText(this.currentdialogueText, dialogueObject.dialogueString, dialogueObject.timeForDialogueToWrite);
    }

}
