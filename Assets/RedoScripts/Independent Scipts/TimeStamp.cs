using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class TimeStamp
    {
        public float time;
        public Vector3 velocity;
        public Vector3 acceleration;
        public Vector3 displacement;
        public Vector3 rotation;

        public TimeStamp(float time, Vector3 velocity, Vector3 acceleration, Vector3 displacement, Vector3 rotation)
        {
            this.time = time;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.displacement = displacement;
            this.rotation = rotation;
        }
    }
}

