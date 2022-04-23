using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControlsToShow
{
    TouchPanel,
    Joystick,
    AllControls
}

public enum TutorialType
{
    TouchPad = 0,
    Joystick = 1
}

[System.Serializable]
public class Tutorial
{
    [SerializeField]
    private GameObject [] objectsToEnable;

    [SerializeField]
    private List<ControlsToShow> controlsToShow;

    [SerializeField]
    private TutorialType tutorialType;

    [SerializeField,TextArea]
    private string messageToGive;
    
    [SerializeField]
    private bool showAllControls = false;

    [SerializeField]
    private bool completeTutorials;

    public bool CompleteTutorials
    {
        get => this.completeTutorials;
    }

    public string MessageToGive
    {
        get => this.messageToGive;
    }

    public virtual void Start()
    {
        this.ToggleTutorialObjects(true);
        GameManager.instance.gameplayHUD.ShowUIElements(this.controlsToShow);
    }

    public virtual void Complete()
    {
        this.ToggleTutorialObjects(false);

        if(this.showAllControls)
        GameManager.instance.gameplayHUD.ShowUIElements(new List<ControlsToShow> { ControlsToShow.AllControls });
    }

    public void ToggleTutorialObjects(bool toggle)
    {
        for (int i = 0; i < this.objectsToEnable.Length; i++)
        {
            this.objectsToEnable[i].SetActive(toggle);
        }
    }
}

public class TutorialsManager : MonoBehaviour
{
    [SerializeField]
    private List<Tutorial> tutorials;

    public static TutorialsManager Instance;

    private Tutorial _currentTutorial;

    [SerializeField]
    private GameObject messagePanel;
    
    [SerializeField]
    private Text messagePanelText;

    private void Awake()
    {
        Instance = this;
    }

    public void CompleteCurrentTutorial()
    {
        if (this._currentTutorial != null)
            this._currentTutorial.Complete();

        if (this._currentTutorial.CompleteTutorials)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void StartTutorial(TutorialType tutorialType)
    {
        if (this._currentTutorial != null)
            this._currentTutorial.Complete();

        this._currentTutorial = this.tutorials[(int)tutorialType];

        this.messagePanel.SetActive(true);
        this.messagePanelText.text = this._currentTutorial.MessageToGive;

        this._currentTutorial.Start();

    }

}
