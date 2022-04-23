using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialEventType
{
    Start,
    Complete
}

public class TutorialPoint : MonoBehaviour
{
    [SerializeField]
    private TutorialEventType _tutorialEventType;


    [Header("Only Applies To Start Tutorial")]
    [SerializeField]
    private TutorialType _tutorialType;


    private void OnEnable()
    {
        this.TutorialMethod();
    }

    public virtual void TutorialMethod()
    {
        switch(this._tutorialEventType)
        {
            case TutorialEventType.Start:
                TutorialsManager.Instance.StartTutorial(this._tutorialType);
                break;

            case TutorialEventType.Complete:
                TutorialsManager.Instance.CompleteCurrentTutorial();
                break;
        }
    }
}
