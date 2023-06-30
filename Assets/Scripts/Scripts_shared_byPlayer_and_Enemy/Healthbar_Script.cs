using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Script : MonoBehaviour
{
    public Slider slider;  // Reference to the Slider component for the health bar
    public Gradient gradient;  // Gradient used to determine the color of the health bar
    public Image fill;  // Image component responsible for filling the health bar

    [SerializeField] float maxHealth = 0;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        slider.maxValue = maxHealth;  // Set the maximum value of the health bar to the provided maxHealth
        slider.value = maxHealth;  // Set the current value of the health bar to the provided maxHealth (full health)
        fill.color = gradient.Evaluate(1f);  // Set the color of the health bar to the color at the end of the gradient
    }

    public void SetHealth(float health)
    {
        slider.value = health;  // Set the current value of the health bar to the provided health value

        // Evaluate the color at the normalized value of the health bar slider within the gradient
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
