using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager.Requests;
using UnityEngine.UIElements;
using Assets.Scripts;
using TMPro.EditorUtilities;

public class BallLauncher : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    
    public Rigidbody ball;
    public Transform target;
    private bool Reset = true;
    public Vector3 ballLocation;
    public float h = 25;
    public float gravity = -18;

    public bool debugPath;
    [Header("Display Controls")]
    [SerializeField]
    [Range(10, 1000)]
    private int LinePoints;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float TimeBetweenPoints = 0.05f;

    void Start()
    {
        //gravity is off until ball is launched so it doesn't start falling on start.
        ball.useGravity = false;
        ballLocation = ball.position;
        Vector3 allVelocity = CalculateLaunchData().initialVelocity;
        Visualize(allVelocity);
        lineRenderer.positionCount = LinePoints;
    }

    void Update()
    {
        
        // when space key is pressed the ball is launched towards the target

        if (Input.GetKeyDown(KeyCode.Space) && Reset == true)
        {
            Launch();
            
            Reset = false;
        }

        // allows user to rest the ball to original position after they have launched the ball.
        if (Input.GetKeyDown(KeyCode.R) && Reset == false)
        {
            ball.useGravity = false;
            ball.velocity = Vector3.zero;
            ball.position = ballLocation;
            Reset = true;
        }

        //if debugPath is checked true it will draw the path in editor only.
        if (debugPath)
        {
            DrawPath();
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        // using kinematic equation for calculating time for ball to hit target
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        // using kinematic equations for calculating the vector velocities for different axis.
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }


    Vector3 CalculatePositionInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = ballLocation + vo * time;
        float sY = (-0.5f * Mathf.Abs(gravity) * (time*time)) +(vo.y * time) + ballLocation.y;

        result.y = sY;

        return result;

    }

    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < LinePoints; i++)
        {
            Vector3 pos = CalculatePositionInTime(vo, 5 * i / (float)LinePoints);
            lineRenderer.SetPosition(i, pos);
        }
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}

    // followed this youtube tutorial:
    // https://www.youtube.com/watch?v=IvT8hjy6q4o
    // it helped me make a simple projectile simulator for a ball hitting a target
    // code also found on github:
    // https://github.com/SebLague/Kinematic-Equation-Problems/blob/master/Kinematics%20problem%2002/Assets/Scripts/BallLauncher.cs
    // I hand copied it from video and made comments whilst watching the video explaining the code to myself so i understand it
    // I then edited the code to make a trajectory which is calculated for the projectile before it hits the target.


