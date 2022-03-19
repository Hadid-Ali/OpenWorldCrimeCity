using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveObject
{
    public Constant.GameEntity waveCharacter;
    public float count;
}

[CreateAssetMenu(fileName = "Wave", menuName = "Waves System/Create Enemy Wave System")]
public class Wave : ScriptableObject
{
    public EntityType entityType;
    public List<WaveObject> waveObject = new List<WaveObject>();
    public bool hasSpecialWaveObject = false;
    public WaveObject specialWaveObject;
    public int numberOfObjectsToDefeat = 5, limitOfVisibleObjects;
    public Difficulty waveDifficultyLevel;
    public ConditionType conditionType = ConditionType.None; 
}
