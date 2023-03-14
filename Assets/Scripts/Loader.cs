using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Loader
{
    public enum Scene
    {
        MainMenu,
        LoadingScene,
        GameScene
    }

    private static Scene targetScene;
    // Start is called before the first frame update
    public static void LoadScene(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        
    }
    public static void LoaderCallback()
    {
        SceneManager.LoadSceneAsync(targetScene.ToString());
    }

}
