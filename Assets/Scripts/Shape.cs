using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Shape : MonoBehaviour
    {
        public double volume;
        [SerializeField] protected List<GameObject> Shapes = new List<GameObject>();
        public Shape()
        {

        }

        public virtual double CalculateVolume()
        {
            return volume;
        }


    }

    public class Sphere1 : Shape
    {
        public double radius;
        public GameObject sphereObject;
        
        public Sphere1(double r) 
        { 
            radius = r;
            CalculateVolume();
            sphereObject = Shapes[0];
        }

        public override double CalculateVolume()
        {
            // Volume formula for sphere: v = 4/3 x pi x r^3
            volume = (4/3) * Mathf.PI * radius * radius * radius;
            return volume;
        }
    }

    class Cuboid : Shape
    {
        public double length;
        public double height;
        public double width;
        public GameObject cubeObject;
        public Cuboid(double length, double height, double width)
        {
            this.length = length;
            this.height = height;
            this.width = width;
            CalculateVolume();
            cubeObject = Shapes[1];
        }

        public override double CalculateVolume()
        {
            // Volume formula for cuboid: v = l x h x w
            volume = length * height * width;
            return volume;
        }
    }

     class Cylinder : Shape
    {
        public double height;
        public double radius;
        public GameObject cylinderObject;
        public Cylinder(double r, double h)
        {
            radius = r;
            height = h;
            CalculateVolume();
            cylinderObject = Shapes[2];
        }
        public override double CalculateVolume()
        {
            // Volume formula for cylinder: v = pi x r^2 x h
            volume = Mathf.PI * radius * radius * height;
            return volume;
        }
    }
}
