using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoadCheckpoint : Checkpoint
{
    public string SceneName = "";

    public override void Action()
    {
        base.Action();
        SceneManager.LoadScene(this.SceneName);
    }
}
