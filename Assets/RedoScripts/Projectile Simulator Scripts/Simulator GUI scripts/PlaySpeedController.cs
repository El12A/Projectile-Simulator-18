using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhysicsProjectileSimulator
{
    // this clas handles the timescale of the gameplay based on what value the user chooses using the play speed slider
    public class PlaySpeedController : PhysicsSimulator
    {
        // reference to the play speed slider and text components
        [SerializeField] private Slider playSpeedSlider;
        [SerializeField] private TMP_Text playSpeedText;

        // This function is called when the slider value changes and it will convert the value to a string, round it and show it to the user;
        // The rounded playspeed is applied to the games internal clock (Time.timeScale) to slow everything down.
        public void OnPlaySpeedSliderValueChanged()
        {
            float roundedTimeScale = playSpeedSlider.value;
            string roundedTimeScaleString = roundedTimeScale.ToString() + "  ";
            roundedTimeScaleString = roundedTimeScaleString.Substring(0, 3);
            roundedTimeScale = float.Parse(roundedTimeScaleString);
            Time.timeScale = roundedTimeScale;
            playSpeedText.text = "Play Speed: " + roundedTimeScale + "x";
        }
    }
}
