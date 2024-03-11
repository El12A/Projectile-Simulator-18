using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    protected GameObject projectile1;
    protected Rigidbody projectileRb;
    private bool airResistanceOn;

    private Vector3 initialVelocity;
    private Vector3 acceleration;
    private float mass;

    private bool reset = true;
    protected bool inMotion;
    private bool isPaused = false;

    protected Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 VelocityToReapply;




    // Start is called before the first frame update
    void Start()
    {
        // set initial position of the projectile
        projectile1 = GameObject.FindWithTag("projectile");
        projectileRb = projectile1.GetComponent<Rigidbody>();
        initialRotation = projectileRb.rotation;
        projectileRb.useGravity = false;
        initialPosition = projectile1.transform.position;
        GameControl.control.veryInitialPosition = initialPosition;
        GameControl.control.initialPosition = initialPosition;
        mass = projectileRb.mass;
        inMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        // as soon as the user hits the space bar this will allow the fireprojectile function to be triggered
        if (Input.GetKeyDown(KeyCode.Space) && reset == true)
        {
            inMotion = true;
            reset = false;
            projectileRb.isKinematic = false;
            initialVelocity = GameControl.control.initialVelocity;
            acceleration = GameControl.control.acceleration;
            projectileRb.velocity = initialVelocity;
        }

        // when R is pressed the projectile is reset.
        if (Input.GetKeyDown(KeyCode.R) && reset == false)
        {
            ResetProjectile();
        }

        if (inMotion == true)
        {
            projectileRb.AddForce(acceleration * mass, ForceMode.Force);
        }

    }

    private void ResetProjectile()
    {
        projectileRb.velocity = Vector3.zero;
        projectileRb.angularVelocity = Vector3.zero;
        projectileRb.isKinematic = true;
        projectile1.transform.position = GameControl.control.initialPosition;
        reset = true;
        inMotion = false;
        Debug.Log("reset");
    }

    public void FreezeProjectile()
    {
        if (isPaused == false)
        {
            VelocityToReapply = projectileRb.GetPointVelocity(projectileRb.position);
            projectileRb.isKinematic = true;
            isPaused = true;
            inMotion = false;
        }
        else
        {
            projectileRb.isKinematic = false;
            projectileRb.velocity = VelocityToReapply;
            isPaused = false;
            inMotion = true;
        }
        
    }

    public Rigidbody GetRigidBody()
    {
        return projectileRb;
    }
    public bool GetInMotion()
    {
        return inMotion;
    }
}
