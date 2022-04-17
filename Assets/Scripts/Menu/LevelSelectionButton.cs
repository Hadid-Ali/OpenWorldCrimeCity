using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField]
    private GameObject lockedImage;

    [SerializeField]
    private GameObject selectedImage;

    [SerializeField]
    private int levelIndex;


    private void Start()
    {
        this.CheckLevelLocking();
    }

    public void SetUnlocked()
    {
        this.lockedImage.SetActive(false);
    }

    public bool IsLocked => PreferenceManager.CurrentLevel < this.levelIndex;

    public void CheckLevelLocking()
    {
        Debug.LogError($"Level Index : { PreferenceManager.CurrentLevel}");
        if (this.IsLocked)
        {
            this.lockedImage.SetActive(true);
        }
    }

    public void LevelButtonPress()
    {
        Constant.GameplayData.currentLevel = this.levelIndex;
        this.ToggleIsSelected(true);

    }

    public void ToggleIsSelected(bool toggle)
    {
        if (this.selectedImage)
            this.selectedImage.SetActive(toggle);
    }
}
