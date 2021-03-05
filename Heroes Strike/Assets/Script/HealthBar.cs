using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealth(int health)
    {
        slider.value = health;
        if(gradient != null)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        if(slider.value == slider.minValue)
        {
            gameObject.SetActive(false);
        }
    }
}
