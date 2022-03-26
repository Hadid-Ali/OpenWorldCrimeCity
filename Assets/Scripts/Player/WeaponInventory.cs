using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HolsterWeapon
{
    public WEAPON weapon;
    public bool isVisible;
    public GameObject[] weaponObjects;

    public void ToggleWeaponVisibility(bool b)
    {
        this.isVisible = b;
        for(int i=0;i<this.weaponObjects.Length;i++)
        {
            this.weaponObjects[i].SetActive(b);
        }
    }
}

public interface ShootingMechanism
{
    public void Reload();
    public void OnWeaponReloaded();
}

public class WeaponInventory : MonoBehaviour,ShootingMechanism
{

    public List<Weapon> weapons = new List<Weapon>();
    public List<HolsterWeapon> holsterWeapons;

    private PlayerController playerController;

    [SerializeField]
    private Weapon currentWeapon;
        [SerializeField]
 
    private ShootingWeapon shootingWeapon;
    private MeleeWeapon meleeWeapon;
    [SerializeField]
    private int currentWeaponIndex = 0;

    private float nextShot;
    private bool isShot = false;
    private bool _isReloadingWeapon = false;

    private void Start()
    {
        this.playerController = this.GetComponent<PlayerController>();
        this.DrawWeapon(this.weapons[0].weaponName);
    }

    public void ReloadWeapon()
    {
        this.shootingWeapon.ReloadWeapon();
    }

    private void OnEnable()
    {
        if(this.currentWeapon)
            this.playerController.OnWeaponChange(currentWeapon);
    }

    public void EnableMagazine(int flagToggle)
    {
        if (this.shootingWeapon.magazineObject)
            this.shootingWeapon.magazineObject.SetActive(flagToggle == 1);
    }

    public void Reload()
    {
        this.playerController.animatorController.ReloadWeapon();
        this._isReloadingWeapon = true;
    }

    public void OnWeaponReloaded()
    {
        this._isReloadingWeapon = false;
    }

    public void DrawWeapon(WEAPON weapon)
    {
        Debug.LogError("Drawing Weapon");
        if(this.currentWeapon)
        {
            this.currentWeapon.gameObject.SetActive(false);
            this.currentWeapon = null;
        }

        List<Weapon> equippedWeapons = this.weapons.FindAll(x => x.isEquippedByPlayer.Equals(true));

        for(int i=0;i< equippedWeapons.Count;i++)
        {
            if(equippedWeapons[i].weaponName.Equals(weapon))
            {
                this.currentWeapon = equippedWeapons[i];
            }
        }

        if(this.currentWeapon)
        {
            this.currentWeapon.gameObject.SetActive(true);
            this.currentWeapon.OnWeaponSelect(this);
        }

        if(this.currentWeapon is ShootingWeapon)
            this.shootingWeapon = this.currentWeapon.gameObject.GetComponent<ShootingWeapon>();

        this.playerController.OnWeaponChange(currentWeapon);    

    }

    public Weapon CurrentWeapon
    {
        get
        {
            return this.currentWeapon;
        }
    }
    

    public  bool isWeaponInInventory(WEAPON weapon)
    {
        for(int i=0;i<this.weapons.Count;i++)
        {
            if (this.weapons[i].weaponName.Equals(weapon))
                return true;
        }
        return false;
    } 

    public void DrawWeapon(int index)
    {
        this.DrawWeapon(this.weapons[index].weaponName);
    }

    public bool IsCharacter(GameObject G)
    {
        return G.GetComponent<CharacterController>();
    }

    public bool IsVehicle(GameObject g)
    {
        return g.GetComponent<Vehicle>();
    }
    
    private void Update()
    {
        if(GameplayHUD.isShooting & !this._isReloadingWeapon)
        {
            this.AttackWithCurrentWeapon();
            Vector3 v = this.transform.eulerAngles;
            this.transform.eulerAngles = new Vector3(v.x, GameManager.instance.mainCamera.transform.eulerAngles.y, v.z);
        }
        else
        {
            if (this.isShot)
                this.isShot = false;
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            this.SwitchWeapon();
        }
    }

    public void SwitchWeapon(ref bool isSuccessSwitch)
    {
        this.SwitchWeapon();
        isSuccessSwitch = this.currentWeapon != null;
    }

    public void SwitchWeapon()
    {
        int index = this.weapons.FindAll(x => x.isEquippedByPlayer.Equals(true)).Count;

        this.currentWeaponIndex++;
        this.currentWeaponIndex %= this.weapons.Count;

        //if (this.currentWeaponIndex >= index)
        //{
        //    this.currentWeaponIndex = 0;
        //    if (this.currentWeapon)
        //    {
        //        this.currentWeapon.gameObject.SetActive(false);
        //        this.currentWeapon = null;
        //    }
        //    this.DrawWeapon(WEAPON.NONE);
        //}
        //else

        this.DrawWeapon(this.currentWeaponIndex);
    }

    public void DamageTarget()
    {
        if (this.playerController.aimingManager.aimedObject)
        {
            if (this.IsCharacter(this.playerController.aimingManager.aimedObject))
            {
                CharacterController c = this.playerController.aimingManager.aimedObject.GetComponent<CharacterController>();

                c.OnAttacked(this.currentWeapon.hitDamage, this.gameObject);
            }

            else
            {

                if (this.IsVehicle(this.playerController.aimingManager.aimedObject))
                {
                    Vehicle v = this.playerController.aimingManager.aimedObject.GetComponent<Vehicle>();

                    v.ApplyDamage(this.currentWeapon.hitDamage);
                }
            }

            if(this.playerController.aimingManager.aimingAtPoint != Vector3.zero)
            {
                Particles.Instance.ShowParticle(ParticleType.BULLET_HIT, this.playerController.aimingManager.aimingAtPoint);
            }
        }

    }

    public void AttackWithCurrentWeapon()
    {
        if(this.currentWeapon)
        {
            switch(this.currentWeapon.weaponAttackMode)
            {
                case AttackMode.AUTOMATIC:
                    if(Time.time>this.nextShot)
                    {
                        this.currentWeapon.Shoot();
                        this.nextShot = Time.time + this.shootingWeapon.shootingRate;
                        this.DamageTarget();
                    }
                    break;

                case AttackMode.BURST_SHOT:
                    if(!this.isShot)
                    {
                        this.isShot = true;
                        this.currentWeapon.Shoot();
                        this.DamageTarget();
                    }
                    break;

                case AttackMode.MELEE:

                    break;
            }
        }
    }
}
