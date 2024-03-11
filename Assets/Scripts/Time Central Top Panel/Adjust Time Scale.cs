using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// https://discussions.unity.com/t/how-to-change-the-speed-of-play-mode/45470
// https://docs.unity3d.com/ScriptReference/Time-timeScale.html
public class AdjustTimeScale : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text text;
    public void OnSliderValueChanged()
    {
        float roundedTimeScale = timeSlider.value;
        string roundedTimeScaleString = roundedTimeScale.ToString() + "  ";
        roundedTimeScaleString = roundedTimeScaleString.Substring(0, 3);
        roundedTimeScale = float.Parse(roundedTimeScaleString);
        Time.timeScale = roundedTimeScale;
        text.text = "Play Speed: " + roundedTimeScale + "x";
    }

}
