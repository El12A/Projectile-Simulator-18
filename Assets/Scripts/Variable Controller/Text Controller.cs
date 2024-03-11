using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class TextController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private FindMissingVariables findMissingVariables;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateVariableText()
    {
        text1.text = GameControl.control.unselectedVariables[0];
        text2.text = GameControl.control.unselectedVariables[1];
    }

    public void UpdateAllText()
    {
        string variable1 = GameControl.control.unselectedVariables[0];
        string variable2 = GameControl.control.unselectedVariables[1];
        findMissingVariables.CalculateMissingVariables();
        text1.text = variable1 + "<br>" + GetVarValue(variable1);
        text2.text = variable2 + "<br>" + GetVarValue(variable2);
    }

    private string GetVarValue(string var)
    {
        if (var == "Initial Velocity")
        {
            string Value = "X:" + RoundToSF(GameControl.control.initialVelocity.x, 3) + " Y:" + RoundToSF(GameControl.control.initialVelocity.y, 3) + " Z:" + RoundToSF(GameControl.control.initialVelocity.z, 3);
            return Value;
        }
        else if (var == "Final Velocity")
        {
            string Value = "X:" + RoundToSF(GameControl.control.finalVelocity.x, 3) + " Y:" + RoundToSF(GameControl.control.finalVelocity.y, 3) + " Z:" + RoundToSF(GameControl.control.finalVelocity.z, 3);
            return Value;
        }
        else if (var == "Displacement")
        {
            string Value = "X:" + RoundToSF(GameControl.control.displacement.x, 3) + " Y:" + RoundToSF(GameControl.control.displacement.y, 3) + " Z:" + RoundToSF(GameControl.control.displacement.z, 3);
            return Value;
        }
        else if (var == "Acceleration")
        {
            string Value = "X:" + RoundToSF(GameControl.control.acceleration.x, 3) + " Y:" + RoundToSF(GameControl.control.acceleration.y, 3) + " Z:" + RoundToSF(GameControl.control.acceleration.z, 3);
            return Value;
        }
        else if (var == "Time")
        {
            string Value = "X:" + RoundToSF(GameControl.control.time.x, 3) + " Y:" + RoundToSF(GameControl.control.time.y, 3) + " Z:" + RoundToSF(GameControl.control.time.z, 3);
            return Value;
        }
        return "";
    }

    public string RoundToSF(float number, int sigFigs)
    {
        string numberString = number.ToString();
        if (number > float.NegativeInfinity && number < float.PositiveInfinity)
        {
            // Check if the number is negative and contains a decimal point
            if (number < 0 && numberString.Contains('.'))
            {
                // Return the first five characters including the minus sign
                return numberString.Substring(0, Mathf.Min(numberString.Length, (sigFigs+2) ));
            }
            // Check if the number contains a decimal point or a negative sign
            else if (numberString.Contains('.') || number < 0)
            {
                // Return the first four characters
                return numberString.Substring(0, Mathf.Min(numberString.Length, (sigFigs+1) ));
            }
            else
            {
                // Return the first three characters
                return numberString.Substring(0, Mathf.Min(numberString.Length, sigFigs ));
            }

        }
        return numberString;
    }
}
