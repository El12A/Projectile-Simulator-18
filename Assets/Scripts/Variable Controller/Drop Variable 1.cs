using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownVariable1 : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private List<TMP_Dropdown.OptionData> variableOptions = new List<TMP_Dropdown.OptionData>();
    
    public void GetDropdownValue()
    {
        int pickedEntryIndex = dropdown.value;
        string selectedOption = dropdown.options[pickedEntryIndex].text;
        foreach (var option in variableOptions)
        {
            Debug.Log(option);
        }
        
    }

    public void AddNewOption()
    {
        dropdown.options.Add(new TMP_Dropdown.OptionData(text: "", image: null));
        dropdown.RefreshShownValue();
    }
}
