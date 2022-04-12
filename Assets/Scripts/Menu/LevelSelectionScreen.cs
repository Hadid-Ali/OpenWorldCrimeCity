using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionScreen : MonoBehaviour
{
    public LevelSelectionButton[] levelSelectionButtons;

    private int currentIndex = -100;

    private void Start()
    {
        int prevIndex = 1;

        for(int i=0;i<this.levelSelectionButtons.Length;i++)
        {
            if(!this.levelSelectionButtons[i].IsLocked)
            {
                prevIndex = i;
            }
        }

        this.levelSelectionButtons[prevIndex].LevelButtonPress();
    }

    public void LevelButtonPress(int levelIndex)
    {
        if (this.levelSelectionButtons[levelIndex -1].IsLocked)
            return;

        if (this.currentIndex >= 0)
        {
            this.levelSelectionButtons[this.currentIndex].ToggleIsSelected(false);
        }

        this.currentIndex = levelIndex - 1;
        this.levelSelectionButtons[this.currentIndex].LevelButtonPress();
    }
}
