using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChestController : MonoBehaviour
{
    [Inject] private TimerController _timerController;

    public void SetupChestContent()
    {
        Debug.Log("Player will recieve " + _timerController.Timer);
    }

    public void OpenChest()
    {
        Debug.Log("Player recieves " + _timerController.Timer + " gold");
    }
}
