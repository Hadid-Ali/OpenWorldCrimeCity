using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTargetWithDistance : MonoBehaviour
{
    private DistanceCalculator _distanceCalculator;

    void Awake()
    {
        this._distanceCalculator = GameManager.instance.DistanceCalculator;   
    }

    private void OnEnable()
    {
        this._distanceCalculator.EnableDistanceCalculator();
    }

    private void OnDisable()
    {
        this._distanceCalculator.DisableDistanceCalculator();
    }

    private void Update()
    {
        this._distanceCalculator.CalculateDistanceAndShow(this.transform.position);
    }

}
