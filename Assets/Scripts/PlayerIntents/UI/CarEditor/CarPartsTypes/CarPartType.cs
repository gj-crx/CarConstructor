using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars.CarEditor
{
    /// <summary>
    /// Represents any placed car part
    /// </summary>
    public class CarPartType : MonoBehaviour
    {
        public string CarPartName = "Unnamed car part";
        public short CarPartTypeID = 0;
        /// <summary>
        /// -1 = main frame
        /// </summary>
        public short ConnectedFramePartID = -1;
        public CarPartTypes Type;
        public Sprite Icon;
        public int Cost;

        private float dragRemoveTimer = 0;
        private const float timeToRemovePart = 1.7f;
        private bool alreadyPlaced = false;
        private Color normalColor;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            try
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                normalColor = spriteRenderer.color;
            } catch { }
        }

        public void InitializeCarPart(Car car, Collider2D connectedFramePart)
        {
            //wheels
            if (Type == CarPartTypes.Wheel)
            {
                ConnectedFramePartID = (short)car.FrameParts.IndexOf(connectedFramePart);

                WheelType wheelType = GetComponent<WheelType>();
                if (ConnectedFramePartID == -1) wheelType.wheelJoint.connectedBody = car.GetComponent<Rigidbody2D>();
                else wheelType.wheelJoint.connectedBody = car.FrameParts[ConnectedFramePartID].GetComponent<Rigidbody2D>();

                wheelType.wheelJoint.connectedAnchor = wheelType.wheelJoint.connectedBody.transform.InverseTransformPoint(transform.position);
                
                car.Wheels.Add(wheelType);
            }
            //engines
            if (Type == CarPartType.CarPartTypes.Engine)
            {
                if (car.Engine != null) car.Engine.gameObject.GetComponent<CarPartType>().RemoveCarPart(car);
                car.Engine = GetComponent<EngineType>();
                spriteRenderer.sortingOrder = -10;
                GetComponent<FixedJoint2D>().connectedBody = car.GetComponent<Rigidbody2D>();
            }

            //frame parts
            if (Type == CarPartType.CarPartTypes.FrameParts)
            {
                //        FixedJoint2D framePartJoint = GetComponent<FixedJoint2D>();
                //        framePartJoint.connectedBody = connectedFramePart.gameObject.GetComponent<Rigidbody2D>();
                //    framePartJoint.connectedAnchor = framePartJoint.transform.InverseTransformPoint(transform.position);

                GetComponent<FramePartType>().Angle = transform.localEulerAngles.z;
                ConnectedFramePartID = -1; //frame parts are always connected to main frame (for now)
                car.FrameParts.Add(GetComponent<Collider2D>());
            }

            alreadyPlaced = true;

            try { GetComponent<CircleCollider2D>().isTrigger = false; } catch { }
        }
        public void RemoveCarPart(Car car)
        {
            car.TotalCost -= Cost;

            if (Type == CarPartTypes.FrameParts) CarConstructor.CurrentConstructedCar.FrameParts.Remove(GetComponent<Collider2D>());

            Destroy(gameObject);
        }

        [System.Serializable]
        public enum CarPartTypes : byte
        {
            Wheel = 0,
            Engine = 1,
            Frame = 2,
            BrakeSystem = 3,
            FrameParts = 4
        }
        
        [System.Serializable]
        public class SerializableCarPart
        {
            public short CarPartTypeID = 0;
            public short ConnectedFramePartID = -1; 
            public Position CarPartPlacementPosition;
            public float Angle = 0;
            
            public SerializableCarPart(CarPartType carPart)
            {
                CarPartTypeID = carPart.CarPartTypeID;
                ConnectedFramePartID = carPart.ConnectedFramePartID;
                CarPartPlacementPosition = new Position(carPart.gameObject.transform.localPosition);
                Angle = carPart.transform.localEulerAngles.z;
            }
            public void UpdateData(CarPartType carPartToUpdate)
            {
                carPartToUpdate.ConnectedFramePartID = ConnectedFramePartID;
                carPartToUpdate.transform.localEulerAngles = new Vector3(0, 0, Angle);
            }
        }

        private void OnMouseDrag()
        {
            if (gameObject.scene.name == "CarConstructorScene" && Type != CarPartTypes.Frame)
            {
                if (CarConstructor.RemoverToolsIsOn)
                {
                    RemoveCarPart(CarConstructor.CurrentConstructedCar);
                }
                else
                {
                    if (alreadyPlaced == false) return;

                    dragRemoveTimer += Time.deltaTime;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g - Time.deltaTime / timeToRemovePart, spriteRenderer.color.b - Time.deltaTime / timeToRemovePart);
                    if (dragRemoveTimer > timeToRemovePart) RemoveCarPart(CarConstructor.CurrentConstructedCar);
                }
            }
        }
        private void OnMouseUp()
        {
            if (Type == CarPartTypes.Frame) return;
            if (transform.parent != null)
            {
                dragRemoveTimer = 0;
                spriteRenderer.color = normalColor;
            }
        }
    }
}
