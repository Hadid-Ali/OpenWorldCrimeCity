using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorSceneHandler
{
    private static string splashScenePath = "Assets/Scene/Splash.unity";
    private static string mainmenuScenePath = "Assets/Scene/MainMenu.unity";
    private static string gameplayScenePath = "Assets/Scene/day.unity";

    [MenuItem("SceneHandler/Open Splash Scene _F1")]
    static void OpenSplashScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(splashScenePath);
        }
    }

    [MenuItem("SceneHandler/Open MainMenu Scene _F3")]
    static void OpenMainMenuScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(mainmenuScenePath);
        }
    }

    [MenuItem("SceneHandler/Open Gameplay Scene _F4")]
    static void OpenGameplayScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(gameplayScenePath);
        }
    }

    [MenuItem("SceneHandler/PlayStop _F5")]
    private static void PlayStopButton()
    {
        if (!EditorApplication.isPlaying)
        {
            bool value = EditorApplication.SaveCurrentSceneIfUserWantsTo();
            if (value)
            {
                EditorApplication.OpenScene(splashScenePath);
                EditorApplication.ExecuteMenuItem("Edit/Play");
            }
        }

    }


    [MenuItem("SceneHandler/Pause _F6")]
    private static void PauseButton()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.ExecuteMenuItem("Edit/Pause");
        }
    }
}