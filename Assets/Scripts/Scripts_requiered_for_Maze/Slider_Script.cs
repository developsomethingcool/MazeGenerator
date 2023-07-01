using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Script : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] bool diff;

    private Data_Percistence dp;

    private float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        dp = new Data_Percistence();

        slider.onValueChanged.AddListener((v) =>
        {
            if (diff)
            {
                dp.SetDifficulty(v);
            }
            else
            {
                dp.SetMazeSize(v);
            }
            sliderValue = v;
        });
    }

    public float getSliderValue()
    {
        return sliderValue;
    }
}
