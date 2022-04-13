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

public enum WeaponSoundType
{
    MAGAZINE_OUT,
    MAGAZINE_IN,
    RELOAD,
    SHOOT
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

    [SerializeField]
    protected AudioClip magazineOutSound;

    [SerializeField]
    protected AudioClip magazineInSound;

    [SerializeField]
    protected AudioClip reloadSound;

    [SerializeField]
    protected bool hasCameraShake = false;

    public virtual void Start()
    {
        if (this.GetComponent<AudioSource>())
            this.weaponSource = this.GetComponent<AudioSource>();
    }

    protected void PlayWeaponSound(AudioClip audio)
    {
        if (this.weaponSource)
        {
            if (audio != null)
            {
                this.weaponSource.PlayOneShot(audio);
            }
        }
    }

    public void PlayWeaponSound(WeaponSoundType soundType)
    {
        switch(soundType)
        {
            case WeaponSoundType.MAGAZINE_OUT:
                this.PlayWeaponSound(this.magazineOutSound);
                break;

            case WeaponSoundType.MAGAZINE_IN:
                this.PlayWeaponSound(this.magazineInSound);
                break;

            case WeaponSoundType.RELOAD:
                this.PlayWeaponSound(this.reloadSound);
                break;
        }    
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
        if(this.hasCameraShake)
        {
            GameManager.instance.cameraManager.SetCameraShake();
        }   
    }
}
