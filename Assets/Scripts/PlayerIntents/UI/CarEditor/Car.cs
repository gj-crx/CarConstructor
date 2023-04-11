using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cars.CarEditor;

namespace Cars
{
    public class Car : MonoBehaviour
    {
        public string CarName = "Unnamed custom car";
        public List<WheelType> Wheels = new List<WheelType>();
        public List<Collider2D> FrameParts = new List<Collider2D>();

        public EngineType Engine = null;

        public float DragRatePerMass = 15;

        public float PowerInput { private get; set; } = 0;
        public bool ContiniousAcceleration = false;

        [SerializeField]
        private int totalCost = 0;

        private int drivableWheelsCount = 0;
        private float inertialPower = 0;
        private Rigidbody2D frameRigidbody;

        public int TotalCost
        {
            get { return totalCost; }
            set
            {
                totalCost = value;

                if (gameObject.scene.name == "CarConstructorScene")
                {
                    CarConstructor.TotalCostTextIndicator.text = totalCost.ToString();
                    if (PlayerRepresentation.LocalPlayer.Money < totalCost) CarConstructor.TotalCostTextIndicator.color = new Color(0.85f, 0, 0);
                    else CarConstructor.TotalCostTextIndicator.color = new Color(0, 0.65f, 0);
                }
            }
        }

        private void Start()
        {
            frameRigidbody = GetComponent<Rigidbody2D>();

            if (gameObject.scene.name != "CarConstructorScene")
            {
                GameStateController.GameStateChangesEvent += OnGameStateChanges;
                OnGameStateChanges(); //initial invoke to get correct state
                CameraFollowing.ObjectToFollow = gameObject;
            }
            else
            {
                ChangeCarState(false);
            }

            RecalculateCarValues();
        }
        private void Update()
        {
            if (GameStateController.CurrentGameState != GameStateController.GameState.Live) return;

            if (ContiniousAcceleration) AccelerateCar();
            else DragDeceleration();
        }
        public void OnGameStateChanges()
        { //put car in static mode for game pausing or activate it
            if (GameStateController.CurrentGameState == GameStateController.GameState.Live) ChangeCarState(true);
            else ChangeCarState(false);
        }
        public void AccelerateCar()
        {
            if (inertialPower > 0 && PowerInput < 0) inertialPower = 0;
            else if (inertialPower < 0 && PowerInput > 0) inertialPower = 0;

            if (PowerInput > 0 && inertialPower < Engine.MaxPower) inertialPower += Engine.AccelerationRate * PowerInput * Time.deltaTime; 
            if (PowerInput < 0 && inertialPower > -Engine.MaxPower) inertialPower += Engine.AccelerationRate * PowerInput * Time.deltaTime; 

            ResetWheelStatus(true);
            ApplyTorqueOnWheels();
        }
        private void ResetWheelStatus(bool newStatus)
        {
            foreach (var wheel in Wheels) wheel.wheelJoint.useMotor = newStatus;
        }
        private void DragDeceleration()
        {
            if (inertialPower > 0)
            {
                if (inertialPower < 15) inertialPower = 0;
                else inertialPower -= DragRatePerMass * frameRigidbody.mass * Time.deltaTime;
            }
            else if (inertialPower < 0)
            {
                if (inertialPower > -15) inertialPower = 0;
                else inertialPower += DragRatePerMass * frameRigidbody.mass * Time.deltaTime;
            }

            ApplyTorqueOnWheels();
            ResetWheelStatus(false);
        }

        private void ApplyTorqueOnWheels()
        {
            float powerPerWheel = inertialPower / drivableWheelsCount; 

            for (int i = 0; i < Wheels.Count; i++)
            {
                if (Wheels[i].DrivableWheel) //torque applies only to drivable wheels
                {
                    JointMotor2D wheelMotor = Wheels[i].wheelJoint.motor;
                    wheelMotor.motorSpeed = powerPerWheel;
                    Wheels[i].wheelJoint.motor = wheelMotor;
                }
            }
        }

        private void ChangeCarState(bool gamePlayState)
        {
            if (gamePlayState)
            {
                frameRigidbody.bodyType = RigidbodyType2D.Dynamic;
                GetComponent<PolygonCollider2D>().isTrigger = false;
                foreach (Rigidbody2D carPartRigidbody in gameObject.GetComponentsInChildren<Rigidbody2D>()) carPartRigidbody.bodyType = RigidbodyType2D.Dynamic;

                foreach (var carPartCollider in gameObject.GetComponentsInChildren<CircleCollider2D>()) carPartCollider.isTrigger = false;
                foreach (var carPartCollider in gameObject.GetComponentsInChildren<BoxCollider2D>()) if (!carPartCollider.gameObject.CompareTag("TriggerColliderParts")) carPartCollider.isTrigger = false;

                RecalculateCarValues();
            }
            else
            {
                foreach (Rigidbody2D carPartRigidbody in gameObject.GetComponentsInChildren<Rigidbody2D>()) carPartRigidbody.bodyType = RigidbodyType2D.Kinematic;
                frameRigidbody.bodyType = RigidbodyType2D.Kinematic;
            }
        }
        private void RecalculateCarValues()
        {
            //drivable wheels
            drivableWheelsCount = 0;
            foreach (var wheel in Wheels) if (wheel.DrivableWheel) drivableWheelsCount++;
        }
        private void OnDestroy()
        {
            GameStateController.GameStateChangesEvent -= OnGameStateChanges;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Collectible"))
            {
                if (PlayerRepresentation.LocalPlayer != null) PlayerRepresentation.LocalPlayer.Money += collision.gameObject.GetComponent<Coin>().OnCollected();
            }
        }
    }
}
