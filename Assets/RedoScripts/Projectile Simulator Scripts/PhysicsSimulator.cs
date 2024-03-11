using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    // This is the Physics Simulator Scene Script 
    // Index of this scene is 1
    public class PhysicsSimulator : SceneController
    {
        public ProjectileMotion projectileMotion;
        public VariableController variableController;


        public void OnExitButtonClick()
        {
            // going from scene index 1 to 0 (from physics simulator to main menu)
            ChangeScene(1,0);
        }

        public void OnProjectileEditorButtonClick()
        {
            // going from scene index 1 to 2 (from physics simulator to projectile editor)
            ChangeScene(1,2);
        }
    }
}

