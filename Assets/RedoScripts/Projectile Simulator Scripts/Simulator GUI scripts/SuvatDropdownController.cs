using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class SuvatDropdownController : VariableController
    {
        [SerializeField] private TMP_Dropdown dropdown1;
        [SerializeField] private TMP_Dropdown dropdown2;
        [SerializeField] private TMP_Dropdown dropdown3;

        private List<string> availableOptions1;
        private List<string> availableOptions2;
        private List<string> availableOptions3;

        private int index;

        protected void Start()
        {
            // Initialize available options
            availableOptions1 = new List<string> { "Initial Velocity", "Acceleration", "Time" };
            availableOptions2 = new List<string> { "Final Velocity", "Acceleration", "Time" };
            availableOptions3 = new List<string> { "Displacement", "Acceleration", "Time" };
            // Update dropdown options
            UpdateDropdownOptions();
        }

        // Update options for all dropdowns
        private void UpdateDropdownOptions()
        {
            dropdown1.ClearOptions();
            dropdown1.AddOptions(new List<string>(availableOptions1));

            dropdown2.ClearOptions();
            dropdown2.AddOptions(new List<string>(availableOptions2));

            dropdown3.ClearOptions();
            dropdown3.AddOptions(new List<string>(availableOptions3));
        }

        // Update available options based on selection in any dropdown
        public void OnAnyDropdownValue1Changed()
        {
            AnyDropdownChange(dropdown1, availableOptions1, 0);
        }
        public void OnAnyDropdownValue2Changed()
        {
            AnyDropdownChange(dropdown2, availableOptions2, 1);
        }
        public void OnAnyDropdownValue3Changed()
        {
            AnyDropdownChange(dropdown3, availableOptions3, 2);
        }

        private void AnyDropdownChange(TMP_Dropdown dropdown, List<string> availableOptions, int i)
        {
            index = dropdown.value;
            Swap(availableOptions);
            AddAndRemoveFromList(availableOptions);
            UpdateDropdownOptions();

            variableController.selectedVariables[i] = dropdown.options[dropdown.value].text;
            variableController.unselectedVariables[0] = availableOptions[1];
            variableController.unselectedVariables[1] = availableOptions[2];

            missingSuvatTextController.UpdateVariableText();
            // call function to activate the inputfield for the variable selected
            SuvatInputFieldController.SetInputActive(i, dropdown.options[dropdown.value].text);
        }


        // Remove selected options from other dropdowns
        private void Swap(List<string> options)
        {
            //makes the current chosen element now be at the front of the list of available options
            string temp = options[0];
            options[0] = options[index];
            options[index] = temp;
        }

        private void AddAndRemoveFromList(List<string> KeyList)
        {
            List<List<string>> optionsList = new List<List<string>> { availableOptions1, availableOptions2, availableOptions3 };
            foreach (List<string> list in optionsList)
            {
                if (list != KeyList)
                {
                    list.Add(KeyList[index]);
                    list.Remove(KeyList[0]);
                }
            }
        }

    }
}

