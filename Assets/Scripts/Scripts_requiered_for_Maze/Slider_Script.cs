using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Script : MonoBehaviour
{
    //slider pbject
    [SerializeField] private Slider slider;

    //ther are two sliders in to project so one can distinguis between them base on this bool
    [SerializeField] bool diff;

    //data_percistence to store the data
    private Data_Percistence dp;

    //slider value
    private float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        //getting the data:percistance instance
        dp = new Data_Percistence();

        //adding a Listener to the slider
        slider.onValueChanged.AddListener((v) =>
        {
            //iff the slider belongs to difficulty we change difficulty
            if (diff)
            {
                dp.SetDifficulty(v);
            }
            else//if the slider dosn't belong to difficulty it belongs(for this project) to maze size
            {
                dp.SetMazeSize(v);
            }
            sliderValue = v; //we save the slider value
        });
    }

    //getter for the slider method
    public float getSliderValue()
    {
        return sliderValue;
    }
}
