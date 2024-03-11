using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    //abstract class mainly for just containing references to abstract functions for children classes and different measurements and variables specific to shape of projectile
    public abstract class Shape : Projectile
    {
        public float volume;
        public float mass;

        // all possible measurements for all different shapes avialable as options to user
        public float radius;
        public float length;
        public float width;
        public float height;

        public abstract float CalculateVolume();
        // used for adjusting the scale of gameobject so in game mesh is the right size in metres
        public abstract void SetScale();
        public abstract float CalculateMass();
    }
}
