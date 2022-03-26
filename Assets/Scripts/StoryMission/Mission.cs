using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum MissionName
{
    CALL_OUT_MY_NAME,
    LETS_HAVE_A_BRAWL,
    MEETING_THE_OLD_BUDS
}
public class Mission : MonoBehaviour
{
    public Transform playerStartPosition;
    public CutSceneCamera startCamera, endingCamera;
    public float cashEarning = 0f;

    public int currentLevel;

    public bool needToAvoidCops = false;
    public bool callCopsExplicitely = false;
    public Difficulty callingCops;

    public float durationBeforeMissionComplete = 0f;

    public MissionName missionName;

    [HideInInspector]
    public bool isPedestrianAttackMission = false;
    public bool isTutorialMission = false;

    public UnityEvent eventOnLevelComplete, eventOnLevelFail;


    private void OnEnable()
    {
        this.currentLevel = System.Int32.Parse(this.gameObject.name[this.gameObject.name.Length - 1].ToString());
        //PreferenceManager.ClearedLevels = this.currentLevel;
    }


    // Start is called before the first frame update
    public virtual void Start()
    {
        if (this.startCamera)
            this.startCamera.gameObject.SetActive(true);

        if (this.playerStartPosition)
        {
            GameManager.instance.playerController.gameObject.transform.position = this.playerStartPosition.position;
            GameManager.instance.playerController.gameObject.transform.rotation = this.playerStartPosition.rotation;
            
        }

        GameManager.instance.currentMission = this;
    }

    public void MissionComplete()
    {
        if(this.callCopsExplicitely)
        {
            GameManager.instance.policeManager.CallCops(this.callingCops);
            this.needToAvoidCops = true;
        }
        if(this.needToAvoidCops)
        {
            GameManager.instance.gameplayHUD.TypeInstruction("Avoid Cops");
            GameManager.instance.policeManager.onCopsAvoided += this.MissionCompletion;
            return;
        }
        else
        {
            GameManager.instance.policeManager.EvadeCops();
        }
             Invoke("MissionCompletion", this.durationBeforeMissionComplete);
    }

    void MissionCompletion()
    {
        if (this.endingCamera)
        {
            this.endingCamera.gameObject.SetActive(true);
            Invoke("CompleteMission", this.endingCamera.cutSceneDuration);
        }
        else
        {
            this.CompleteMission();
        }
    }

    public virtual void OnEnemyNeutralized()
    {

    }

    void CompleteMission()
    {
        GameManager.instance.MissionComplete(this.cashEarning);
        Destroy(this.gameObject);
    }

    public void MissionFail()
    {
        GameManager.instance.gameplayHUD.MissionFail();
        if (PreferenceManager.ClearedLevels > 3)
        {
            GameManager.instance.policeManager.CallCops(this.callingCops);
            GameManager.instance.gameplayHUD.TypeInstruction("Avoid Cops");
        }
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {

    }
}
