using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirResistance : MonoBehaviour
{
    public float density = 1.2f; // Air density (kg/m^3)
    public float mass = 1f; // Mass of the projectile (kg)
    public float dragCoefficient = 0.5f; // Drag coefficient of the projectile

    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshFilter meshFilter;

    void Start()
    {

    }

    void FixedUpdate()
    {
        // Calculate cross-sectional area
        float crossSectionalArea = CalculateCrossSectionalArea();
        Debug.Log("cross " + crossSectionalArea);
        // Calculate drag force
        Vector3 velocity = rb.velocity;
        float velocitySquared = velocity.sqrMagnitude;
        float dragForceMagnitude = 0.5f * density * velocitySquared * dragCoefficient * crossSectionalArea;

        // Apply drag force as acceleration
        Vector3 dragForceDirection = -velocity.normalized;
        Vector3 dragForce = dragForceMagnitude * dragForceDirection;
        Vector3 acceleration = dragForce / mass;
        rb.AddForce(acceleration, ForceMode.Acceleration);
    }

    float CalculateCrossSectionalArea()
    {
        // Initialize cross-sectional area
        float area = 0f;

        // Get the mesh of the object
        Mesh mesh = meshFilter.mesh;

        // Get vertices of the mesh
        Vector3[] vertices = mesh.vertices;

        // Get the direction of motion (opposite of velocity)
        Vector3 velocityDirection = -rb.velocity.normalized;

        // Iterate over each triangle in the mesh
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            // Get vertices of the triangle
            Vector3 v0 = vertices[mesh.triangles[i]];
            Vector3 v1 = vertices[mesh.triangles[i + 1]];
            Vector3 v2 = vertices[mesh.triangles[i + 2]];

            // Calculate normal of the triangle
            Vector3 triangleNormal = Vector3.Cross(v1 - v0, v2 - v0).normalized;

            // Check if the triangle is facing the opposite direction of motion
            if (Vector3.Dot(triangleNormal, velocityDirection) < 0)
            {
                // Calculate area of the triangle
                float triangleArea = Vector3.Cross(v1 - v0, v2 - v0).magnitude / 2f;

                // Add area of the triangle to the total cross-sectional area
                area += triangleArea;
            }
        }

        return area;
    }
}
