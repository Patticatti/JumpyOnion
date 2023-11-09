using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetTotalHeight(float height)
    {
        slider.maxValue = height;
    }

    public void UpdateProgress(float height)
    {
        slider.value = height;
    }
}
