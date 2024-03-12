using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    // this class translates the target object based off of the displcement suvat variable.
    public class MoveTarget : VariableController
    {
        private void Start()
        {
            // get reference to the projectile script from projectile gameobject
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
        }
        public void OnDisplacementChange()
        {
            // if there has been a successful input then adjust the position of the target x distance from the projectile initial position, where x is the displacement suvat variable
            if (ErrorMessageText.text == "Successfull Input")
            {
                transform.position = projectile.initialPosition + variableController.displacement;
            }
        }
    }
}
