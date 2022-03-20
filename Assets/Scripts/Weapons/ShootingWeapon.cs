using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootingWeapon : Weapon
{
    public bool useAmmoMechanism = false;

    public int ammoCount = 50;
    public int ammoCappacity = 200;

    public float shootingRate;

    public GameObject muzzle,shell,shellPoint;

    private float nextShot = 0.0f;
    private bool isShot = false;
    public AudioClip dryShot;

    public virtual void Fire()
    {

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

    public override void Shoot()
    {
        if (this.ammoCount <= 0)
        {
            this.DryShot();
            return;
        }
        Debug.Log("Shoot");
        this.PlayWeaponAudio();
        base.Shoot();
        Instantiate(this.shell, this.shellPoint.transform.position, this.shell.transform.rotation, this.transform);
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
            this.ammoCount -= 1;
        }
        else
        {

        }
    }
}
