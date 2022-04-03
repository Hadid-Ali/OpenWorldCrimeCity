using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathDrawer : MonoBehaviour
{
    public Transform targetToFollow;

    private Transform _selfTransform;
    private NavMeshPath navMeshPath;

    [SerializeField]
    private LineRenderer _lineRenderer;

    public float offset = 1.5f;
    public float pointOffset = 1.5f;

    private void Awake()
    {
        this._selfTransform = this.transform;
        navMeshPath = new NavMeshPath();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.CalculatePath();
        this.DrawPath();
    }

    public void CalculatePath()
    {
        //NavMesh.CalculatePath(this._selfTransform.position+ new Vector3(0,0.5f,0f), this.targetToFollow.position, NavMesh.AllAreas, this.navMeshPath);
        int roadMask = 1 << NavMesh.GetAreaFromName("Roads");

        NavMesh.CalculatePath(this._selfTransform.position + new Vector3(0,this.offset, 0f), this.targetToFollow.position, roadMask, this.navMeshPath);
    }
    
    public void DrawPath()
    {
        if (this.navMeshPath.corners.Length <= 0)
            return;

        this._lineRenderer.SetVertexCount(this.navMeshPath.corners.Length);

        Vector3[] points = new Vector3[this.navMeshPath.corners.Length];

        for(int i=0;i<this.navMeshPath.corners.Length;i++)
        {
            points[i] = this.navMeshPath.corners[i] + new Vector3(0f, this.pointOffset, 0f);
        }

        this._lineRenderer.SetPositions(points);
    }
}
