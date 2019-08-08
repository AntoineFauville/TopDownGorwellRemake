using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerController : MonoBehaviour
{
    [Inject] private SceneController _sceneController;
    [Inject] private TimerVillagerView _timerVillagerView;
    [Inject] private SavingController _savingController;
    [Inject] private GameSettings _gameSettings;

    public Text TimerText;
    
    private bool finished;

    public int Timer;

    void Update()
    {
        if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Village)
        {
            //don't display the timer on top of the screen
            TimerText.gameObject.SetActive(false);

            //activate the character
            _timerVillagerView.CharacterImage.gameObject.SetActive(true);

            //check if player enters the house if yes then display the canvas description, otherwise don't
            if (_timerVillagerView.ColliderTriggerArea.IsTriggeredByPlayer)
            {
                _timerVillagerView.CanvasDescription.enabled = true;
            }
            else
            {
                _timerVillagerView.CanvasDescription.enabled = false;
            }

            //make sure to display the correct timers from the playerprefs
            UpdateView();

            //stop here since we don't care about dungeon
            return;
        }
        else if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Dungeon)
        {
            //enable the timer in the dungeon
            TimerText.gameObject.SetActive(true);

            //don't display the stuff from the village
            _timerVillagerView.CharacterImage.gameObject.SetActive(false);
            _timerVillagerView.CanvasDescription.enabled = false;

            

            //set the timer that will be read by the chest system to get the bonus gold etc
            Timer = (int)Time.timeSinceLevelLoad;

            string minutes = ((int)Time.timeSinceLevelLoad / 60).ToString();
            string seconds = (Time.timeSinceLevelLoad % 60).ToString("f0");

            if (Timer > _gameSettings.MaxTimingInDungeon)
                Timer = _gameSettings.MaxTimingInDungeon;

            TimerText.text = minutes + ":" + seconds;

            if (finished)
            {
                SaveTimer(Timer);
                return;
            }
        }
    }

    public void Finish()
    {
        finished = true;
        TimerText.color = Color.yellow;
    }

    //adjust the view in the village mode loading correct times from the saving system
    void UpdateView()
    {
        string minutes;
        string seconds;

        // 1 best time
        minutes = (_savingController.GetPlayerPrefInt(_gameSettings.BestTime) / 60).ToString();
        seconds = (_savingController.GetPlayerPrefInt(_gameSettings.BestTime) % 60).ToString("f0");

        _timerVillagerView.BestTime.text = minutes + ":" + seconds;

        // 2 previous time
        minutes = (_savingController.GetPlayerPrefInt(_gameSettings.PreviousTime) / 60).ToString();
        seconds = (_savingController.GetPlayerPrefInt(_gameSettings.PreviousTime) % 60).ToString("f0");

        _timerVillagerView.PreviousTime.text = minutes + ":" + seconds;
        
    }

    void SaveTimer(int newTimer)
    {
        //in all case I overwrite when saving the last time in the dungeon.
        _savingController.SetPlayerPrefInt(_gameSettings.PreviousTime, newTimer);

        //if you beat your best score then it overwrite the best time.
        if (newTimer <= _savingController.GetPlayerPrefInt(_gameSettings.BestTime))
        {
            _savingController.SetPlayerPrefInt(_gameSettings.BestTime, newTimer);
        }
    }
}
