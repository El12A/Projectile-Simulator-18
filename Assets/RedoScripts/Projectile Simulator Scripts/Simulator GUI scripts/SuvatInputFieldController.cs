using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace PhysicsProjectileSimulator
{
    public class SuvatInputFieldController : VariableController
    {
        [SerializeField] private GameObject singleInputPrefab;
        [SerializeField] private GameObject vector3InputPrefab;
        [SerializeField] private Transform parentTransform;
        public InputFieldRestrictor inputFieldRestrictor;
        private TMP_InputField[] inputFields;
        private Vector3 spawnPosition;

        

        // Start is called before the first frame update
        protected void Start()
        {
            foreach (GameObject inputprefab in variableController.spawnInputsPrefabList)
            {
                inputFields = inputprefab.GetComponentsInChildren<TMP_InputField>();
                foreach (TMP_InputField input in inputFields)
                {
                    input.text = "0";
                    input.onValueChanged.AddListener(delegate { inputFieldRestrictor.OnValueChanged(); });
                    input.onEndEdit.AddListener(delegate { inputFieldRestrictor.OnEndEdit(); });
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetInputActive(int dropdownNum, string variable)
        {
            Debug.Log(variable);
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

        private void spawnNewInputField(GameObject inputPrefab, Vector3 spawnPos, int index)
        {
            // if the number of spawned gameobjects which are stored in spawnInputsPrefabList are 3 or more first delete the one which is to be replaced.
            if (variableController.spawnInputsPrefabList.Count > 2)
            {
                Destroy(variableController.spawnInputsPrefabList[index]);
                variableController.spawnInputsPrefabList.RemoveAt(index);
            }
            // spawn the specified gameobject at set location with no rotation and set the object to have the varaiableControllerInput as its parent and translate it to the right position specified by spawnposition 
            GameObject spawnedInputField = Instantiate(inputPrefab, transform.position, Quaternion.identity);
            spawnedInputField.transform.SetParent(parentTransform);
            spawnedInputField.transform.Translate(spawnPos);

            inputFields = spawnedInputField.GetComponentsInChildren<TMP_InputField>();

            foreach (TMP_InputField input in inputFields)
            {
                input.text = "0";
                input.onValueChanged.AddListener(delegate { OnAnyInputFieldValueChanged(input); });
                input.onEndEdit.AddListener(delegate { OnAnyInputFieldEndEdit(input); });
            }

            // add the gameobject to the list at the specified index.
            variableController.spawnInputsPrefabList.Insert(index, spawnedInputField);
        }
        
        public void OnAnyInputFieldValueChanged(TMP_InputField input)
        {
            inputFieldRestrictor.UpdateInputField(input);
            inputFieldRestrictor.OnValueChanged();
        }
        public void OnAnyInputFieldEndEdit(TMP_InputField input)
        {
            inputFieldRestrictor.UpdateInputField(input);
            inputFieldRestrictor.OnEndEdit();
        }
    }
}

