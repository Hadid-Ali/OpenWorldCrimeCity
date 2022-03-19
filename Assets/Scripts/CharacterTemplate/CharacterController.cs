using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Character_STATES
{
    IDLE,
    WALK,
    CHASE,
    LOOKING_FOR_TARGET,
    ATTACK,
    DEFENSE
}
public enum EntityType
{
    NONE,
    PLAYER,
    PEDESTRIAN,
    ENEMY,
    ZOMBIE,
    COP,
    MAFIA
}

public delegate void CharacterDelegate();

[RequireComponent(typeof(AnimationController))]
public class CharacterController : GameEntity
{
    public EntityType characterType;
    public float health = 100f;

    [Tooltip("Kill This Character will Raise Level 1 Cops")]
    public bool isInnocent = false;
    [Tooltip("Kill This Character will Raise Level 2 Cops")]
    public bool isCop = false;

    [Tooltip("Kill This Character will Raise Level Alert Cops")]
    public bool isVIP = false;

    public GameObject nearByEnemy;
    public AnimationController animatorController;

    public CharacterDelegate OnCharacterKilled;

    public bool isPlayer = false;

    public bool isHit = false;
    
    public float totalhealth = 100;

    public virtual void Awake()
    {
        this.animatorController = this.GetComponent<AnimationController>();
        this.totalhealth = this.health;

        int x = Random.Range(0, 20);

        if (this.isInnocent | this.isCop)
        {
            this.isVIP = x < 5; 

        }
    }
    
    public void ToggleIsHit(int valueFlagBoolean)
    {
        this.isHit = valueFlagBoolean == 1;
    }

    public virtual void Start()
    {
        this.OnCharacterKilled += this.DisableCharacter;
    }

    public void RotateTo(Transform target)
    {
        if (target)
        {
            this.transform.LookAt(target.transform);
            Vector3 v = this.transform.eulerAngles;

            this.transform.eulerAngles = new Vector3(0, v.y, 0);
        }
    }

    public void RotateTo(GameObject G)
    {
        if (G)
            this.RotateTo(G.transform);
    }

    public void RotateToNearbyEnemy()
    {
        if (this.nearByEnemy)
        {
            this.RotateTo(this.nearByEnemy);
        }
    }

    public void RemoveForceFromRagdoll()
    {
        Rigidbody[] r = this.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody R in r)
        {
            R.velocity = Vector3.zero;
            R.angularVelocity = Vector3.zero;
        }
    }

    public virtual void OnEnable()
    {
        this.health = 100f;

    }

    public virtual void OnAttacked(float damage)
    {
        this.health -= damage;

        this.health = Mathf.Clamp(this.health, 0, this.totalhealth);
        if(this.health<=0)
        {
            this.KillWithForce(GameManager.instance.playerController.transform.forward, 100f);
        }
    //    Debug.Log("Is Attacked");
    }

    void DisableCharacter()
    {
        this.gameObject.SetActive(false);
    }

    [ContextMenu("Enemy Killing")]
    private void EnemyKilling()
    {
        this.KillWithForce(-this.transform.forward, 100f);
    }
    
    public virtual void KillWithForce(Vector3 dir, float ragdForce)
    {
        bool canstartWave = true;
        if(GameManager.instance.currentMission)
        {
            canstartWave = !GameManager.instance.currentMission.isTutorialMission;
        }
        if(canstartWave)
        {
            Difficulty difficulty = this.isVIP ? Difficulty.Hard : this.isCop ? Difficulty.Medium : Difficulty.Easy;

            if (this.isInnocent | this.isCop | this.isVIP)
            {
                GameManager.instance.policeManager.CallCops(difficulty);
            }
        }

        this.AddForceToRagdoll(ragdForce, dir);
        if (this.OnCharacterKilled != null)
            this.OnCharacterKilled();
    }
    
    public void AddForceToRagdoll(float force,Vector3 direction)
    {
        //GameObject R = (GameObject)Instantiate(this.characterRagdoll, this.transform.position, this.transform.rotation);
        GameObject R = GameManager.instance.ragdollsPool.CreateRagdoll(this.gameEntityName, this.transform.position, this.transform.rotation);
        //GameObject R = GameManager.instance.poolManager.FindPoolByType(this.gameEntityName).Instantiate(this.transform.position, this.transform.rotation);
        R.GetComponentInChildren<Rigidbody>().AddForce(force *direction * Time.deltaTime,ForceMode.VelocityChange);
    }
}
