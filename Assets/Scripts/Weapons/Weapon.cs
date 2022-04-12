using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WEAPON
{
    M4A1,
    REVOLVER,
    PISTOL,
    ROCKETLAUNCHER,
    KNIFE,
    NONE,
    SWORD
}

public enum WeaponType
{
    Rifle = 0,
    Pistol = 1,
    RocketLauncher,
    Sniper,
    Melee,
    None
}

public enum AttackMode
{
    AUTOMATIC,
    BURST_SHOT,
    MELEE
}


public class Weapon : GameEntity
{

    public GameObject weaponObject;
    public float hitDamage;
    public WEAPON weaponName;
    public AttackMode weaponAttackMode;
    public WeaponType weaponType;

    public bool isEquippedByPlayer = false;

    public AudioClip weaponSound;

    protected AudioSource weaponSource;


    public virtual void Start()
    {
        if (this.GetComponent<AudioSource>())
            this.weaponSource = this.GetComponent<AudioSource>();
    }

    public virtual void PlayWeaponAudio()
    {
        if(this.weaponSource)
        {
            if(this.weaponSound!=null)
            {
                this.weaponSource.PlayOneShot(this.weaponSound);
            }
        }
    }

    public virtual void OnWeaponSelect(WeaponInventory weaponInventory)
    {

    }

    public virtual void OnStopShooting()
    {

    }

    public virtual bool CanShoot => true;

    public void Shoot()
    {
        if(this.CanShoot)
        {
            this._ShootWeapon();
        }
    }

    protected virtual void _ShootWeapon()
    {
        GameManager.instance.cameraManager.SetCameraShake();
    }
}
