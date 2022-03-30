 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayInstructionBar : InstructionBar
{
    [SerializeField]
    private Text rewardText;

    [SerializeField]
    private Animator bellAnimator;

    private GameplayInstructionBarManager _gameplayInstructionBarManager;
    private bool isInit = false;

    [SerializeField]
    private float _waitBeforeBell = 1f;

    [SerializeField]
    private float _instructionAnimateDuration = 2f;

    private void Awake()
    {
        this.Init();   
    }

    private void OnEnable()
    {
        this._gameplayInstructionBarManager.OnMainInstructionBarShow();
    }

    private void Init()
    {
        if (this.isInit)
            return;

        this._gameplayInstructionBarManager = GameManager.instance.gameplayHUD;
    }

    public void ShowInstruction(string instruction,string reward)
    {
        if (!this.isInit)
            this.Init();

        this.SetInstruction(instruction);
        StartCoroutine(this.Coroutine_ShowInstruction(instruction, reward));
    }

    private IEnumerator Coroutine_ShowInstruction(string instruction, string reward)
    {
        this.rewardText.text = $"REWARD: {reward}$";

        yield return new WaitForSecondsRealtime(this._waitBeforeBell);
        this.AnimateBell();

        yield return new WaitForSecondsRealtime(this._instructionAnimateDuration);
        this._gameplayInstructionBarManager.OnMainInstructionBarHide(instruction);

        this.gameObject.SetActive(false);
    }

    private void AnimateBell()
    {
        this.bellAnimator.SetTrigger("Animate");
    }
}
