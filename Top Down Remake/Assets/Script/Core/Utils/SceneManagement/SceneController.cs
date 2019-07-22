using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneController : MonoBehaviour
{
    [Inject] private LoadingScreenFactory _loadingScreenFactory;

    public string GetActiveSceneName()
    {
        string sceneName;

        sceneName = SceneManager.GetActiveScene().name;

        return sceneName;
    }

    public int GetActiveSceneIndex()
    {
        int sceneIndex;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        return sceneIndex;
    }

    public void LoadScene(int sceneIndex)
    {
        _loadingScreenFactory.CreateLoadingScreen();
        SceneManager.LoadScene(sceneIndex);
    }
}
