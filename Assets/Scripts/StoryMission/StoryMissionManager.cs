using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMissionManager : MonoBehaviour
{
    //Later to be shifted from resources
    public List<string> missions;

    private LevelData currentMission;

    public string levelFolderPrefix = "Levels\\";

    public void Start()
    {
        GameObject G = (GameObject) Instantiate(Resources.Load(string.Format("{0}{1}", levelFolderPrefix, this.missions[PreferenceManager.CurrentLevel])));

        this.currentMission = G.GetComponent<LevelData>();
    }

}
