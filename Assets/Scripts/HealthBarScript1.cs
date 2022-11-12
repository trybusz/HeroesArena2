using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript1 : MonoBehaviour
{
    private float primaryValue;
    private float specialValue;
    private float primaryValue2;
    private float specialValue2;
    public Slider healthSlider;
    public Slider primarySlider;
    public Slider specialSlider;
    // Start is called before the first frame update
    public void SetHealth(float healthRatio)
    {
        healthSlider.value = healthRatio;
    }
    public void SetPrimary(float primaryRatio)
    {
        primarySlider.value = primaryRatio;
    }
    public void SetSpecial(float specialRatio)
    {
        specialSlider.value = specialRatio;
    }
    /*
    public void SetAbilities(float primaryAttackPauseTime, float primaryAttackPause, float specialAttackPauseTime, float specialAttackPause)
    {
        primaryValue = primaryAttackPauseTime;
        specialValue = specialAttackPauseTime;
        primaryValue2 = primaryAttackPause;
        specialValue2 = specialAttackPause;
        
    }
    */
    private void Update()
    {
        /*
        if (primaryValue - Time.timeSinceLevelLoad > 0)
        {
            primarySlider.value = (primaryValue - Time.timeSinceLevelLoad) / primaryValue2;
        }
        if (specialValue - Time.timeSinceLevelLoad > 0)
        {
            specialSlider.value = (specialValue - Time.timeSinceLevelLoad) / specialValue2;
        }
        */
    }
}
