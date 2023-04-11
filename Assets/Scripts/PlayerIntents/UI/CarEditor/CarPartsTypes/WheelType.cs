using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cars;

namespace Cars
{
    public class WheelType : MonoBehaviour
    {
        public bool DrivableWheel = true;

        public float powerLimit = 2000;

        [HideInInspector]
        public WheelJoint2D wheelJoint;

        private void Awake()
        {
            wheelJoint = GetComponent<WheelJoint2D>();
        }
    }
}
