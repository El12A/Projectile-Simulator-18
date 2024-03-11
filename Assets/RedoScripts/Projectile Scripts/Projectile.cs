using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class Projectile : GameComponent
    {
        public GameObject projectileObject;
        public GameObject projectileCamera;

        public SphereProjectile sphereProjectile;  
        public CubeProjectile cubeProjectile;
        public CylinderProjectile cylinderProjectile;
        public ConeProjectile coneProjectile;
        public TeardropProjectile teardropProjectile;
        public Rigidbody projectileRb;
        [SerializeField] private MeshCollider projectileMeshCollider;
        [SerializeField] private MeshFilter projectileMeshFilter;
        [SerializeField] private MeshRenderer projectileMeshRenderer;
        [SerializeField] private PhysicMaterial physicMaterial;

        public string projectileShape;

        protected Dictionary<string, float> materials =
        new Dictionary<string, float>()
        {
        {"Wood", 800},
        {"Polystyrene", 30},
        {"Glass", 2600},
        {"Lead", 11343},
        {"Iron", 7870},
        {"Gold", 19320}
        };

        public string materialName;
        public float density;
        public float restitution;
        public float frictionalCoefficient;
        public float drag;

        public Vector3 initialPosition;
        public Vector3 currentVelocity;
        public Vector3 currentAcceleration;
        public Vector3 displacement;
        public Vector3 timeIsMoving;

        private void Start()
        {
            initialPosition = transform.position;
        }
        // retrieves the mesh collider and mesh filter and recalculates the collider based on the meshfilter eg sphere, cube, clinder etc.
        // This is used after there is change in the mesh component of the projectile so that the projectile collisions match the actual shape of the projectile
        // In addition friction and restitution is set for the physics material
        public virtual void UpdateMeshesCollidersAndPhyiscsMaterial()
        {
            projectileMeshCollider.sharedMesh = projectileMeshFilter.mesh;
            projectileMeshFilter.mesh.RecalculateBounds();
            projectileMeshCollider.sharedMesh.RecalculateBounds();
            physicMaterial.staticFriction = frictionalCoefficient;
            physicMaterial.dynamicFriction = frictionalCoefficient;
            physicMaterial.bounciness = restitution;
        }
        public float GetDensity()
        {
            density = materials[materialName];
            return density;
        }

        public virtual void UpdateRigidbody()
        {

        }

    }
}
