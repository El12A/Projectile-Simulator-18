using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class VariableControllerInput : MonoBehaviour
{
    [SerializeField] private GameObject singleInputPrefab;
    [SerializeField] private GameObject vector3InputPrefab;
    [SerializeField] private Transform parentTransform;
    private string allowedCharacters = "0123456789.-";
    private TMP_InputField[] inputFields;
    private Vector3 spawnPosition;
    [SerializeField] protected List<GameObject> spawnList;

    // Start is called before the first frame update
    protected  void Start()
    {
       foreach (GameObject inputprefab in spawnList)
       {
            inputFields = inputprefab.GetComponentsInChildren<TMP_InputField>();
            foreach (TMP_InputField input in inputFields)
            {
                input.text = "0";
                input.onValueChanged.AddListener(delegate { OnValueChanged(input); });
                input.onEndEdit.AddListener(delegate { OnEndEdit(input); });
            }
       }
       GameControl.control.spawnInputsPrefabList = spawnList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SetInputActive(int dropdownNum, string variable)
    {

        // if dropdown being changed is the first one make the spawnposition for the input field be as described
        // spawn position differs based on which dropdown it is
        if (dropdownNum == 0)
        {
            spawnPosition = new Vector3(800, 300, 0);
        }
        else if (dropdownNum == 1)
        {
            spawnPosition = new Vector3(800, 200, 0);
        }
        else if (dropdownNum == 2)
        {
            spawnPosition = new Vector3(800, 100, 0);
        }

        // if time is the variable that is being chosen in the dropdown then spawn a gameobject with only one input field and make it spawn slightly more to the right so it is central
        if (variable == "Time" || variable == "Acceleration")
        {
            spawnPosition.x = spawnPosition.x + 80;
            spawnNewInputField(singleInputPrefab, spawnPosition, dropdownNum);
        }
        // otherwise we are spawning a vector in which case spawn a gameobject containing three input fields
        else
        {

            spawnNewInputField(vector3InputPrefab, spawnPosition, dropdownNum);
        }


    }

    protected void spawnNewInputField(GameObject inputPrefab, Vector3 spawnPos, int index)
    {
        // if the number of spawned gameobjects which are stored in spawnlist are 3 or more first delete the one which is to be replaced.
        if (spawnList.Count > 2)
        {
            Destroy(spawnList[index]);
            spawnList.RemoveAt(index);
        }
        // spawn the specified gameobject at set location with no rotation and set the object to have the varaiableControllerInput as its parent and translate it to the right position specified by spawnposition 
        GameObject spawnedInputField = Instantiate(inputPrefab, transform.position, Quaternion.identity);
        spawnedInputField.transform.SetParent(parentTransform);
        spawnedInputField.transform.Translate(spawnPos);

        inputFields = spawnedInputField.GetComponentsInChildren<TMP_InputField>();

        foreach (TMP_InputField input in inputFields)
        {
            input.text = "0";
            input.onValueChanged.AddListener(delegate { OnValueChanged(input); });
            input.onEndEdit.AddListener(delegate { OnEndEdit(input); });
        }

        // add the gameobject to the list at the specified index.
        spawnList.Insert(index, spawnedInputField);
        GameControl.control.spawnInputsPrefabList = spawnList;
    }

    void OnValueChanged(TMP_InputField inputField)
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
                if (numberCharacterCount == 3)
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
                else if (c != '.')
                {
                    validatedValue += c;
                    numberCharacterCount++;
                }
            }
        }
        // Update the input field text with only the allowed characters
        inputField.text = validatedValue;
    }
    void OnEndEdit(TMP_InputField inputField)
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
            inputField.text = "0";
        }
    }
}

