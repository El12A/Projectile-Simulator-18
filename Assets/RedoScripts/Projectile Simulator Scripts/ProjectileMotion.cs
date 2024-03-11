using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{

    public class ProjectileMotion : PhysicsSimulator
    {
        public bool reset;
        public bool inMotion;
        public bool isPaused;

        private Vector3 accelerationForce;
        public GameObject targetObject;

        private Rigidbody projectileRb;
        private Vector3 velocityToReapply;
        private Vector3 locationToReapply;
        private float timeToReapply;

        private float deltaTime;
        [SerializeField] private GameObject forward1sTimeStampButton;
        [SerializeField] private GameObject back1sTimeStampButton;
        [SerializeField] private DisplayCurrentVariables displayCurrentVariables;

        // Start is called before the first frame update
        void Start()
        {
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
            projectileRb = projectile.projectileRb;
            projectileRb.isKinematic = true;
            inMotion = false;
            reset = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && reset == true)
            {
                // when space bar is pressed by user begin projectile motion
                // can only be fired once until reset with keyboard button press R
                inMotion = true;
                reset = false;
                projectile.projectileRb.isKinematic = false;
                accelerationForce = variableController.acceleration;
                projectile.projectileRb.velocity = variableController.initialVelocity;
            }

            // when R is pressed the projectile is reset.
            if (Input.GetKeyDown(KeyCode.R) && reset == false)
            {
                // if trying to reset while paused then unpause before resetting projectile
                if (isPaused == true)
                {
                    OnPausePlayButtonClick();
                }
                ResetProjectile();
            }

            if (inMotion == true)
            {
                //To apply the right force need to use F = ma as in order to have the same acceleration on any object we need to apply a force proportional to the mass
                // Calculate the time elapsed since the last frame
                deltaTime = Time.deltaTime;

                // Calculate the force to be applied based on acceleration, mass, and time elapsed
                Vector3 force = accelerationForce * projectileRb.mass * deltaTime;

                // Apply the force to the projectile
                projectileRb.AddForce(force, ForceMode.Impulse);


            }
        }

        // reseting the velocity and angular velocity to zero, making the projectile kinematic so it no longer moves and also reseting its position to its initial start position (inside the projectile shooter cannon object as you can see in the scene).
        private void ResetProjectile()
        {
            projectileRb.velocity = Vector3.zero;
            projectileRb.angularVelocity = Vector3.zero;
            projectileRb.isKinematic = true;
            projectileRb.transform.position = projectile.initialPosition;
            reset = true;
            inMotion = false;
        }

        // when the pause/play button is clicked then depending on the state it will either pause the projectile motion or allow it to contiue moving
        public void OnPausePlayButtonClick()
        {
            // if projectile is not reset meaning it also has been launched then allow user to play or pause (for error prevention)
            if (reset == false)
            {
                // if the simulation is not paused save the current velocity for the unpause and then set the rigidbogy to kinematic so it cannot move
                if (isPaused == false)
                {
                    timeToReapply = displayCurrentVariables.time;
                    velocityToReapply = projectileRb.GetPointVelocity(projectileRb.position);
                    // save location incase location changes due to timestamp buttons
                    locationToReapply = projectile.projectileObject.transform.position;
                    projectileRb.isKinematic = true;
                    isPaused = true;
                    inMotion = false;
                    forward1sTimeStampButton.SetActive(true);
                    back1sTimeStampButton.SetActive(true);
                }
                //else set kinematic to false so it can move and apply the previous velocity saved in velocityToReapply
                else
                {
                    projectileRb.isKinematic = false;
                    projectile.projectileObject.transform.position = locationToReapply;
                    projectileRb.velocity = velocityToReapply;
                    isPaused = false;
                    inMotion = true;
                    forward1sTimeStampButton.SetActive(false);
                    back1sTimeStampButton.SetActive(false);
                    displayCurrentVariables.ResetStack();
                    displayCurrentVariables.time = timeToReapply;
                }
            }
        }
        // this is attached to Move Button Click event
        // it moves the targetGameObject to the displacement entered by the user only if its a successfull user input
        public void OnDisplacementChange()
        {
            if (variableController.ErrorMessageText.text == "Successfull Input")
            {
                targetObject.transform.position = GameControl.control.initialPosition + GameControl.control.displacement;
            }
        }
    }
}

