using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private Transform[] points;

    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (this.points.Length <= 0)
            return;

        this.DrawPath(this.points);
    }

    public void DrawPath(Transform [] transformPoints)
    {
        Vector3[] vectors = new Vector3[transformPoints.Length];

        for (int i = 0; i < transformPoints.Length; i++)
        {
            vectors[i] = transformPoints[i].position;
        }
        this.DrawPath(vectors);

    }

    public void DrawPath(Vector3 [] drawPathPoints)
    {
        if (drawPathPoints.Length <= 0)
            return;

        this._lineRenderer.SetVertexCount(drawPathPoints.Length);
        this._lineRenderer.SetPositions(drawPathPoints);
    }
}
