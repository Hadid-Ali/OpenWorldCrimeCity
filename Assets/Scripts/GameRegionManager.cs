using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRegionManager : MonoBehaviour
{
    public List<GameRegion> allWaves = new List<GameRegion>();

    private List<Vector3> positions = new List<Vector3>();

    public bool canFight = false;
    
     int count = 0;

    public int Count
    {
        get
        {
            return this.count;

        }
        set
        {
            this.count = value;
            if(this.count>=5)
            {
                for(int i=0;i<this.allWaves.Count;i++)
                {
                    this.allWaves[i].gameObject.SetActive(true);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.CopyPositions(ref this.positions);
    }

    public void CopyPositions(ref List<Vector3> list)
    {
        for(int i=0;i< this.transform.childCount;i++)
        {
            list.Add(this.transform.GetChild(i).transform.position);
        }
    }
}
