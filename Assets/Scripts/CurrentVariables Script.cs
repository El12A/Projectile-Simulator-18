using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentVariablesScript : ProjectileMotion
{
    [SerializeField] private TMP_Text displacementText;
    [SerializeField] private TMP_Text velocityText;
    [SerializeField] private TMP_Text accelerationText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] ProjectileMotion proMotion;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        inMotion = proMotion.GetInMotion();
        if (inMotion == true)
        {
            projectileRb = proMotion.GetRigidBody();
            
            time += Time.deltaTime;
            // show displacement by getting string version of current position and taking it away from current rigidbody component position
            displacementText.text = "Displacement: " + (projectileRb.position - GameControl.control.initialPosition).ToString();

            velocityText.text = "Velocity: " + (projectileRb.GetPointVelocity(projectileRb.position) ).ToString();
            // use F = ma to get acceleration and 
            accelerationText.text = "Acceleration: " + (projectileRb.GetAccumulatedForce() / projectileRb.mass).ToString();
            timeText.text = "Time: " + time.ToString();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            time = 0;
            timeText.text = "Time: ";
            velocityText.text = "Velocity: ";
            accelerationText.text = "Acceleration: ";
            displacementText.text = "Displacement: ";
        }
    }
}
