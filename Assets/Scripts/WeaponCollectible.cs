using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectible : Collectible
{
    public WEAPON weaponToGet;


    public bool showGizmo = true;
    public float width = 20f;

    public Color color = Color.green;

    public override void OnObjectCollected()
    {
        Weapon weapon = GameManager.instance.playerController.weaponInventory.weapons.Find(x => x.weaponName.Equals(this.weaponToGet));
        if (!weapon.isEquippedByPlayer)
            weapon.isEquippedByPlayer = true;
        else
            weapon.GetComponent<ShootingWeapon>().FullMagazine();


        GameManager.instance.playerController.weaponInventory.DrawWeapon(weapon.weaponName);

        #region WaveSpawningOnCollection
        int y= Random.Range(0, 15);

        EntityType entityType = y < 5 ? EntityType.ZOMBIE : y > 5 & y < 10 ? EntityType.MAFIA : EntityType.COP;
        Difficulty difficulty = Random.Range(0, 10) < 5 ? Difficulty.Easy : Difficulty.Medium;

        if(!GameManager.instance.isfirstWeaponSelected)
        {
            entityType = EntityType.ZOMBIE;
            difficulty = Difficulty.Easy;
        }

        StartCoroutine(GameManager.instance.playerWavesSpawner.StartWaveRoutine(entityType, difficulty));
        //GameManager.instance.playerWavesSpawner.StartWave(entityType, difficulty);
        GameManager.instance.isfirstWeaponSelected = true;
        #endregion

        GameManager.instance.regionManager.canFight = true;
        base.OnObjectCollected();
    }


        
    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = this.color;
            Gizmos.DrawSphere(this.transform.position, this.width);
        }
    }
}
