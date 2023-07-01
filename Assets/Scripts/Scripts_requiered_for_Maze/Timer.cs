using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextModifier timerText;

    private float timerTime;


    // Update is called once per frame
    void LateUpdate()
    {
        timerTime += Time.time;

        timerText.UpdateText(Math.Round(timerTime/1000,1).ToString());
    }
}
