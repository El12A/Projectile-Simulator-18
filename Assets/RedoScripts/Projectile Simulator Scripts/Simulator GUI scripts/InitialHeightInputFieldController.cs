using PhysicsProjectileSimulator;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    // this class handlees the starting height of the projectile and projectile shooter
    public class InitialHeightInputFieldController : PhysicsSimulator
    {
        [SerializeField] private GameObject projectileShooter;
        public InputFieldRestrictor inputFieldRestrictor;
        
        private void Start()
        {
            // on start get reference to projectile script of projectile
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
        }
        public void OnInputHeightEndEdit()
        {
            // calls the onEndEdit function attached to the InputField of InitialHeight 
            inputFieldRestrictor.OnEndEdit();
            float initialHeight = float.Parse(inputFieldRestrictor.inputField.text);

            if (initialHeight <= 50 && initialHeight >= 1)
            {
                Vector3 shooter = projectileShooter.transform.position;
                // set new height of the projectile shooter gameobject based on input
                projectileShooter.transform.position = new Vector3(shooter.x, shooter.y + initialHeight - 1, shooter.z);
                // set new height of projectile aswell to that of the projectile shooter
                Vector3 proj = projectile.initialPosition;
                projectile.initialPosition = new Vector3(proj.x, proj.y + initialHeight - 1, proj.z);
                projectile.projectileObject.transform.position = projectile.initialPosition;
                //reupdate the variables based off the new starting position and also reupdates the target position
                variableController.UpdateInputtedVariables();
            }
            else
            {
                // if input is not in range show error message saying so and do not update initial height
                variableController.ErrorMessageText.text = "InputHeight Must be atleast 1m and no more than 50m";
            }
        }
    }
}
