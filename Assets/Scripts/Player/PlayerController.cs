using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


public class PlayerController : CharacterController
{
    [HideInInspector]
    public AimingManager aimingManager;
    [HideInInspector]
    public ThirdPersonCharacter thirdPersonCharacter;
    [HideInInspector]
    public WeaponInventory weaponInventory;
    [HideInInspector]
    public FullBodyIK fullBodyIK;
    [HideInInspector]
    public MeleeFightManager meleeFightHandler;
    public PlayerSpawner playerSpawner;
    [HideInInspector]
    public ThirdPersonUserControl thirdPersonUserControl;
    [HideInInspector]
    public AICharacterControl playerAiController;

    public GameObject cutSceneCamera;
    public NearbyEnemyLocator nearbyEnemyLocator;

    private Transform _transform;

    public float distance = 5f;


    public override void Awake()
    {
        base.Awake();
        this.aimingManager = this.GetComponent<AimingManager>();
        this.weaponInventory = this.GetComponent<WeaponInventory>();
        this.thirdPersonCharacter = this.GetComponent<ThirdPersonCharacter>();
        this.fullBodyIK = this.GetComponent<FullBodyIK>();
        this.meleeFightHandler = this.GetComponent<MeleeFightManager>();
        this.playerSpawner = this.GetComponent<PlayerSpawner>();
        this.thirdPersonUserControl = this.GetComponent<ThirdPersonUserControl>();
        this.playerAiController = this.GetComponent<AICharacterControl>();

        this._transform = this.transform;
    }

    public void TogglePlayerForAIMovement(bool toggle)
    {
        this.thirdPersonCharacter.TogglePlayerPhysics(!toggle);
        this.thirdPersonUserControl.enabled = this.thirdPersonCharacter.enabled = this.meleeFightHandler.enabled = !toggle;
    }

    public void ToggleCutSceneCamera(bool b,Vector3 cutScenePosition, Vector3 rotation)
    {
        if(this.cutSceneCamera)
        {

            this.cutSceneCamera.SetActive(b);
            this.cutSceneCamera.transform.localEulerAngles = Vector3.zero;
            this.cutSceneCamera.transform.localEulerAngles = new Vector3(0, rotation.y, 0f);

            GameObject cCam = this.cutSceneCamera.transform.GetChild(0).gameObject;
            cCam.transform.localEulerAngles = new Vector3(rotation.x, 0f, rotation.z);
            cCam.transform.localPosition = cutScenePosition;

        }
    }

    public void AllignWith(Transform target)
    {
        this._transform.eulerAngles = new Vector3(0f, target.eulerAngles.y, 0f);
    }

    public override void KillWithForce(Vector3 dir, float ragdForce)
    {
        base.KillWithForce(dir, ragdForce);
        GameManager.instance.GameOver();
    }

    public Vector3 NearbyPositionAtLayer()
    {

        Vector3 sp = UnityEngine.Random.insideUnitSphere * this.distance;

        sp += this.transform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(sp, out hit, this.distance, NavMesh.GetAreaFromName(Constant.LAYERS.ENVTERRAIN));
        return hit.position;

    }


    public override void Start()
    {
        base.Start();
        this.isPlayer = true;
    }

    public override void OnAttacked(float damage,GameObject attacker)
    {
        base.OnAttacked(damage, attacker);
        GameManager.instance.gameplayHUD.FillPlayerHealthBar(this.health, this.totalhealth);
        if (!IsInvoking("FIllHealth"))
            Invoke("FIllHealth", 1f);
        //  GameManager.instance.gameplayHUD.ShowHurtEffect();
    }

    void FIllHealth()
    {
        this.OnAttacked(-1f, null);
        if (!IsInvoking("FIllHealth"))
            Invoke("FIllHealth", 1f);
    }

    public void OnWeaponChange(Weapon weapon)
    {
        bool isWeapon = weapon != null;
        this.animatorController.SetWeapon(weapon ? weapon.weaponType : WeaponType.None);
        //  GameManager.instance.mainCamera.ChangeCameraMode(weapon ? CameraMode.Aim : CameraMode.Orbit);

        //GameManager.instance.gameplayHUD.ToggleCrosshair(weapon != null);
        GameManager.instance.gameplayHUD.shootingJoyStick.gameObject.SetActive(isWeapon);
        this.meleeFightHandler.canMeleeFight = !isWeapon;
        GameManager.instance.gameplayHUD.ToggleMeleeFightControls(!isWeapon);
        GameManager.instance.gameplayHUD.ToggleCloseAimingButton(isWeapon);
    }

    public bool CanAim
    {
        get
        {
            return this.weaponInventory.CurrentWeapon != null;
        }
    }
}
