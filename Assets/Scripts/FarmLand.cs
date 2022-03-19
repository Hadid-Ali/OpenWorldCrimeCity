using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmAreaName
{
    SOMERVILLE,CALIFORNIATEXASFARM,SOUTHVILLEFARM
}

public enum Drugs
{
    MARIJUANA,COCAINE,NICOTINE,ICEMETH
}

public enum FarmElements
{
    FARMERS,CROPS,TRACTORS
}

public class FarmLand : MonoBehaviour
{
    public List<GameObject> cropsObject, farmers, farmingVehicles;
    public Vector2 growingRange = new Vector2(-4.39f, 4f);

    public FarmAreaName farmName;
    public Drugs drugType;

    public float farmingSpeed = 0f;
    public float requiredFarmers = 20f;
    [Header("Hours to Grow Farm from Start to Cultivation")]
    public float farmGrowTime = 4f;   

    public bool isLandCultivating = false;

    public bool testFarmeres, testCrops, testTractors;

    private void Start()
    {
        bool isLandUnlocked = PreferenceManager.IsFarmOwned(this.farmName.ToString());
        bool isCultivationGoing = PreferenceManager.GetFarmersWorkingOnFarm(this.farmName.ToString()) > 0;
        this.isLandCultivating = isLandUnlocked & isCultivationGoing;

        if (this.testFarmeres)
            this.ToggleFarmElements(true, FarmElements.FARMERS);

        if (this.testCrops)
            this.ToggleFarmElements(true, FarmElements.CROPS);

        if (this.testTractors)
            this.ToggleFarmElements(true, FarmElements.TRACTORS);
    }
    

    public void ToggleFarmElements(bool b,FarmElements farmElement)
    {
        List<GameObject> elements = farmElement == FarmElements.CROPS ? this.cropsObject : farmElement == FarmElements.FARMERS ? this.farmers : this.farmingVehicles;
        for(int i=0;i<elements.Count;i++)
        {
            elements[i].SetActive(b);
        }
    }
    
}
