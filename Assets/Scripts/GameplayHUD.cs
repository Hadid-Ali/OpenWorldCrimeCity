using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CnControls;

public enum MessageMode
{
    YesNo,
    Ok
}

public interface GameplayInstructionBarManager
{
    public void OnMainInstructionBarShow();
    public void OnMainInstructionBarHide(string shortInstructionToShow);
}


public enum ControlsType
{
    Movement,
    Fighting,
    Driving
}

public delegate void HudDelegate();

public class GameplayHUD : MonoBehaviour, GameplayInstructionBarManager
{
    public HudDelegate yesBtnDelegate, noBtnDelegate, okBtnDelegate;
    public Animator screenAnimator;

    [SerializeField]
    private Touchpad mainTouchPad;

    public CanvasGroup hudCanvasGroup;
    public Image crosshair, playerHealthBar;

    public GameObject gameplayControls, shootingJoyStick, characterControls, carControls, messageBox, yesNoBtn, oKBtn, noBulletMessage, healthBarParent, instructionBar,
        missionComplete, missionFail,
        homeMenu;

    public Button actionBtn, carEnterBtn, carExitBtn, punchBtn,
        missionButton,homeTransitionButton,farmsTransitionButton,restockAmmoBtn,
        bcutton;

    public Text messageText, titleText, instruction, cashEarned,totalCurrentCash;

    public static bool isShooting = false;
    public static bool isAiming = false;

    public static bool isAccelerate, isBrake, isRight, isLeft;

    HudDelegate actionBtnDelegate;
    public Image[] alertLevels;
    public Sprite alertImage, normalImage;



    private PlayerAiming playerAimingComponent;

    [SerializeField]
    private Image closeAimingImage;

    [SerializeField]
    private Image weaponImage;

    [SerializeField]
    private List<Sprite> weaponSprites;

    [SerializeField]
    private GameObject closeAimingButton;

    [SerializeField]
    private GameObject crosshairFocus;

    public static GameplayHUD Instance;

<<<<<<< Updated upstream
    [SerializeField]
    private GameplayInstructionBar _gameplayInstructionBar;

    [SerializeField]
    private GameObject shortInstructionBar;

    void GameplayInstructionBarManager.OnMainInstructionBarShow()
    {

    }

    void GameplayInstructionBarManager.OnMainInstructionBarHide(string shortInstructionToShow)
    {

    }

=======
>>>>>>> Stashed changes
    public void SetAlertLevel(int level)
    {
        for (int i = 0; i < this.alertLevels.Length; i++)
        {
            this.alertLevels[i].sprite = i <= level - 1 ? this.alertImage : this.normalImage;
        }
    }
    

    public void ToggleHomeControls(bool b)
    {
        this.homeMenu.SetActive(b);
    }
    
    public void UpdateCashEarned()
    {
       if(this.totalCurrentCash)
        {
            this.totalCurrentCash.text = string.Format("{0}$", PreferenceManager.CashBalance);
        }
    }

    private void OnEnable()
    {
        Instance = this;
        CutSceneHandler.OnCutSceneEnd_Event += CutSceneHandler_OnCutSceneEnd_Event;
    }

    private void OnDisable()
    {
        Instance = null;
        CutSceneHandler.OnCutSceneEnd_Event -= CutSceneHandler_OnCutSceneEnd_Event;
    }

    private void CutSceneHandler_OnCutSceneEnd_Event()
    {
        gameplayControls.SetActive(true);
    }

    void CacheAimingComponent()
    {
        if (this.playerAimingComponent == null)
            this.playerAimingComponent = GameManager.instance.playerController.aimingManager;
    }

    private void Start()
    {
        if (this.actionBtn != null)
            this.actionBtn.onClick.AddListener(this.ActionButtonEvent);


        this.UpdateCashEarned();

        this.homeTransitionButton.onClick.AddListener(() =>
        {
            this.LoadScene(Constant.Scenes.savinghudScene);
        });

        if(this.farmsTransitionButton)
        this.farmsTransitionButton.onClick.AddListener(() =>
        {
            this.LoadScene(Constant.Scenes.farmingScene);
        });

        if(this.missionButton)
        this.missionButton.onClick.AddListener(() =>
        {
            GameManager.instance.StartNextMission();
        });

        if(this.restockAmmoBtn)
        this.restockAmmoBtn.onClick.AddListener(() =>
        {
            GameManager.instance.ReStockAmmo();
        });
        
        if(this.bcutton)
        {
            this.bcutton.onClick.AddListener(() =>
            {
                this.LoadScene(Constant.Scenes.gameplayScene);
            });
        }
    }

    public void PauseButtonInput()
    {
        GameManager.instance.PauseGame();
    }

    private bool isCloseAiming = false;

    public void CloseAiming()
    {
        this.isCloseAiming = !this.isCloseAiming;
        this.ToggleCloseAiming(this.isCloseAiming);
    }

    private void ToggleCloseAiming(bool bToggle)
    {
        this.CacheAimingComponent();
        this.playerAimingComponent.AimWeapon(bToggle);
        this.closeAimingImage.color = bToggle ? Color.green : Color.white;
        this.mainTouchPad.offsetDrag = bToggle ? 4f : 1f;
    }

    public void ToggleCloseAimingButton(bool toggle)
    {
        this.isCloseAiming = false;
        this.ToggleCloseAiming(false);
        this.closeAimingButton.SetActive(toggle);
    }

    public void ToggleGameplayControls(bool b)
    {
        this.gameplayControls.SetActive(b);
    }

    public void FadeScreenIn()
    {
        this.screenAnimator.SetTrigger("FadingIn");
    }

    public void FadeScreenOut()
    {
        this.screenAnimator.SetTrigger("FadingOut");
    }

    public void MissionComplete(float reward)
    {
        this.ToggleGameplayControls(false);
        this.missionComplete.SetActive(true);

        this.cashEarned.text = string.Format("Cash Earned: {0}$", reward.ToString());

        this.UpdateCashEarned();

        Invoke("DisableMenu", 5f);

    }
    
    void DisableMenu()
    {
        this.ToggleGameplayControls(true);
        this.missionComplete.SetActive(false);
    }

    public void MissionFail()
    {
        this.missionFail.SetActive(true);
        Invoke("DisableMissionFail", 1f);
    }

    void DisableMissionFail()
    {
        this.missionFail.SetActive(false);
    }

    public void ShowActionButton(HudDelegate actionEvent)
    {
        Delegate.RemoveAll(actionBtnDelegate, actionBtnDelegate);

        if (actionEvent != null)
        {
            actionBtnDelegate += actionEvent;
        }

        if (this.actionBtn != null)
            this.actionBtn.gameObject.SetActive(true);
    }

    public void HideActionButton()
    {
        Delegate.RemoveAll(actionBtnDelegate, actionBtnDelegate);

        if (this.actionBtn != null)
            this.actionBtn.gameObject.SetActive(false);
    }

    public void ActionButtonEvent()
    {
        if (this.actionBtnDelegate != null)
            this.actionBtnDelegate();
    }

    public void LoadScene(string scene)
    {
        Application.LoadLevel(scene);
    }

    public void RestockAmmo()
    {

    }

    public void StartMission()
    {

    }

    public void ShowMessage(string message, string title = "Message",MessageMode mode = MessageMode.Ok,HudDelegate yesMethod = null,HudDelegate noMethod = null, HudDelegate okMethod = null)
    {
        this.yesBtnDelegate = this.noBtnDelegate = this.okBtnDelegate = null;

        if (yesMethod != null)
            this.yesBtnDelegate += yesMethod;
        if (noMethod != null)
            this.noBtnDelegate += noMethod;
        if (okMethod != null)
            this.okBtnDelegate += okMethod;

        if (!System.String.IsNullOrEmpty(title))
            this.titleText.text = title;

        this.messageText.text = message;

        this.messageBox.SetActive(true);

        this.yesNoBtn.SetActive(false);
        this.oKBtn.SetActive(false);

        switch(mode)
        {
            case MessageMode.YesNo:
                this.yesNoBtn.SetActive(true);
                break;

            case MessageMode.Ok:
                this.oKBtn.SetActive(true);
                break;
        }
    }

    public void TypeInstruction(string text,float typeT = 1f)
    {
        this.instructionBar.SetActive(true);
        StartCoroutine(UtilityMethods.TypeText(text, typeT, this.instruction));
    }

    public void ResetInstruction()
    {
       this.instruction.text = "";
    }

    public void FillPlayerHealthBar(float currentHealth, float fullHealth)
    {
        this.playerHealthBar.fillAmount = currentHealth / fullHealth;
    }

    public void HideHB()
    {
        this.healthBarParent.SetActive(false);
    }

    public void ToggleMeleeFightControls(bool b)
    {
        this.punchBtn.gameObject.SetActive(b);
        //this.kickBtn.gameObject.SetActive(b);
    }

    public void YesEvent()
    {
        this.messageBox.SetActive(false);

        if (this.yesBtnDelegate != null)
            this.yesBtnDelegate();
    }

    public void NoEvent()
    {
        this.messageBox.SetActive(false);

        if (this.noBtnDelegate != null)
            this.noBtnDelegate();
    }

    public void OkEvent()
    {
        this.messageBox.SetActive(false);

        if (this.okBtnDelegate != null)
            this.okBtnDelegate();
    }

    public void Toggle_IsAccelerate(bool b)
    {
        isAccelerate = b;
    }

    public void Toggle_IsBrake(bool b)
    {
        isBrake = b;
    }

    public void Toggle_IsLeft(bool b)
    {
        isLeft = b;
    }

    public void PunchMethod()
    {
        GameManager.instance.playerController.meleeFightHandler.PunchAttack();
    }

    public void KickMethod()
    {
        GameManager.instance.playerController.meleeFightHandler.KickAttack();
    }

    public void ChangeControlsType(ControlsMode mode)
    {
        this.characterControls.SetActive(false);
        this.carControls.SetActive(false);
        switch (mode)
        {
            case ControlsMode.PLAYER:
                this.characterControls.SetActive(true);
                break;

            case ControlsMode.CAR:
                this.carControls.SetActive(true);
                break;

            case ControlsMode.BIKE:
                this.carControls.SetActive(true);
                break;

        }
    }
    public void ChangeCrosshair(GameObject obj)
    {
        if (!obj)
        {
            this.crosshairFocus.SetActive(false);
            return;
        }

        switch(obj.tag)
        {
            case Constant.TAGS.PEDESTRIAN:
            case Constant.TAGS.MISSION_OBJECTS:
            case Constant.TAGS.ENEMY:
                this.crosshairFocus.SetActive(true);
                break;

            default:
                this.crosshairFocus.SetActive(false);
                break;
        }
    }



    public void Toggle_IsRight(bool b)
    {
        isRight = b;
    }

    public void ToggleCarEnterBtn(bool b)
    {
        this.carEnterBtn.gameObject.SetActive(b);
    }

    public void ToggleCarExitBtn(bool b)
    {
        this.carExitBtn.gameObject.SetActive(b);
    }

    public void ActionJoystick(bool b)
    {
        this.shootingJoyStick.SetActive(b);
    }

    public void ToggleCrosshair(bool b)
    {
       // this.crosshair.gameObject.SetActive(b);
    }

    public void GiveNoBulletMessage()
    {
        this.noBulletMessage.SetActive(true);
        if (IsInvoking("Hide"))
            CancelInvoke("Hide");
        Invoke("Hide", 0.5f);
    }

    void Hide()
    {
        this.noBulletMessage.SetActive(false);
    }

    private void CheckForTap()
    {
        if(!this.isTap)
        {
            isShooting = false;
        }
    }

    public bool canShootFurther => isTap;

    private bool isTap = false;

    public void ToggleIsShooting(bool b)
    {
        this.isTap = b;
        if (b)
        {
            isShooting = true;

            if (IsInvoking("CheckForTap"))
                CancelInvoke("CheckForTap");

            Invoke("CheckForTap", 1f);
        }

        else if (!IsInvoking("CheckForTap") && !b)
        {
            isShooting = false;
        }
    }

    public void SetWeaponIcon(int index)
    {
        this.weaponImage.sprite = this.weaponSprites[index];
    }

    public void ReloadWeapon()
    {
        GameManager.instance.playerController.weaponInventory.ReloadWeapon();
    }

    public void ChangeWeapon()
    {
        bool isSuccessSwitch = false;
        GameManager.instance.playerController.weaponInventory.SwitchWeapon(ref isSuccessSwitch);
        this.ActionJoystick(isSuccessSwitch);
    }

    #region CarButtonHandler

    public GameObject carBtn;

    public void ShowCarButton()
    {
        if (!carBtn.activeInHierarchy)
        {
            carBtn.SetActive(true);
        }
    }

    public void HideCarButton()
    {
        if (carBtn.activeInHierarchy)
        {
            carBtn.SetActive(false);
        }
    }

    public void GetIntoTheCar()
    {
        if (TrafficToRccCar.Instance.EnterTrafficCar())
        {
            HideCarButton();
            gameplayControls.SetActive(false);
        }
    }

    #endregion

    public void OpenStoreMenu()
    {
        StoreMenu.Instance.OpenStoreMenu();
    }

    #region Dialogues
    [Header("Dialogues Panel")]
    public GameObject dialoguesPanel;
    public Image dialogueIcon;
    public Text dialogueText;
    //[Space (10)]
    #endregion
}
