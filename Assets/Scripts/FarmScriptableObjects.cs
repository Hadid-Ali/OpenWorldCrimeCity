using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Farmland",menuName ="Create Farm",order = 1)]
public class FarmScriptableObjects :    ScriptableObject
{
    public FarmAreaName farmName;
    public Drugs farmCultivatingDrug;
    public float farmLandPrice,
        amountToPurchaseSeeds,
        amountToHireFarmers,
        requiredFarmers,
        requiredVehicles,
        amountToHireTractors,
        cultivationHours,
        farmingSpeed = 1f,
        requiredFarmingHours = 5f,
        requiredTractorAm;


}
