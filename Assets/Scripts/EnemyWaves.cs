using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWaves : MonoBehaviour
{
    public List<Enemy> Enemies;

    public UnityEvent unityEventToCallOnWaveCompleted;

    public SimpleDelegate OnWaveStarted;
    public SimpleDelegate OnWaveCompleted;

    public int enemies;
    [SerializeField]
    private bool isWaveAlerted = false;

    [SerializeField]
    private BoxCollider _waveTriggerCollider;

    public void AlertWave()
    {
        if (this.isWaveAlerted)
            return;

        this.isWaveAlerted = true;
        for(int i=0;i<this.Enemies.Count;i++)
        {
            this.Enemies[i].InstantAlert();
        }
    }

    private void OnEnable()
    {
        this.enemies = this.Enemies.Count;
        this.OnWaveCompleted += this.EnemiesCompleted;
        for(int i=0;i<this.Enemies.Count;i++)
        {
            this.Enemies[i].wave = this;
        }
    }

    public void EnemiesCompleted()
    {
        try
        {
            if (this.unityEventToCallOnWaveCompleted != null)
                this.unityEventToCallOnWaveCompleted.Invoke();
        }
        catch(System.Exception e)
        {

        }
    }

    public void WaveEnemyKilled()
    {
        this.enemies--;

        if(this.enemies<=0)
        {
            if (this.OnWaveCompleted != null)
                this.OnWaveCompleted();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAGS.PLAYER))
        {
            this.AlertWave();
            this._waveTriggerCollider.enabled = false;
        }
    }
}
