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

    private void OnDisable()
    {
        Debug.LogError("GameObject");
    }

    private void Init()
    {
        if (this.isInit)
            return;

        this._gameplayInstructionBarManager = GameManager.instance.gameplayHUD;
        this.isInit = true;
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

        Debug.LogError("Show Instruction");

        yield return new WaitForSeconds(this._waitBeforeBell);
        this.AnimateBell();

        Debug.LogError($"After Animate Bell {this._waitBeforeBell}");

        yield return new WaitForSecondsRealtime(this._instructionAnimateDuration);
        this._gameplayInstructionBarManager.OnMainInstructionBarHide(instruction);

        Debug.LogError($"After Animate Bell {this._waitBeforeBell}");

        this.gameObject.SetActive(false);
    }

    private void AnimateBell()
    {
        this.bellAnimator.SetTrigger("Animate");
    }
}
