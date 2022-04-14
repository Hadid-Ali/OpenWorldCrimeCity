using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PedestrianSystem;
using UnityEngine.SceneManagement;

public enum ControlsMode
{
    PLAYER = 0,
    CAR = 1,
    BIKE = 2
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController playerController;
    public vThirdPersonCamera mainCamera;
    public PedestrianSystemManager pedestrianManager;
    public GameplayHUD gameplayHUD;
    public RagdollsPool ragdollsPool;
    public PoliceManager policeManager;
    public CameraManager cameraManager;

    public CameraShake cameraShake;

    public PedestrianSpawner pedestrianSpawner;
    public PedestrianRemover PedestrianRemover;
    public EntitiesPool gameEntitiesPool;
    public GameEntitySpawner playerWavesSpawner;
    public Vehicle playerVehicle;

    public ObjectPoolsManager objectPools;

    public TaxiModuleManager taxiModule;
    public EnvironmentAnimator environmentAnimator;

    public GameRegionManager regionManager;
    public MapCanvasController radarController;

    public LevelData currentMission;
    public LevelSpawner levelSpawner;

    public bool isfirstWeaponSelected = false;

    public bool isGameMode = true;

    public GameObject mainMenuElements, gameplayElements, welcomeText;

    public string iOSRateLink = "itms-apps://itunes.apple.com/app/idYOUR_ID", AndroidRateLink = "market://details?id=YOUR_ID";

    public int totalCashEarned = 0;
    public int totalKills = 0;

    public NavigationBehaviour navigationBehaviour;

    public AdsManager _adsManager;

    public void AssignNavigationTarget(NavigationTarget navigationTarget)
    {
        this.navigationBehaviour.AssignTarget(navigationTarget);
    }

    public void OnRateButton()
    {
#if UNITY_ANDROID
        Application.OpenURL(this.AndroidRateLink);
#elif UNITY_IPHONE
        Application.OpenURL(this.iOSRateLink);
#endif
    }

    public PlayerCheckPointInterface playerCheckPointHandler => this.playerController;

    public void ShakeCamera()
    {
        this.cameraShake.enabled = true;
    }

    private void Awake()
    {
        if (!this.isGameMode)
        {
            this.mainMenuElements.SetActive(Constant.isMainMenu);
            this.gameplayElements.SetActive(!Constant.isMainMenu);
        }
        this._adsManager = AdsManager.Instance;
        instance = this;
    }

    public void OnDialogueSequenceEnd(bool isPhoneCall)
    {
        if(isPhoneCall)
        {
            this.ShowInterstitial();
        }
    }

    public void ShowInterstitial()
    {
        this._adsManager.ShowInterstitial();
    }

    private void Start()
    {
        LoadingHandler.OnLoadingCompleteEvent += this.ActivateLevel;
    }

    public void ActivateLevel()
    {
        this.levelSpawner.ActivateLevel();
        Constant.UtilityData.isMenuTransition = false;
    }

    public DistanceCalculator DistanceCalculator
    {
        get => this.playerController;
    }

    public void StartNextMission()
    {
        Constant.GameplayData.currentLevel++;
        this.RestartGame();
    }

    public void EarnReward(int rewardToGive,bool isLevelComplete,float waitBeforeLevelComplete = 1f)
    {
        PreferenceManager.CashBalance += rewardToGive;
        this.totalCashEarned += rewardToGive;

        if(isLevelComplete)
        {
            Invoke("MissionComplete", waitBeforeLevelComplete);
        }
        
        else
        {
            this.gameplayHUD.ShowCashEarned(rewardToGive);
        }
    }

    
    public void ReStockAmmo()
    {
        WeaponInventory inventory = this.playerController.weaponInventory;

        for(int i=0;i<inventory.weapons.Count;i++)
        {
            if(inventory.weapons[i].isEquippedByPlayer)
            {
                ShootingWeapon weapon = (ShootingWeapon)inventory.weapons[i];
                weapon.ammoCount = weapon.ammoCappacity;
            }
        }
    }


    public void MissionComplete()
    {
        if (PreferenceManager.CurrentLevel > Constant.GameplayData.totalLevels)
            PreferenceManager.CurrentLevel ++;

        this.ShowInterstitial();

        this.gameplayHUD.MissionComplete(this.totalCashEarned);
    }
   
    public void MissionFail()
    {
        Invoke("_MissionFail", 2f);
    }
    
    private void _MissionFail()
    {
        this.ShowInterstitial();
        GameManager.instance.gameplayHUD.MissionFail();
    }

    public void PauseGame()
    {
        this.ShowInterstitial();
        this.gameplayHUD.TogglePauseMenu(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        this.gameplayHUD.TogglePauseMenu(false);
    }


   [ContextMenu("Clear Prefs")]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(Constant.Scenes.maineMenu);
    }

    public void PlayClickSound()
    {
        SoundManager.instance.PlaySound(SoundType.CLICK_SOUND);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }



    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Constant.Scenes.gameplayScene);
    }

    public void SetGameQuality(int i)
    {
        QualitySettings.SetQualityLevel(i );
    }

    float timeScale = 1f;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        this.playerWavesSpawner.StartWave(EntityType.ZOMBIE, Difficulty.Easy);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            this.timeScale = Time.timeScale;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 5f;

        }
        else
        {
            Time.timeScale = this.timeScale;
        }
    }

    public void ChangePlayMode(ControlsMode mode)
    {
        this.gameplayHUD.ChangeControlsType(mode);
    }
}
