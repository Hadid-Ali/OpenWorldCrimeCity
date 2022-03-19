using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void WaveDelegate();
public class GameEntitySpawner : WaveSpawner
{
    public List<Wave> objectsWave;
    private CharacterSpawner characterObjectsSpawner;

    public WaveDelegate onWaveCompleted;

    [SerializeField]
    private Wave currentWave;

    private bool isWaveCompleted = false, canInterruptWave;

    public Vector2 waveWaits = new Vector2(120f, 300f);

    private Coroutine routine;

    [SerializeField]
    private float waitBeforeNextWave = 0;

    private void OnEnable()
    {
        
    }

    public IEnumerator StartWaveRoutine(EntityType waveEntity, Difficulty levelObWave = Difficulty.None, float wait = 60f)
    {
        yield return new WaitForSeconds(wait);
        this.StartWave(waveEntity, levelObWave);
    }

    public void StartWave(Wave wave)
    {
        this.StartWave(wave.entityType, wave.waveDifficultyLevel);
    }

    public override void Init(Wave wave)
    {
        this.currentWave = wave;
        if (wave.conditionType != ConditionType.None)
            GameManager.instance.environmentAnimator.ApplyCondition(wave.conditionType);
        base.Init(wave);
    }

    public override void StartWave(EntityType waveEntity,Difficulty levelObWave = Difficulty.None)
    {
        if (this.routine != null)
            StopCoroutine(this.routine);

        if(levelObWave!= Difficulty.None & this.IsWaveContinue)
        {
            if((int) levelObWave > (int)this.currentWave.waveDifficultyLevel)
            {
                this.canInterruptWave = true;
            }
        }

        if(this.IsWaveContinue)
        {
            if (!this.canInterruptWave)
                return;
        }
        List<Wave> wave =  this.objectsWave.FindAll(x => x.waveDifficultyLevel.Equals(levelObWave) & x.entityType.Equals(waveEntity));

        //new List<Wave>();
        //for(int i=0;i<this.objectsWave.Count;i++)
        //{
        //    if(this.objectsWave[i].entityType.Equals(waveEntity) & this.objectsWave[i].waveDifficultyLevel.Equals(levelObWave))
        //    {
        //        wave.Add(this.objectsWave[i]);
        //    }
        //}

        Debug.LogError(wave.Count);
        this.ResetWave();
        GameManager.instance.regionManager.canFight = false;
        this.Init(wave[Random.Range(0, wave.Count)]);

        StartCoroutine(this.WaveRoutine());
    }


    public override void ResetWave()
    {
        this.currentWave = null;
        base.ResetWave();
    }
      
    public override IEnumerator WaveRoutine()
    {
        while(true)
        {
            if (this.numberOfObjectsPresent < this.presentObjectsLimit)
            {
                if (this.numberOfObjectsToSpawn > 0)
                {
                    WaveObject obj = null;
                    bool isSpecialSpawn = false;
                    if(this.currentWave.specialWaveObject !=null & this.currentWave.hasSpecialWaveObject)
                    {
                        isSpecialSpawn = this.numberOfObjectsToSpawn == 1;
                    }

                    obj = isSpecialSpawn ? this.currentWave.specialWaveObject : this.currentWave.waveObject[Random.Range(0, this.currentWave.waveObject.Count)];


                    GameObject g = this.SpawnObject(obj.waveCharacter, GameManager.instance.playerController.playerSpawner.NearbyPositionAtLayer(), Quaternion.identity);
                    CharacterController controller = g.GetComponent<CharacterController>();
                    if(controller.gameEntityName.ToString().Contains("COP"))
                    {
                        this.OnCopSpawned(controller);
                    }
                    //if (GameManager.instance.playerController.playerSpawner.GetSpawnPoint!=null)
                    //{
                    //    CharacterSpawnPoint point = GameManager.instance.playerController.playerSpawner.GetSpawnPoint;
                    //    this.SpawnObject(obj.waveCharacter, point.HitPoint, Quaternion.identity);
                    //    point.isOccupied = true;
                    //}
                }
                else
                {
                    this.CompleteWave();
                }
            }
            yield return new WaitForSeconds(this.waveTransitWait);
        }
    }

    public void OnCopSpawned(CharacterController controller)
    {
        GameManager.instance.policeManager.AddToCops(controller);
    }

    public bool IsWaveContinue
    {
        get
        {
            return this.currentWave != null;
        }
    }

    public override void CompleteWave()
    {
        StopAllCoroutines();
        GameManager.instance.regionManager.canFight = true;
        this.isWaveCompleted = true;
        if (this.onWaveCompleted != null)
            this.onWaveCompleted();

        GameManager.instance.environmentAnimator.ApplyCondition(ConditionType.Day);

        int i = Random.Range(0, 15);
        this.waitBeforeNextWave = Random.Range(this.waveWaits.x, this.waveWaits.y);
        this.routine = StartCoroutine(this.StartWaveRoutine(Random.Range(0, 10) < 5 ? EntityType.ZOMBIE : EntityType.MAFIA, i > 6 ? Difficulty.Easy : i <= 6 & i > 11 ? Difficulty.Medium : Difficulty.Hard,this.waitBeforeNextWave));

        this.ResetWave();
    }

    public override GameObject SpawnObject(Constant.GameEntity entity, Vector3 position, Quaternion qr)
    {
        this.numberOfObjectsToSpawn--;
        this.numberOfObjectsPresent ++;

        NavMeshHit hit;

        if(NavMesh.SamplePosition(position,out hit,10f,NavMesh.AllAreas))
        {
            position = hit.position;
        }
        GameObject g= GameManager.instance.gameEntitiesPool.CreateEntity(entity, position, qr, this.OnObjectRemoved);
        return g;
    }
}
