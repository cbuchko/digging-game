using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setMaxFuel(int fuelAmount){
        slider.maxValue = fuelAmount;
        slider.value = fuelAmount;

        fill.color = gradient.Evaluate(1f);
    }
    
    public void setFuel(int fuelAmount){
        slider.value = fuelAmount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
