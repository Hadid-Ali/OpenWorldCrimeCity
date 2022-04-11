using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionScreen : MonoBehaviour
{
    public LevelSelectionButton[] levelSelectionButtons;

    private int currentIndex = -100;

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
