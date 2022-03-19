using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmsManager : MonoBehaviour
{
    public List<FarmScriptableObjects> farms;
    
    public float RequestFarmPrice(FarmAreaName farmName)
    {
        return this.Farm(farmName).farmLandPrice;
    }

    public float RequestFarmersForFarm(FarmAreaName farmName)
    {
        return this.Farm(farmName).amountToHireFarmers;
    }

    public float RequestTractorsForFarm(FarmAreaName farmName)
    {
        return this.Farm(farmName).amountToHireTractors;
    }

    public float RequestSeedsForFarm(FarmAreaName farmName)
    {
        return this.Farm(farmName).amountToPurchaseSeeds;
    }

    public void PayFarmersForFarm(FarmAreaName farmName)
    {
        float farmersAmount = this.RequestFarmersForFarm(farmName);

        if(PreferenceManager.CashBalance>=farmersAmount)
        {
            PreferenceManager.CashBalance -= farmersAmount;
            PreferenceManager.SetFarmersOnFarms(farmName.ToString(), (int)this.Farm(farmName).requiredFarmers);

            GameLoopUI.instance.ShowMessage("You got farmers at your land! Cultivation Started", "Greate", UIMessageType.OK_Message);
        }

        else
        {
            float cashDeficit = farmersAmount - PreferenceManager.CashBalance;

            GameLoopUI.instance.ShowMessage(string.Format("You deficit {0}$ to hire farmers at your land", cashDeficit),"Insufficient Cash", UIMessageType.OK_Message);
        }
    }

    public void PurchaseSeedsForFarm(FarmAreaName farmName)
    {
        float seedsAmount = this.RequestSeedsForFarm(farmName);

        if (PreferenceManager.CashBalance >= seedsAmount)
        {
            PreferenceManager.CashBalance -= seedsAmount;
            PreferenceManager.SetSeedsOwnedAtFarm(this.Farm(farmName).farmCultivatingDrug.ToString(), farmName.ToString(), (int)this.Farm(farmName).requiredFarmers);

            GameLoopUI.instance.ShowMessage("You got farmers at your land! Cultivation Started", "Greate", UIMessageType.OK_Message);
        }

        else
        {
            float cashDeficit = seedsAmount - PreferenceManager.CashBalance;

            GameLoopUI.instance.ShowMessage(string.Format("You deficit {0}$ to buy seeds", cashDeficit), "Insufficient Cash", UIMessageType.OK_Message);
        }
    }

    public void PurchaseTractorsForFarm(FarmAreaName farmName)
    {
        float tractorsAmount = this.RequestTractorsForFarm(farmName);

        if (PreferenceManager.CashBalance >= tractorsAmount)
        {
            PreferenceManager.CashBalance -= tractorsAmount;
            PreferenceManager.SetTractorsOwnedAtFarm(farmName.ToString(), (int)this.Farm(farmName).requiredVehicles);

            GameLoopUI.instance.ShowMessage("You got farmers at your land! Cultivation Started", "Greate", UIMessageType.OK_Message);
        }

        else
        {
            float cashDeficit = tractorsAmount - PreferenceManager.CashBalance;

            GameLoopUI.instance.ShowMessage(string.Format("You deficit {0}$ to buy seeds", cashDeficit), "Insufficient Cash", UIMessageType.OK_Message);
        }
    }


    public void PurchaseFarm(FarmAreaName farmName)
    {
        float farmPrice = this.RequestFarmPrice(farmName);

        if (PreferenceManager.CashBalance >= farmPrice)
        {
            PreferenceManager.CashBalance -= farmPrice;
            PreferenceManager.SetFarmOwned(farmName.ToString());

            GameLoopUI.instance.ShowMessage(farmName + " Purchased", "Farm Purchased", UIMessageType.OK_Message);
        }

        else
        {
            float cashDeficit = farmPrice - PreferenceManager.CashBalance;

            GameLoopUI.instance.ShowMessage(string.Format("Your Cash is deficit {0}$ to buy {1} farm",cashDeficit, farmName), "Insufficient Cash", UIMessageType.OK_Message);
        }
    }

    public float GetRequiredFarmers(FarmAreaName farmName)
    {
        return this.Farm(farmName).amountToHireFarmers;
    }

    public FarmScriptableObjects Farm(FarmAreaName farmAreaName)
    {
        return this.farms.Find(x => x.farmName.Equals(farmAreaName));
    }
}
