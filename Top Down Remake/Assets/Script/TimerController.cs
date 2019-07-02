using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerController : MonoBehaviour
{
    [Inject] private SceneController _sceneController;

    public Text TimerText;
    
    private bool finished;

    void Update()
    {
        if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Village)
        {
            TimerText.gameObject.SetActive(false);
            return;
        }
        else if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Dungeon)
        {
            TimerText.gameObject.SetActive(true);

            if (finished)
                return;

            string minutes = ((int)Time.timeSinceLevelLoad / 60).ToString();
            string seconds = (Time.timeSinceLevelLoad % 60).ToString("f0");

            TimerText.text = minutes + ":" + seconds;
        }
    }

    public void Finish()
    {
        finished = true;
        TimerText.color = Color.yellow;
    }
}
