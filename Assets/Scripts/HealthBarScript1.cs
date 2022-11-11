using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript1 : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void SetHealth(int maxHealth, int currentHealth)
    {
        slider.value = (float)currentHealth / (float)maxHealth;
    }
}
