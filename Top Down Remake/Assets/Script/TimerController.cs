using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text TimerText;

    private float startTime;
    private bool finished;

    void Update()
    {
        if (finished)
            return;

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        TimerText.text = minutes + ":" + seconds;
    }

    public void Finish()
    {
        finished = true;
        TimerText.color = Color.yellow;
    }
}
