using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceManager : MonoBehaviour
{
    const string _farmOwned = "farmOwned";
    const string _farmersWorking = "farmersWorking";
    const string _cropSeedsOwned = "cropSeedsOwned";
    const string _unlockedCrop = "unlockedCrop";
    const string _ownedDrugs = "ownedDrugs";
    const string _productionRate = "productionRate";
    const string _tractorsOwned = "tractorsOwned";

    const string _cashAmount = "Cash";
    const string _levelIndex = "levelIndex";
    const string _clearedLevels = "clearedLevels";
    const string _currentLevel = "currentLevel";
    

    public static int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt(_levelIndex);
        }

        set
        {
            PlayerPrefs.SetInt(_levelIndex, value);
        }
    }

    public static float CashBalance
    {
        set
        {
            PlayerPrefs.SetFloat(_cashAmount, value);
        }

        get
        {
            return PlayerPrefs.GetFloat(_cashAmount);
        }
    }

    public void ResetFarmElementsOfWorking(string farmName,string cropName)
    {
        SetFarmersOnFarms(farmName, 0);
        SetTractorsOwnedAtFarm(farmName, 0);
        SetSeedsOwnedAtFarm(farmName, cropName, 0);
    }

    public static bool IsFarmOwned(string farmName)
    {
        return PlayerPrefs.GetInt(string.Format("{0}{1}", farmName, _farmOwned)) == 1;
    }

    public static void SetFarmOwned(string farmName)
    {
        PlayerPrefs.SetInt(string.Format("{0}{1}", farmName, _farmOwned), 1);
    }

    public static int GetFarmersWorkingOnFarm(string farmName)
    {
        return PlayerPrefs.GetInt(string.Format("{0}{1}", farmName, _farmersWorking));
    }

    public static void SetFarmersOnFarms(string farmName,int count)
    {
        PlayerPrefs.SetInt(string.Format("{0}{1}", farmName, _farmersWorking), count);
    }

    public static void SetSeedsOwnedAtFarm(string cropName,string farmName,int Count)
    {
        PlayerPrefs.SetInt(string.Format("{0}{1}{2}",farmName,cropName,_cropSeedsOwned),Count);
    }

    public static int GetCropSeedsOwned(string cropName,string farmName)
    {
        return PlayerPrefs.GetInt(string.Format("{0}{1}{2}", farmName, cropName, _cropSeedsOwned));
    }


    public static void SetTractorsOwnedAtFarm(string farmName, int Count)
    {
        PlayerPrefs.SetInt(string.Format("{0}{1}", farmName, _tractorsOwned), Count);
    }

    public static int GetTractorsOwned(string farmName)
    {
        return PlayerPrefs.GetInt(string.Format("{0}{1}", farmName, _tractorsOwned));
    }

    public static bool IsCropUnlocked(string cropName)
    {
        return PlayerPrefs.GetInt(string.Format("{0}{1}", cropName, _unlockedCrop)) == 1;
    }

    public static int GetDrugsOwned(string drugName)
    {
        return PlayerPrefs.GetInt(string.Format("{0}{1}", drugName, _ownedDrugs));
    }

    public static void SetDrugsOwned(string drugName,int quantity)
    {
        PlayerPrefs.SetInt(string.Format("{0}{1}", drugName, _ownedDrugs), quantity);
    }

    #region For Weapon
    public static bool GetWeaponLockedStatus(string weaponName)
    {
        return PlayerPrefs.GetInt(weaponName, 2) > 0;
    }
    public static int GetWeaponRAdCount(string weaponName)
    {
        return PlayerPrefs.GetInt(weaponName, 2);
    }
    public static void SetWeaponLockedStatus(string weaponName, int rAdCount = 0)
    {
        PlayerPrefs.SetInt(weaponName, rAdCount);
    }
    #endregion
}