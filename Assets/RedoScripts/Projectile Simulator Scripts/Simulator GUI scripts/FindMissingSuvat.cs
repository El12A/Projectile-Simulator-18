using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class FindMissingSuvat : VariableController
    {
        private Vector3 u;
        private Vector3 v;
        private Vector3 a;
        private Vector3 s;
        private Vector3 t;

        public void CalculateMissingVariables()
        {
            //sets variables for suvat as their one letter symbols to make code much neater 
            u = variableController.initialVelocity;
            a = variableController.acceleration;
            v = variableController.finalVelocity;
            s = variableController.displacement;
            t = variableController.time;
            Debug.Log("u" + u + "a" + a + "v" + v + "s" + s + "t" + t);
            int varNum = 0;
            // using a simple number system to map to a different possibility as there are twn possible combinations for what goes in and what needs to be calculated.
            // so different varibles going in add up to give a specific number which is always different if the ouput variables are not the same.
            // then based on what number is produced it will calculate the correct missing variables at the if statements.
            // using number as then order of selectedVariables doesn't matter for the number unlike if a string variable was used that added a letter instead of number.
            foreach (string variable in variableController.selectedVariables)
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
                u.x = Mathf.Sqrt((v.x * v.x) - 2 * a.x * s.x);
                u.y = Mathf.Sqrt((v.y * v.y) - 2 * a.y * s.y);
                u.z = Mathf.Sqrt((v.z * v.z) - 2 * a.z * s.z);
                CalculateTime();
            }
            //IN = s,a,u      OUT: Equation [3]: v    Equation [1]: t
            else if (varNum == 56)
            {
                v.x = Mathf.Sqrt((u.x * u.x) + 2 * a.x * s.x);
                v.y = Mathf.Sqrt((u.y * u.y) + 2 * a.y * s.y);
                v.z = Mathf.Sqrt((u.z * u.z) + 2 * a.z * s.z);
                CalculateTime();
            }
            //IN = s,a,t      OUT: Equation [4]: u    Equation [1]: v
            else if (varNum == 106)
            {
                u.x = (s.x + 0.5f * a.x * t.x * t.x) / t.x;
                u.y = (s.y + 0.5f * a.y * t.y * t.y) / t.y;
                u.z = (s.z + 0.5f * a.z * t.z * t.z) / t.z;

                v.x = u.x + a.y * t.x;
                v.y = u.y + a.y * t.y;
                v.z = u.z + a.y * t.z;
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
                u.x = v.x - a.x * t.x;
                u.y = v.y - a.y * t.y;
                u.z = v.z - a.z * t.z;
                CalculateDisplacement();
            }
            //IN = a,t,u      OUT: Equation [1]: v    Equation [2]: s
            else if (varNum == 151)
            {
                v.x = u.x + a.x * t.x;
                v.y = u.y + a.y * t.y;
                v.z = u.z + a.z * t.z;
                CalculateDisplacement();
            }
            //IN = v,u,s      OUT: Equation [2]: t    Equation [1]: a
            else if (varNum == 65)
            {
                t.x = (s.x * 2) / (u.x + v.x);
                t.y = (s.y * 2) / (u.y + v.y);
                t.z = (s.z * 2) / (u.z + v.z);

                CalculateAcceleration();
            }
            //IN = v,t,s      OUT: Equation [3]: u    Equation [1]: a
            else if (varNum == 115)
            {
                u.x = ((2 * s.x) / t.x) - v.x;
                u.y = ((2 * s.y) / t.y) - v.y;
                u.z = ((2 * s.z) / t.z) - v.z;

                CalculateAcceleration();
            }
            //IN = s,u,t      OUT: Equation [3]: v    Equation [2]: a   
            else if (varNum == 155)
            {
                v.x = ((2 * s.x) / t.x) - u.x;
                v.y = ((2 * s.y) / t.y) - u.y;
                v.z = ((2 * s.z) / t.z) - u.z;

                CalculateAcceleration();
            }
            //IN = v,u,t      OUT: Equation [2]: s    Equation [1]: a    
            else if (varNum == 160)
            {
                CalculateDisplacement();
                CalculateAcceleration();
            }

            // handles any component that says NaN in any of the Vector3 float components and replaces it with zero.
            // This is because dividing by zero may occur if the user enters certain varibles with zero as the value and this causes NaN (not a number)
            u = HandleNaN(u);
            v = HandleNaN(v);
            s = HandleNaN(s);
            t = HandleNaN(t);
            a = HandleNaN(a);


            // all variables are set to either their unchanged values if they were inputted or they have been just calculated and are set to the correct number in the VariableController script
            variableController.initialVelocity = u;
            variableController.acceleration = a;
            variableController.finalVelocity = v;
            variableController.displacement = s;
            variableController.time = t;

        }

        // The three functions below each have 4 occurences of this equation so replaced with function to shorten length of code
        private void CalculateAcceleration()
        {
            a.x = (v.x - u.x) / t.x;
            a.y = (v.y - u.y) / t.y;
            a.z = (v.z - u.z) / t.z;
        }

        private void CalculateDisplacement()
        {
            s.x = 0.5f * (u.x + v.x) * t.x;
            s.y = 0.5f * (u.y + v.y) * t.y;
            s.z = 0.5f * (u.z + v.z) * t.z;
        }

        private void CalculateTime()
        {
            t.x = (v.x - u.x) / a.x;
            t.y = (v.y - u.y) / a.y;
            t.z = (v.z - u.z) / a.z;
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
}

