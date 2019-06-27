using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
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
        SceneManager.LoadScene(sceneIndex);
    }
}
