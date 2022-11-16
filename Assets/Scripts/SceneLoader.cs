using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    private static string sceneToLoad;

    public static string SceneToLoad { get => sceneToLoad; }

    // load
    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // progress load
    public static void ProgressLoad(string sceneName)
    {
        sceneToLoad = sceneName;
        Debug.Log("LoadingProgress");
        SceneManager.LoadScene("LoadingProgress");
    }
    // reload level
    public static void ReloadLevel()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        ProgressLoad(currentScene);
    }

    // load next level
    public static void LoadNextLevel()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        var nextLevel = int.Parse(currentSceneName.Split("Maze3D")[1]) + 1;
        string nextSceneName = "Maze3D" + nextLevel;

        if (SceneUtility.GetBuildIndexByScenePath(nextSceneName) == -1)
        {
            Debug.LogError(nextSceneName + " does not exists!");
            SceneLoader.Load("MainMenu");
            return;
        }

        ProgressLoad(nextSceneName);

    }
}
