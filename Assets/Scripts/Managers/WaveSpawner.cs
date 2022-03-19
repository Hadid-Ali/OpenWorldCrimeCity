using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveSpawner : MonoBehaviour
{
    public int numberOfObjectsPresent = 0;

    public int numberOfObjectsToSpawn = 0;
    public int presentObjectsLimit = 0;

    public float waveTransitWait = 1f;
    
    public virtual void ResetWave()
    {
        this.numberOfObjectsPresent = this.numberOfObjectsToSpawn = this.presentObjectsLimit = 0;
    }
    
    public virtual void Init(Wave wave)
    {
        this.numberOfObjectsToSpawn = wave.numberOfObjectsToDefeat;
        this.presentObjectsLimit = wave.limitOfVisibleObjects;
        this.numberOfObjectsPresent = 0;
    }


    public virtual void OnObjectRemoved()
    {
        if (this.numberOfObjectsPresent > 0)
            this.numberOfObjectsPresent--;
        else
            Debug.LogError("Wave Completed");
    }

    public abstract GameObject SpawnObject(Constant.GameEntity entity, Vector3 position, Quaternion identity);
    public abstract void StartWave(EntityType waveType,Difficulty waveLevel);
    public abstract void CompleteWave();
    public abstract IEnumerator WaveRoutine();

}
 