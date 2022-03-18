using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTSSizer : MonoBehaviour
{
    public TSMainManager tsManager;
    public float laneSize = 1f;

    [SerializeField]
    private TSLaneInfo[] lanes;

    [SerializeField]
    private TSLaneInfo[] newLanes;

    public Vector3 offsetToLanes = new Vector3(0f, 0f, 0f);

    public void Start()
    {
        this.lanes = this.tsManager.lanes;
    }

    public void SizeLanes()
    {
        if (this.lanes == null)
            this.Start();

        for(int i=0;i<this.lanes.Length;i++)
        {
            this.lanes[i].laneWidth = this.laneSize;
        }
    }

    public void AddOffsetToNewLanes()
    {
        for(int i=0;i<this.tsManager.lanes.Length;i++)
        {
            this.lanes[i].conectorA += this.offsetToLanes;
            this.lanes[i].conectorB += this.offsetToLanes;
        }
    }
}
