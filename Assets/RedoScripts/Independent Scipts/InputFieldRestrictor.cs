using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class InputFieldRestrictor : MonoBehaviour
    {
        public string allowedCharacters;
        public int maxCharacters;
        public string defaultString;
        public TMP_InputField inputField;
        public void UpdateInputField(TMP_InputField input)
        {
            inputField = input;
        }
        public void OnValueChanged()
        {
            // Validate the input value for the specific input field
            int numberCharacterCount = 0;
            string newValue = inputField.text;
            string validatedValue = "";

            bool decimalPointAdded = false;

            foreach (char c in newValue)
            {
                // Check if the character is in the allowedCharacters string
                if (allowedCharacters.Contains(c.ToString()))
                {
                    // makes sure that a max of 3sf for the input
                    if (numberCharacterCount == maxCharacters)
                        continue;
                    // makes sure that if the first character is a decimal point to ignore it
                    if (c == '.' && validatedValue.Length == 0)
                        continue;
                    if (c == '-' && validatedValue.Length != 0)
                        continue;


                    // Ensure only one decimal point is added
                    if (c == '.' && !decimalPointAdded)
                    {
                        validatedValue += c;
                        decimalPointAdded = true;
                    }
                    else if (c == '-')
                    {
                        validatedValue += c;
                    }
                    // if its not a  decimal point or dash (has to be a number) then add 1 to numberCharacterCount
                    else if (c != '.')
                    {
                        validatedValue += c;
                        numberCharacterCount++;
                    }

                }
            }
            // Updates the input field text so that it only contains the allowed characters
            inputField.text = validatedValue;
        }
        public void OnEndEdit()
        {
            // If the last character is a decimal point, add a zero
            string text = inputField.text;
            if (text.EndsWith(".") || text.EndsWith("-"))
            {
                inputField.text += "0";
            }

            // If left empty, set text to "0"
            if (string.IsNullOrEmpty(text))
            {
                inputField.text = defaultString;
            }
        }
    }
}

