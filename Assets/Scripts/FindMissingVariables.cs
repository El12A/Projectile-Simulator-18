using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMissingVariables : MonoBehaviour
{
    private Vector3 initialVelocity;
    private Vector3 finalVelocity;
    private Vector3 acceleration;
    private Vector3 displacement;
    private Vector3 time;

    public void CalculateMissingVariables()
    {
        initialVelocity = GameControl.control.initialVelocity;
        acceleration = GameControl.control.acceleration;
        finalVelocity = GameControl.control.finalVelocity;
        displacement = GameControl.control.displacement;
        time = GameControl.control.time;

        int varNum = 0;
        foreach (string variable in GameControl.control.selectedVariables)
        {
            if (variable == "Acceleration")
            {
                varNum = varNum + 1;
            }
            if (variable == "Displacement")
            {
                varNum = varNum + 5;
            }
            if (variable == "Final Velocity")
            {
                varNum = varNum + 10;
            }
            if (variable == "Initial Velocity")
            {
                varNum = varNum + 50;
            }
            if (variable == "Time")
            {
                varNum = varNum + 100;
            }
        }
        // Equation[1]: v = u + at
        // Equation[2]: s = 1/2(u + v)t
        // Equation[3]: v^2 = u^2 + 2as
        // Equation[4]: s = vt - 1/2at^2

        //IN = v,a,s      OUT: Equation [3]: u    Equation [1]: t
        if (varNum == 16)
        {
            initialVelocity.x = Mathf.Sqrt((finalVelocity.x * finalVelocity.x) - 2 * acceleration.x * displacement.x);
            initialVelocity.y = Mathf.Sqrt((finalVelocity.y * finalVelocity.y) - 2 * acceleration.y * displacement.y);
            initialVelocity.z = Mathf.Sqrt((finalVelocity.z * finalVelocity.z) - 2 * acceleration.z * displacement.z);
            CalculateTime();
        }
        //IN = s,a,u      OUT: Equation [3]: v    Equation [1]: t
        else if (varNum == 56)
        {
            finalVelocity.x = Mathf.Sqrt((initialVelocity.x * initialVelocity.x) + 2 * acceleration.x * displacement.x);
            finalVelocity.y = Mathf.Sqrt((initialVelocity.y * initialVelocity.y) + 2 * acceleration.y * displacement.y);
            finalVelocity.z = Mathf.Sqrt((initialVelocity.z * initialVelocity.z) + 2 * acceleration.z * displacement.z);
            CalculateTime();
        }
        //IN = s,a,t      OUT: Equation [4]: u    Equation [1]: v
        else if (varNum == 106)
        {
            initialVelocity.x = (displacement.x + 0.5f * acceleration.x * time.x * time.x) / time.x;
            initialVelocity.y = (displacement.y + 0.5f * acceleration.y * time.y * time.y) / time.y;
            initialVelocity.z = (displacement.z + 0.5f * acceleration.z * time.z * time.z) / time.z;

            finalVelocity.x = initialVelocity.x + acceleration.y * time.x;
            finalVelocity.y = initialVelocity.y + acceleration.y * time.y;
            finalVelocity.z = initialVelocity.z + acceleration.y * time.z;
        }
        //IN = v,u,a      OUT: Equation [1]: t    Equation [2]: s 
        else if (varNum == 61)
        {
            CalculateTime();
            CalculateDisplacement();
        }
        //IN = v,a,t      OUT: Equation [1]: u    Equation [2]: s
        else if (varNum == 111)
        {
            initialVelocity.x = finalVelocity.x - acceleration.x * time.x;
            initialVelocity.y = finalVelocity.y - acceleration.y * time.y;
            initialVelocity.z = finalVelocity.z - acceleration.z * time.z;
            CalculateDisplacement();
        }
        //IN = a,t,u      OUT: Equation [1]: v    Equation [2]: s
        else if (varNum == 151)
        {
            finalVelocity.x = initialVelocity.x + acceleration.x * time.x;
            finalVelocity.y = initialVelocity.y + acceleration.y * time.y;
            finalVelocity.z = initialVelocity.z + acceleration.z * time.z;
            CalculateDisplacement();
        }
        //IN = v,u,s      OUT: Equation [2]: t    Equation [1]: a
        else if (varNum == 65)
        {
            time.x = (displacement.x * 2) / (initialVelocity.x + finalVelocity.x);
            time.y = (displacement.y * 2) / (initialVelocity.y + finalVelocity.y);
            time.z = (displacement.z * 2) / (initialVelocity.z + finalVelocity.z);

            CalculateAcceleration();
        }
        //IN = v,t,s      OUT: Equation [3]: u    Equation [1]: a
        else if (varNum == 115)
        {
            initialVelocity.x = ((2 * displacement.x) / time.x) - finalVelocity.x;
            initialVelocity.y = ((2 * displacement.y) / time.y) - finalVelocity.y;
            initialVelocity.z = ((2 * displacement.z) / time.z) - finalVelocity.z;

            CalculateAcceleration();
        }
        //IN = s,u,t      OUT: Equation [3]: v    Equation [2]: a   
        else if (varNum == 155)
        {
            finalVelocity.x = ((2 * displacement.x) / time.x) - initialVelocity.x;
            finalVelocity.y = ((2 * displacement.y) / time.y) - initialVelocity.y;
            finalVelocity.z = ((2 * displacement.z) / time.z) - initialVelocity.z;

            CalculateAcceleration();
        }
        //IN = v,u,t      OUT: Equation [2]: s    Equation [1]: a    
        else if (varNum == 160)
        {
            CalculateDisplacement();
            CalculateAcceleration();
        }

        initialVelocity = HandleNaN(initialVelocity);
        finalVelocity = HandleNaN(finalVelocity);
        displacement = HandleNaN(displacement);
        time = HandleNaN(time);
        acceleration = HandleNaN(acceleration);

        GameControl.control.initialVelocity = initialVelocity;
        GameControl.control.acceleration = acceleration;
        GameControl.control.finalVelocity = finalVelocity;
        GameControl.control.displacement = displacement;
        GameControl.control.time = time;

    }

    private void CalculateAcceleration()
    {
        acceleration.x = (finalVelocity.x - initialVelocity.x) / time.x;
        acceleration.y = (finalVelocity.y - initialVelocity.y) / time.y;
        acceleration.z = (finalVelocity.z - initialVelocity.z) / time.z;
    }

    private void CalculateDisplacement()
    {
        displacement.x = 0.5f * (initialVelocity.x + finalVelocity.x) * time.x;
        displacement.y = 0.5f * (initialVelocity.y + finalVelocity.y) * time.y;
        displacement.z = 0.5f * (initialVelocity.z + finalVelocity.z) * time.z;
    }

    private void CalculateTime()
    {
        time.x = (finalVelocity.x - initialVelocity.x) / acceleration.x;
        time.y = (finalVelocity.y - initialVelocity.y) / acceleration.y;
        time.z = (finalVelocity.z - initialVelocity.z) / acceleration.z;
    }

    // converts any NaN (not a number) is returned this is due to problems like dividing by zero, the result should therefore be zero anyways if this is the case and therefore we set that component of the vector to zero 
    private Vector3 HandleNaN(Vector3 myVector)
    {
        Vector3 VectorToReturn = new Vector3();
        for (int i = 0; i < 3; i++)
        {
            if (float.IsNaN(myVector[i]))
            {
                VectorToReturn[i] = 0.0f;
            }
            else
            {
                VectorToReturn[i] = myVector[i];
            }
        }
        return VectorToReturn;
    }
}

