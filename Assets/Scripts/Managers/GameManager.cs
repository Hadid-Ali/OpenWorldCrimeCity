﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PedestrianSystem;
using UnityEngine.SceneManagement;

public enum PlayMode
{
    PLAYER,
    CAR,
    BIKE
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController playerController;
    public vThirdPersonCamera mainCamera;
    public PedestrianSystemManager pedestrianManager;
    public GameplayHUD gameplayHUD;
    public CameraController camController;
    public RagdollsPool ragdollsPool;
    public PoliceManager policeManager;

    public CameraShake cameraShake;

    public PedestrianSpawner pedestrianSpawner;
    public PedestrianRemover PedestrianRemover;
    public EntitiesPool gameEntitiesPool;
    public GameEntitySpawner playerWavesSpawner;
    public Vehicle playerVehicle;

    public TaxiModuleManager taxiModule;
    public EnvironmentAnimator environmentAnimator;

    public GameRegionManager regionManager;
    public MapCanvasController radarController;

    public Mission currentMission;

    public bool isfirstWeaponSelected = false;

    public bool isGameMode = true;

    public GameObject mainMenuElements, gameplayElements, gameOverMenu, gamePauseMenu, welcomeText;

    public string iOSRateLink = "itms-apps://itunes.apple.com/app/idYOUR_ID", AndroidRateLink = "market://details?id=YOUR_ID";


    public void OnRateButton()
    {
#if UNITY_ANDROID
        Application.OpenURL(this.AndroidRateLink);
#elif UNITY_IPHONE
        Application.OpenURL(this.iOSRateLink);
#endif
    }

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
        instance = this;
    }

    private void Start()
    {
        if (Application.loadedLevelName.Equals(Constant.Scenes.gameplayScene))
            if (PreferenceManager.ClearedLevels <= 0)
            {
                this.StartMission(1);
            }
    }

    public void StartNextMission()
    {
        this.StartMission(PreferenceManager.ClearedLevels + 1);
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

    public void StartMission(int index)
    {
        if (PreferenceManager.ClearedLevels >= Constant.Levels.totalLevels)
            return;

        GameObject missionObject = (GameObject) Instantiate(Resources.Load(string.Format("Levels/Mission{0}", index)));
    }

    public void MissionComplete(float reward)
    {
        PreferenceManager.ClearedLevels += 1;
        Debug.LogError(PreferenceManager.ClearedLevels);
        this.gameplayHUD.MissionComplete(reward);
        PreferenceManager.CashBalance += reward;

        PreferenceManager.CurrentLevel++;
    }
    

    public void GameOver()
    {
        this.gameplayHUD.characterControls.SetActive(false);
        this.gameplayHUD.carControls.SetActive(false);
        this.gameOverMenu.SetActive(true);
    }

    public void PauseGame()
    {
        this.gamePauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        this.gamePauseMenu.SetActive(false);
    }

    [ContextMenu("Cleared Levels")]
    public void ClearedLevels()
    {
        Debug.Log("Cleared Levels: " + PreferenceManager.ClearedLevels);
    }

   [ContextMenu("Clear Prefs")]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void PlayGame()
    {
        this.mainMenuElements.SetActive(false);
        this.gameplayElements.SetActive(true);
        Constant.isMainMenu = false;
        if(Constant.isFirstPlay)
        {
            Constant.isFirstPlay = false;
            if (this.welcomeText)
            {
                this.welcomeText.SetActive(true);
                Invoke("HideText",2f);
            }
        }
    }
    
    void HideText()
    {
        if (this.welcomeText)
            this.welcomeText.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadSceneAsync(1);
        Constant.isMainMenu = true;
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

        SceneManager.LoadSceneAsync(1);
        Constant.isMainMenu = false;
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

    public void ChangePlayMode(PlayMode mode)
    {
        this.camController.ToggleCamMode(mode);
        this.gameplayHUD.ChangeUIMode(mode);
    }
}
