using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace Assets.Scripts
{
    public class Body : MonoBehaviour
    {
        private Shape shape;
        private Cuboid cuboid = new Cuboid(2, 3, 4);
        
        public Dictionary<string, double> Materials =
            new Dictionary<string, double>()
            {
                {"Wood", 800},
                {"Polystyrene", 30},
                {"Glass", 2600},
                {"Lead", 11343},
                {"Iron", 7870},
                {"Gold", 19320}

            };
        public string materialName;
        private double density;
        public double mass;
        public bool isStatic;
        public double Restution;

        public Body(Shape shape, string material)
        {
            this.shape = shape;
            this.materialName = material;
            density = Materials[materialName];
            mass = density * shape.volume;
            shape.gameObject.GetComponent<Rigidbody>();
        }

        private void Awake()
        {

        }
    }
}
