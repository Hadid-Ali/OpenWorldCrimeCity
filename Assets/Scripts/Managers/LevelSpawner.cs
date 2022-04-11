using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private LevelData _currentLevel;

    void Start()
    {
        this.SpawnLevel();
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
        }
    }

    public void ActivateLevel()
    {
        if (this._currentLevel != null)
            this._currentLevel.ActivateLevel();
    }

}
