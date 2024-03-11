using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{

    [SerializeField] private Rigidbody projectile;
    Vector3 velocity;
    Vector3 initialVelocity = new Vector3(0.0f, 10.0f, 0.2f);
    Vector3 finalVelocity = new Vector3(0.0f, 0.0f, 0.2f);
    float Acceleration = -8;
    Vector3 gravity;
    float time;
    Vector3 initialPosition;
    Vector3 Distance_Travelled;
    public bool airResistanceOn;
    private bool fired;
    private sphereProjectile projectileBody;

    void Start()
    {
        // turn off gravity for projectile
        projectile.useGravity = false;
        // Set initial starting position for projectile
        initialPosition = projectile.position;
        // V = u + at
        // T= v – u /a Equation 1 rearranged to get time of specified final velocity 
        time = (finalVelocity.y - initialVelocity.y) / Acceleration;
        // v^2 = u^2 + 2as
        // s = (v^2 – u^2)/2a Equation 4 to get distance travelled
        Distance_Travelled = (Vector3.Scale(finalVelocity, finalVelocity) - Vector3.Scale(initialVelocity, initialVelocity)) / (2 * Acceleration);
        // make gravity be the acceleration set by user
        gravity = new Vector3(0.0f, Acceleration, 0.0f);
        projectileBody = GetComponent<sphereProjectile>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fired = true;
        }

        if (fired == true)
        {
            // if user has selected to apply air Resistance then these calculations will be made
            if (airResistanceOn == true)
            {
                Vector3 airResistance = CalculateAirResistance(velocity);
                // Update velocity with air resistance (force of air resistance /mass = acceleration)
                //Using the v = root of u^2 + 2as
                velocity = CalculateVelocity(airResistance);
                // Update position
                transform.position += velocity * Time.deltaTime; // Calculate torque due to air resistance
                Vector3 torque = Vector3.Cross(transform.position, airResistance);
                // Update angular velocity 
                //angularVelocity += (torque / momentOfInertia) * Time.deltaTime;
                // Apply angular drag to angular velocity
                //angularVelocity -= angularDrag * angularVelocity;
                // Update rotation
                //transform.Rotate(angularVelocity * Time.deltaTime, Space.World);
            }


            else
            {
                //Doesn't apply air resistance
                //Calculate Position of projectile after each time Update
                // does this by adding calculated displacement after x time and adding it to initial position of projectile and moving the projectile to that position
                // This is the most cost efficient method of simulating projectile movement which will increase performance of the simulator greatly.
                projectile.position = initialPosition + initialVelocity * Time.deltaTime + Vector3.Scale(Vector3.up, gravity) * Time.deltaTime * Time.deltaTime * (1 / 2);
                Debug.Log(projectile.position);
            }
        }

    }

    public Vector3 CalculateVelocity(Vector3 AirResistance)
    {
        Vector3 acceleration = gravity + (AirResistance / (float)projectileBody.mass);
        // Calculate the new velocity components
        Vector3 newVelocityComponents = initialVelocity + 2 * acceleration * Time.deltaTime;
        // Apply the square root component-wise
        Vector3 sqrtVelocity = new Vector3(Mathf.Sqrt(newVelocityComponents.x), Mathf.Sqrt(newVelocityComponents.y), Mathf.Sqrt(newVelocityComponents.z));
        // Update the velocity
        velocity += sqrtVelocity;
        return velocity;

    }

    Vector3 CalculateAirResistance(Vector3 velocity)
    {
        // Simplified air resistance model (adjust as needed) 
        float speed = velocity.magnitude;
        float dragCoefficient = 0.5f; // Adjust based on the projectile shape 
        Vector3 normalizedVelocity = velocity.normalized;
        Vector3 airResistance = -dragCoefficient * speed * normalizedVelocity;
        return airResistance;
    }
}
