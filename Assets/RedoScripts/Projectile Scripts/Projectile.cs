using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class Projectile : GameComponent
    {
        // contains a reference to the object of the projectile
        // a reference to the camera object attached to the projectile gameobject
        public GameObject projectileObject;
        public GameObject projectileCamera;

        // a reference to all the different projectile scripts so they can be accesed by other scripts
        public SphereProjectile sphereProjectile;
        public CubeProjectile cubeProjectile;
        public CylinderProjectile cylinderProjectile;
        public ConeProjectile coneProjectile;
        public TeardropProjectile teardropProjectile;
        // a reference to the rigidbody component which needs to be frequently accessed in other scripts
        public Rigidbody projectileRb;
        // and references to the mesh components that make up the 3d aspect of the projectile
        [SerializeField] private MeshCollider projectileMeshCollider;
        [SerializeField] private MeshFilter projectileMeshFilter;
        [SerializeField] private MeshRenderer projectileMeshRenderer;
        [SerializeField] private PhysicMaterial physicMaterial;

        public string projectileShape;

        // dictionary for the materials that the projectile can be with correspondent densities
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

        // important non shape specific variables of the projectile
        public string materialName;
        public float density;
        public float restitution;
        public float frictionalCoefficient;
        public float drag;

        // make wherever the projectile is initially found at runtime as the initialposition
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
        // gets the density value using the densityName as a key for the density dictionary.
        public float GetDensity()
        {
            density = materials[materialName];
            return density;
        }

        // empty function that is specific to shape that is why it is empty here
        public virtual void UpdateRigidbody()
        {

        }

    }
}
