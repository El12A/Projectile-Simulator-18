using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class MoveTarget : VariableController
    {
        private void Start()
        {
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
        }
        public void OnDisplacementChange()
        {
            if (ErrorMessageText.text == "Successfull Input")
            {
                transform.position = projectile.initialPosition + variableController.displacement;
            }
        }
    }
}

