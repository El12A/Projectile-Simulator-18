using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PhysicsProjectileSimulator
{
    public class VariableController : PhysicsSimulator
    {
        public TMP_Text ErrorMessageText;
        [SerializeField] private GameObject trajectoryObject;
        private LineRenderer trajectoryLineRenderer;
        private int maxIterations = 1000;
        private float maxTime = 100f;
        private float timeStep = 0.1f;
        private bool trajectory0n = false;
        [SerializeField] private GameObject HideVariableButton1;
        [SerializeField] private GameObject HideVariableButton2;
        [SerializeField] private Toggle airResistanceToggle;

        public SuvatDropdownController SuvatDropdownController;
        public SuvatInputFieldController SuvatInputFieldController;
        public MissingSuvatTextController missingSuvatTextController;
        public FindMissingSuvat findMissingSuvat;

        [SerializeField] public List<GameObject> spawnInputsPrefabList;
        // conatins at start: Initial velocity, Final velocity, Displacement
        public List<string> selectedVariables;
        // conatins at start: Accleration, Time
        public List<string> unselectedVariables;

        public Vector3 displacement;
        public Vector3 initialVelocity;
        public Vector3 finalVelocity;
        public Vector3 acceleration;
        public Vector3 time;

        private bool acceptInputs = true;
        private void Start()
        {
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateInputtedVariables()
        {
            foreach (string var in selectedVariables)
            {
                int index = selectedVariables.IndexOf(var);
                if (var == "Displacement")
                {
                    displacement = SetVariableVector3(index);
                }
                else if (var == "Initial Velocity")
                {
                    initialVelocity = SetVariableVector3(index);
                }
                else if (var == "Final Velocity")
                {
                    finalVelocity = SetVariableVector3(index);
                }
                else if (var == "Acceleration")
                {
                    acceleration = SetVariableSingle(index);

                }
                else if (var == "Time")
                {
                    time = SetVariableSingle(index);
                }

            }

            foreach (string var in selectedVariables)
            {
                if (var == "Displacement")
                {
                    if (CheckVariableInRange(displacement.x, 3, 50) == false || CheckVariableInRange(displacement.y, 1, 50) == false || CheckVariableInRange(displacement.z, -50, 50) == false)
                    {
                        // if one of the displacement components are not it in specified variables don't accept input
                        acceptInputs = false;
                        ErrorMessageText.text = "Displacement needs to be in the range of:                       X:(3 - 50) Y:(1-50) Z:(-50 - 50)";
                    }
                }
                else if (var == "Initial Velocity")
                {
                    if (CheckVariableInRange(initialVelocity.x, 1, 50) == false || CheckVariableInRange(initialVelocity.y, 0, 50) == false || CheckVariableInRange(initialVelocity.z, -50, 50) == false)
                    {
                        // if one of the initialVelocity components are not it in specified variables don't accept input
                        acceptInputs = false;
                        ErrorMessageText.text = "Initial Velocity needs to be in the range of:                   X:(3 - 50) Y:(1-50) Z:(-50 - 50)";
                    }
                }
                else if (var == "Final Velocity")
                {
                    if (CheckVariableInRange(finalVelocity.x, -50, 50) == false || CheckVariableInRange(finalVelocity.y, -50, 50) == false || CheckVariableInRange(finalVelocity.z, -50, 50) == false)
                    {
                        // if one of the finalVelocity components are not it in specified variables don't accept input
                        acceptInputs = false;
                        ErrorMessageText.text = "Final Velocity needs to be in the range of:                     X:(3 - 50) Y:(1-50) Z:(-50 - 50)";
                    }
                }
                else if (var == "Acceleration")
                {
                    if (CheckVariableInRange(acceleration.x, -20, 20) == false)
                    {
                        // if one of the acceleration components are not it in specified variables don't accept input
                        acceptInputs = false;
                        ErrorMessageText.text = "Acceleration needs to be in the range of:                       Y:(1-50)";
                    }
                }
                else if (var == "Time")
                {
                    if (CheckVariableInRange(time.x, 0, 50) == false)
                    {
                        // if one of the time components are not it in specified variables don't accept input
                        acceptInputs = false;
                        ErrorMessageText.text = "Time needs to be in the range of:                               Y:(1-50)";
                    }
                }

            }

            if (acceptInputs == true)
            {
                foreach (string var in selectedVariables)
                {
                    int index = selectedVariables.IndexOf(var);
                    if (var == "Displacement")
                    {
                        displacement = SetVariableVector3(index);
                        Debug.Log("s");
                    }

                    if (var == "Initial Velocity")
                    {
                        initialVelocity = SetVariableVector3(index);
                        Debug.Log("u");
                    }
                    if (var == "Final Velocity")
                    {
                        finalVelocity = SetVariableVector3(index);
                        Debug.Log("v");
                    }
                    if (var == "Acceleration")
                    {
                        acceleration = SetVariableSingle(index);
                        acceleration.x = 0.0f;
                        acceleration.z = 0.0f;
                        Debug.Log("a");

                    }
                    if (var == "Time")
                    {
                        time = SetVariableSingle(index);
                        Debug.Log("t");
                    }
                }
                ErrorMessageText.text = "Successfull Input";
                missingSuvatTextController.UpdateAllText();
                ResetHideButtons();
                DrawTrajectory(projectile.projectileObject.transform.position, initialVelocity, 0);
            }
            else
            {
                acceptInputs = true;
            }



        }

        private Vector3 SetVariableVector3(int index)
        {
            GameObject parentObject = spawnInputsPrefabList[index];

            TMP_InputField[] inputFields = parentObject.GetComponentsInChildren<TMP_InputField>();

            // Access the text of each InputField
            float inputText1 = float.Parse(inputFields[0].text);
            float inputText2 = float.Parse(inputFields[1].text);
            float inputText3 = float.Parse(inputFields[2].text);



            return new Vector3(inputText1, inputText2, inputText3);
        }

        private Vector3 SetVariableSingle(int index)
        {
            GameObject parentObject = spawnInputsPrefabList[index];

            TMP_InputField[] inputFields = parentObject.GetComponentsInChildren<TMP_InputField>();
            float inputText = float.Parse(inputFields[0].text);
            return new Vector3(inputText, inputText, inputText);

        }

        private bool CheckVariableInRange(float varComponent, float lowRange, float highRange)
        {
            if (varComponent >= lowRange && varComponent <= highRange)
            {
                return true;
            }
            return false;
        }

        private void ResetHideButtons()
        {
            HideVariableButton1.SetActive(true);
            HideVariableButton2.SetActive(true);
        }

        public void ActivateTrajectory()
        {
            if (trajectory0n == false)
            {
                trajectory0n = true;
                trajectoryObject.SetActive(true);
                trajectoryLineRenderer = trajectoryObject.GetComponent<LineRenderer>();
                DrawTrajectory(projectile.initialPosition, initialVelocity, 0);
            }
            else
            {
                trajectory0n = false;
                trajectoryObject.SetActive(false);
            }
            
        }
        //recursive algorithm to calculate trajectory path
        private void DrawTrajectory(Vector3 position, Vector3 velocity, int iteration)
        {
            if (trajectory0n == true)
            {

                if (iteration >= maxIterations)
                {
                    // stops the trajectory from having too many points so that the trajectory line renderer is never too performance heavy from rendering to many points
                    Debug.LogWarning("Max iterations reached. Abort trajectory calculation");
                    return;
                }
                if (position.y < 0)
                {
                    return; // This will stop the recursive algorithm when projectile trajectory reaches the ground
                }
                if (iteration >= maxTime / timeStep)
                {
                    Debug.LogWarning("Max time reached. Aborting trajectory calculation.");
                    return;
                }
                trajectoryLineRenderer.positionCount = iteration + 1;
                trajectoryLineRenderer.SetPosition(iteration, position);

                Vector3 nextPosition = position + velocity * timeStep;
                Vector3 nextVelocity = velocity + acceleration * timeStep;

                DrawTrajectory(nextPosition, nextVelocity, iteration + 1);
            }
        }

        private Vector3 CalculatePositionAtTime(float time)
        {
            // Calculates the position of the projectile at a specific moment in time
            return projectile.initialPosition + initialVelocity * time + 0.5f * acceleration * time * time;
        }

        // if toggle is on make the projectile drag equal whatever specified by projectile's drag variable else make it zero so no air resistance is experienced by the projectile
        public void ToggleAirResistance()
        {
            int dragmultiplier = 0;
            if (airResistanceToggle.isOn == true)
            {
                dragmultiplier = 1;
            }
            projectile.projectileRb.drag = dragmultiplier * projectile.drag;
        }
    }

}
