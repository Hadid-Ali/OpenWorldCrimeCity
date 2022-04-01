using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectAfterLoadingComplete : MonoBehaviour
{
    public GameObject[] ObjectsToEnable, ObjectsToDisable;

    private void OnEnable()
    {
        LoadingHandler.OnLoadingCompleteEvent += OnLoadingCompleted;
    }

    private void OnDisable()
    {
        LoadingHandler.OnLoadingCompleteEvent += OnLoadingCompleted;
    }

    private void OnLoadingCompleted()
    {
        foreach (var obj in ObjectsToEnable)
        {
            obj.SetActive(true);
        }
        foreach (var obj in ObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}