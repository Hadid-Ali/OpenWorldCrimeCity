using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    public GameObject selectedImage;
    public int levelIndex;

    public void LevelButtonPress()
    {
        Constant.GameplayData.currentLevel = this.levelIndex;
        this.ToggleIsSelected(true);

    }

    public void ToggleIsSelected(bool toggle)
    {
        this.selectedImage.SetActive(toggle);
    }
}
