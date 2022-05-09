using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreaterHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void SetMaxValue(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void SetValue(float health)
    {
        slider.value = health;
    }
}
