using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSelf : MonoBehaviour
{
    [SerializeField] TextModifier timerText;

    private float timerTime;


    // Update is called once per frame
    void LateUpdate()
    {
        timerTime += Time.time;

        timerText.UpdateText(FormatTime());
    }

    public string endTimer()
    {
        gameObject.SetActive(false);
        return FormatTime();
    }

    private string FormatTime()
    {
        float seconds = timerTime / 1000;

        int hours = (int)(seconds / 3600);
        int minutes = (int)((seconds % 3600) / 60);
        int secondsInt = (int)(seconds % 60);
        int milliseconds = (int)((seconds - (int)seconds) * 1000);
        int roundedMilliseconds = milliseconds / 10;

        return $"{hours:D2}:{minutes:D2}:{secondsInt:D2}.{roundedMilliseconds:D1}";
    }
}
