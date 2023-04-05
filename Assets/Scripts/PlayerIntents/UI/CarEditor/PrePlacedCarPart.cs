using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI;

namespace Cars.CarEditor
{
    /// <summary>
    /// Temporal script that is used by players to add new parts to the car via editor
    /// </summary>
    public class PrePlacedCarPart : MonoBehaviour
    {
        [SerializeField]
        private bool placedCorrectlyOnTheFrame = false;
        [SerializeField]
        private Color badPlacementColor = Color.red;

        private Color normalColor;
        private SpriteRenderer spriteRenderer;
        private CarPartType partType;
        private bool initialPositionSet = false;
        private bool buttonReleased = false;

        private readonly List<Collider2D> collidedCarParts = new List<Collider2D>();
        private Collider2D connectedFramePartCollider = null;
        

        private void Start()
        {
            partType = GetComponent<CarPartType>();

            spriteRenderer = GetComponent<SpriteRenderer>();
            normalColor = spriteRenderer.color;
            spriteRenderer.color = badPlacementColor;

            if (partType.Type == CarPartType.CarPartTypes.Engine) spriteRenderer.sortingOrder = 10; //temporal solution to actually see where engine is
        }

            private void LateUpdate() => PartPlaceSelection();
        private void PartPlaceSelection()
        {
            if (CarConstructor.ManualCarPartPlacementActive == false) return;

            if (Application.isMobilePlatform)
            { //touch control

                if (buttonReleased == false && Input.touchCount == 0) buttonReleased = true;

                //initial position and drag
                if (Input.touchCount > 0 && CarConstructor.PartPlacementJoystick.Horizontal == 0 && CarConstructor.PartPlacementJoystick.Vertical == 0)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(CarConstructor.ClickPlaceArea, Input.touches[0].position))
                    {
                        transform.position = RemoveZCord(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
                        initialPositionSet = true;
                    }
                }
            }
            else
            { //mouse control
                //initial position
                if (initialPositionSet == false && Input.GetMouseButtonDown(0) && CarConstructor.PartPlacementJoystick.Horizontal == 0 && CarConstructor.PartPlacementJoystick.Vertical == 0)
                {
                    transform.position = RemoveZCord(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    initialPositionSet = true;
                    return;
                }

                //drag
                if (initialPositionSet && Input.GetMouseButton(0) && CarConstructor.PartPlacementJoystick.Horizontal == 0 && CarConstructor.PartPlacementJoystick.Vertical == 0)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(CarConstructor.ClickPlaceArea, Input.mousePosition))
                    {
                        transform.position = RemoveZCord(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                }
            }
        }
        private Vector3 RemoveZCord(Vector3 sourceVector) => new Vector3(sourceVector.x, sourceVector.y, 0);


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.isTrigger)
            { //leaving frame
                placedCorrectlyOnTheFrame = false;
                spriteRenderer.color = badPlacementColor;
                if (connectedFramePartCollider == collision) connectedFramePartCollider = null;
            }
            else
            {
                collidedCarParts.Remove(collision);
                if (placedCorrectlyOnTheFrame && collidedCarParts.Count == 0) spriteRenderer.color = normalColor;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.isTrigger)
            {   //car part enters frame
                if (collidedCarParts.Count == 0) spriteRenderer.color = normalColor;
                connectedFramePartCollider = collision;
                placedCorrectlyOnTheFrame = true;
            }
            else
            { //car part meets other car parts collider
                collidedCarParts.Add(collision);
                spriteRenderer.color = badPlacementColor;
            }
        }


        public void PlaceCarPart()
        {
            if (placedCorrectlyOnTheFrame && collidedCarParts.Count == 0)
            {
                transform.SetParent(CarConstructor.CurrentConstructedCar.transform);
                partType.InitializeCarPart(CarConstructor.CurrentConstructedCar, connectedFramePartCollider);
                spriteRenderer.color = normalColor;

                Destroy(this); //removing JUST SCRIPT not object
            }
            else partType.RemoveCarPart(CarConstructor.CurrentConstructedCar);

        }
        public void ForcePlaceToCar(Car carToPlace, CarPartType.SerializableCarPart data)
        {
            CarPartType carPartType = GetComponent<CarPartType>();
            data.UpdateData(carPartType);

            if (carPartType.ConnectedFramePartID != -1) carPartType.InitializeCarPart(carToPlace, carToPlace.FrameParts[carPartType.ConnectedFramePartID]);
            else carPartType.InitializeCarPart(carToPlace, carToPlace.GetComponent<Collider2D>());

            Destroy(this); //removing JUST SCRIPT not object
        }
    }
}
