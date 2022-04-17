using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InGamePanel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


    public void TogglePanel(bool toggle)
    {
        this.gameObject.SetActive(toggle);
    }
}
