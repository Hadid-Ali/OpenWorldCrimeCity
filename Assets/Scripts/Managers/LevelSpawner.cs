using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private LevelData _currentLevel;

    private GameObject introVideo;

    void Start()
    {
        if (!PreferenceManager.IsFirstTimePlayed)
        {
            Invoke("SpawnIntro", 2);
        }
        else
            this.SpawnLevel();
    }

    private void SpawnIntro()
    {
        GameManager.instance.gameplayHUD.hudCanvasGroup.alpha = 0;
        GameManager.instance.gameplayHUD.hudCanvasGroup.interactable = false;
        introVideo = Instantiate(Resources.Load("CutScenes/IntroCutScene")) as GameObject;
    }

    public LevelData CurrentLevel
    {
        get => this._currentLevel;
    }

    public void SpawnLevel()
    {
        if (Constant.GameplayData.currentLevel <= Constant.GameplayData.totalLevels)
        {
            string levelNameToSpawn = $"Levels\\Level{Constant.GameplayData.currentLevel.ToString()}";

            Debug.LogError($"String {levelNameToSpawn}");

            GameObject levelObject = (GameObject)Instantiate(Resources.Load(levelNameToSpawn));
            this._currentLevel = levelObject.GetComponent<LevelData>();

            if(!Constant.UtilityData.isMenuTransition)
            {
                levelObject.SetActive(true);
            }
        }
    }

    public void ActivateLevel()
    {
        if (this._currentLevel != null)
            this._currentLevel.ActivateLevel();
    }


    public void IntroVideoCompleted()
    {
        this.SpawnLevel();

        GameManager.instance.gameplayHUD.hudCanvasGroup.alpha = 1;
        GameManager.instance.gameplayHUD.hudCanvasGroup.interactable = true;

        Destroy(introVideo);

        PreferenceManager.IsFirstTimePlayed = true;
    }
}