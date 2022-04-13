using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootingWeapon : Weapon
{
    public bool useAmmoMechanism = false;

    public int ammoCount = 50;
    public int ammoCappacity = 200;

    public int magazineCount = -100;

    public bool hasUnlimitedAmmo = true;

    private int _currentMagazineAmmo;

    public float shootingRate;
    public float weaponReloadingTime = 2f;

    public GameObject muzzle,shellPoint,magazineObject,holsterObject;

    private float nextShot = 0.0f;
    private bool isShot = false;
    protected bool _isReloading = false;
    public AudioClip dryShot;

    public ShootingMechanism shootingPlayer;

    private Transform _shellPointTransform;
    private Transform _selfTransform;

    [SerializeField]
    protected bool hasWeaponShell = false;

    private void OnEnable()
    {
        if(this.holsterObject)
        {
            this.holsterObject.SetActive(false);
        }
    }

    public override void Start()
    {
        base.Start();

        this._shellPointTransform = this.shellPoint.transform;
        this._selfTransform = this.transform;

        this.CacheMagazine();
    }

    public void CacheMagazine()
    {
        this._currentMagazineAmmo = this.magazineCount;
    }

    public virtual void Fire()
    {

    }

    public override void OnWeaponSelect(WeaponInventory weaponInventory)
    {
        Debug.LogError("On Weapon Select");
        this.shootingPlayer = weaponInventory;
    }

    public void FullMagazine()
    {
        this.ammoCount = Mathf.Clamp(this.ammoCount + 100, 0, this.ammoCappacity);
    }

    public void DryShot()
    {
        if (this.dryShot != null)
        this.weaponSource.PlayOneShot(this.dryShot);
        GameManager.instance.gameplayHUD.GiveNoBulletMessage();
    }

    public virtual void ReloadWeapon()
    {
        if (this.shootingPlayer == null)
            return;

        this._isReloading = true;
        this.shootingPlayer.Reload();
        this.CacheMagazine();
        Invoke("OnWeaponReloaded", this.weaponReloadingTime);
    }

    public virtual void OnWeaponReloaded()
    {
        this.shootingPlayer.OnWeaponReloaded();
        this._isReloading = false;
    }

    public bool IsReloading
    {
        get
        {
            return this._isReloading;
        }
    }

    public override bool CanShoot => base.CanShoot & !this._isReloading;

    protected override void _ShootWeapon()
    {
        if (this._currentMagazineAmmo <= 0 && this.magazineCount > 0)
        {
            this.ReloadWeapon();
        }

        if (this.ammoCount <= 0)
        {
            this.DryShot();
            return;
        }


        base._ShootWeapon();
        this.PlayWeaponAudio();

        if(this.hasWeaponShell)
        {
            GameManager.instance.objectPools.shellsPool.Instantiate(this._shellPointTransform.position, this._shellPointTransform.rotation, this._selfTransform);
        }
        if (this.useAmmoMechanism)
        {
            this.ConsumeBullet();
        }


        if (IsInvoking("hideM"))
            CancelInvoke("hideM");

        this.showM();
        Invoke("hideM", 0.2f);
    }

    void showM()
    {
        this.muzzle.SetActive(true);
    }

    void hideM()
    {
        this.muzzle.SetActive(false);
    }

    public override void OnStopShooting()
    {
        base.OnStopShooting();
    }

    public void ConsumeBullet()
    {
        if(this.ammoCount>0)
        {
            if(!this.hasUnlimitedAmmo)
            {
                this.ammoCount -= 1;
            }
            this._currentMagazineAmmo -= 1;
        }
        else
        {

        }
    }
}
