using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class used to for The timer
public class TimerSelf : MonoBehaviour
{
    //TextModifier refers to a script which modifies the text on a specific textmeshpro
    [SerializeField] TextModifier timerText;

    //interne reppresentation of the time
    private float timerTime;


    // Update is called once per frame
    void LateUpdate()
    {
        //Adds the time elapst since the last frame to our courrent timer representation
        timerTime += Time.time;
        //Updating the test on the textmeshpro
        timerText.UpdateText(FormatTime());
    }

    //Funktion called at the end of the game used to end the timer
    public string endTimer()
    {
        gameObject.SetActive(false); //Setting the timer as inaktive deaktivates the script
        return FormatTime(); //returns the endTime
    }

    //funktion used to formate the time got from Time.time into a nice representation
    private string FormatTime()
    {
        //we store the time in millisekunds when deviding with 1000 we get secunds
        float seconds = timerTime / 1000;


        int hours = (int)(seconds / 3600); //3600 secinds represent one hour
        int minutes = (int)((seconds % 3600) / 60); //with the modulu operatore we get the the time which can not be represented by a full hour
        int secondsInt = (int)(seconds % 60);//Here we get the seconds which are to few to fill a minute
        int milliseconds = (int)((seconds - (int)seconds) * 1000); //Here we calculate the milisecunds
        int roundedMilliseconds = milliseconds / 10; //rounding the milisecunds to one digit

        return $"{hours:D2}:{minutes:D2}:{secondsInt:D2}.{roundedMilliseconds:D1}";//returning the formated string
    }
}
