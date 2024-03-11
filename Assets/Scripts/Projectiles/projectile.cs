using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class projectile : ScriptableObject
    {
        protected Dictionary<string, double> Materials =
        new Dictionary<string, double>()
        {
            {"Wood", 800},
            {"Polystyrene", 30},
            {"Glass", 2600},
            {"Lead", 11343},
            {"Iron", 7870},
            {"Gold", 19320}
        };
        protected string materialName;
        protected double volume;
        protected double density;
        protected double mass;
        protected bool isStatic;
        protected double Restution;



        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
