using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{

    void Start()
    {
        Invoke("LoadScene", 2);
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}