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
        public float wheelPowerEfficiency = 1.0f;

        [HideInInspector]
        public WheelJoint2D wheelJoint;

        private void Awake()
        {
            wheelJoint = GetComponent<WheelJoint2D>();
            if (DrivableWheel == false) wheelJoint.useMotor = false;
        }
    }
}
