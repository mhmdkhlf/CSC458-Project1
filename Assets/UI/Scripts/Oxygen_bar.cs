using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen_bar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image fill;
    [SerializeField] Gradient gradient;
    public void SetMaxHealth(int oxygen)
    {
        slider.maxValue = oxygen;
        slider.value = oxygen;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float oxygen)
    {
        slider.value = oxygen;
        fill.color = gradient.Evaluate(slider.normalizedValue); //same as value but gives us from 0 to 1 and we want that for the evaluate
    }
}
