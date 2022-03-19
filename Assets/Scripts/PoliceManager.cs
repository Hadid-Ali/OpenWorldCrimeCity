using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    private int _alertLevel = 0;
    public float basicEvasionDistance = 100f;
    private float evasionDistance;
    public Vector3 copsAlertPosition;

    public List<CharacterController> cops;

    public SimpleDelegate onCopsAvoided;

    public GameObject player;

    public void AddToCops(CharacterController controller)
    {
        this.cops.Add(controller);
    }



    private void Awake()
    {
        this.cops = new List<CharacterController>();
        this.evasionDistance = this.basicEvasionDistance;
    }

    


    public int AlertLevel
    {
        get
        {
            return this._alertLevel;
        }

        set
        {
            this._alertLevel = value;
            GameManager.instance.gameplayHUD.SetAlertLevel(this._alertLevel);
        }
    }

    public IEnumerator PoliceEvasion()
    {
        while (true)
        {

            if (this.player == null)
                this.player = GameManager.instance.playerController.gameObject;

            float distance = Vector3.Distance(this.player.transform.position, this.copsAlertPosition);
          //  Debug.LogError("Distance: " + distance);
            if (distance >= this.basicEvasionDistance)
            {

            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void CallCops(Difficulty copsLevel)
    {
        if (this.copsAlertPosition.Equals(Vector3.zero))
            this.copsAlertPosition = GameManager.instance.playerController.transform.position;
        switch (copsLevel)
        {
            case Difficulty.Easy:
                this.AlertLevel = 1;
                this.basicEvasionDistance = this.evasionDistance * 1f;

                break;

            case Difficulty.Medium:
                this.AlertLevel = 3;
                this.basicEvasionDistance = this.evasionDistance * 2f;
                break;

            case Difficulty.Hard:
                this.AlertLevel = 5;
                this.basicEvasionDistance = this.evasionDistance * 3f;
                break;
        }
        GameManager.instance.playerWavesSpawner.StartWave(EntityType.COP, copsLevel);
        StartCoroutine(this.PoliceEvasion());
    }

    [ContextMenu("Evade Alert")]
    public void EvadeCops()
    {
        this.AlertLevel = -100;
        this.copsAlertPosition = Vector3.zero;
        for(int i=0;i<this.cops.Count;i++)
        {
            GameManager.instance.gameEntitiesPool.DestroyEntity(this.cops[i].gameEntityName, this.cops[i].gameObject);
        }
        if (this.onCopsAvoided != null)
            this.onCopsAvoided();
        this.cops.Clear();
    }
}
