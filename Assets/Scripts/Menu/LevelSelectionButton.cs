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
        if(this.IsLocked)
        {
            this.lockedImage.SetActive(true);
        }
    }

    public void SetUnlocked()
    {
        this.lockedImage.SetActive(false);
    }

    public bool IsLocked => PreferenceManager.CurrentLevel < this.levelIndex;

    public void CheckLevelLocking()
    {

    }

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
