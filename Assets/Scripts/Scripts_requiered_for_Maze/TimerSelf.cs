using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSelf : MonoBehaviour
{
    [SerializeField] TextModifier timerText;
    private float timerTime;

    void LateUpdate()
    {
        timerTime += Time.deltaTime;
        timerText.UpdateText(FormatTime());
    }

    public string EndTimer()
    {
        enabled = false;
        return FormatTime();
    }

    private string FormatTime()
    {
        int hours = (int)(timerTime / 3600);
        int minutes = (int)((timerTime % 3600) / 60);
        int seconds = (int)(timerTime % 60);
        int milliseconds = (int)((timerTime * 1000) % 1000);

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
    }
}