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

        // reference to error message text, trajectory and its line renderer
        public TMP_Text ErrorMessageText;
        [SerializeField] private GameObject trajectoryObject;
        private LineRenderer trajectoryLineRenderer;
        // variables for the draw trajectory recursive function
        private int maxIterations = 1000;
        private float maxTime = 100f;
        private float timeStep = 0.1f;
        // boolean for the trajectory whether it should be active or not active
        private bool trajectory0n = false;
        // references to the reveal variable buttons 
        [SerializeField] private GameObject HideVariableButton1;
        [SerializeField] private GameObject HideVariableButton2;
        [SerializeField] private Toggle airResistanceToggle;

        // references to the different children scripts that need to be refernced in different children scripts hence the reason there is a reference here in the parent class
        public SuvatDropdownController SuvatDropdownController;
        public SuvatInputFieldController SuvatInputFieldController;
        public MissingSuvatTextController missingSuvatTextController;
        public FindMissingSuvat findMissingSuvat;

        [SerializeField] public List<GameObject> spawnInputsPrefabList;
        // conatins at start: Initial velocity, Final velocity, Displacement
        public List<string> selectedVariables;
        // conatins at start: Accleration, Time
        public List<string> unselectedVariables;

        // the important 5 suvat variables 3 of which are entered by the user and 2 of which are then calculated based off of the users inputs
        public Vector3 displacement;
        public Vector3 initialVelocity;
        public Vector3 finalVelocity;
        public Vector3 acceleration;
        public Vector3 time;

        [SerializeField] private GameObject projectileShooter;

        private bool acceptInputs = true;
        private void Start()
        {
            // get reference to the projectile script from the projectile gameobject
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
        }

        // based on which variables have been selected set the value of the suvat variable to that inputted the user
        // this function is called when calculate Button is clicked
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

            // then we check if the values given are actuall in the allowed range
            // if not an error message will be shown and variables wont be accepted as suvat inputs
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
            
            // if inputted variables have passed the test then say succesful input to the user and continue by calling update all text function of child class, hiding the text of the new calculated variables (calculated in child class), updating projectile shooter angle, and drawing the trajectory if trajectory toggle is set to true
            if (acceptInputs == true)
            {
                ErrorMessageText.text = "Successfull Input";
                missingSuvatTextController.UpdateAllText();
                ResetHideButtons();
                AdjustProjectileShooterAngle();
                DrawTrajectory(projectile.projectileObject.transform.position, initialVelocity, 0);
            }
            // else reset accepet inputs to true ready for another check
            else
            {
                acceptInputs = true;
            }



        }

        // this method will get the correct input prefab and get a vector 3 value from what is found in the input field
        // this is called when trying to get what is in a vector 3 input field prefab eg what is entered for initial velocity, final velocity or displacement 
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

        // this method returns a vector 3 with all the components equal to the float entered by the user in the single inputField
        // used for time and acceleration, for acceleration however after being calle the x and z components are set to 0 as acceleration input is only for Y component
        private Vector3 SetVariableSingle(int index)
        {
            GameObject parentObject = spawnInputsPrefabList[index];

            TMP_InputField[] inputFields = parentObject.GetComponentsInChildren<TMP_InputField>();
            float inputText = float.Parse(inputFields[0].text);
            return new Vector3(inputText, inputText, inputText);
        }

        // this method will make sure that the variable is in the correct desired range return a bool for yes or no - true or false
        private bool CheckVariableInRange(float varComponent, float lowRange, float highRange)
        {
            if (varComponent >= lowRange && varComponent <= highRange)
            {
                return true;
            }
            return false;
        }

        // resets the Reveal variable buttons called when valid input is given 
        private void ResetHideButtons()
        {
            HideVariableButton1.SetActive(true);
            HideVariableButton2.SetActive(true);
        }

        // activates or deactivates the trajectory
        public void ActivateTrajectory()
        {
            // if the trajectory is not yet on then activate it
            if (trajectory0n == false)
            {
                trajectory0n = true;
                trajectoryObject.SetActive(true);
                trajectoryLineRenderer = trajectoryObject.GetComponent<LineRenderer>();
                DrawTrajectory(projectile.initialPosition, initialVelocity, 0);
            }
            // else deactivate the trajectory
            else
            {
                trajectory0n = false;
                trajectoryObject.SetActive(false);
            }

        }
        //recursive algorithm that calculates and draws the trajectory path
        private void DrawTrajectory(Vector3 position, Vector3 velocity, int iteration)
        {
            // if trajectory toggle is set to true then draw trajectory else do not
            if (trajectory0n == true)
            {
                // stops the trajectory from having too many points so that the trajectory line renderer is never too performance heavy from rendering to many points
                if (iteration >= maxIterations)
                {
                    Debug.LogWarning("Max iterations reached. Abort trajectory calculation");
                    return;
                }
                // This will stop the recursive algorithm when projectile trajectory reaches the ground as then it will no longer be visible so no point drawing any more points
                if (position.y < 0)
                {
                    return; 
                }
                // if points are being draw for a long time stop the trajetory calculation as becoming too performance heavy
                if (iteration >= maxTime / timeStep)
                {
                    Debug.LogWarning("Max time reached. Aborting trajectory calculation.");
                    return;
                }
                // add one to the position cound and set new position for the line renderer
                trajectoryLineRenderer.positionCount = iteration + 1;
                trajectoryLineRenderer.SetPosition(iteration, position);
                // calculate nex position and velocity to apply for the next iteration of the recursive trajectory drawing function
                Vector3 nextPosition = position + velocity * timeStep;
                Vector3 nextVelocity = velocity + acceleration * timeStep;

                // call the function again with the new values for position and velocity
                DrawTrajectory(nextPosition, nextVelocity, iteration + 1);
            }
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

        // adjusts the angle rotation of the projectile shooter to that of the projectile at initial velocity
        public void AdjustProjectileShooterAngle()
        {
            projectileShooter.transform.rotation = Vector3.Angle(initialVelocity);
        }
    }
}
