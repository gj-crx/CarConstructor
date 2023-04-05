using UnityEngine;
using UnityEngine.UI;

namespace Cars.CarEditor
{
    [RequireComponent(typeof(Button))]
    public class ButtonAddCarPart : MonoBehaviour
    {
        public CarPartType CarPartPrefabToPlace;
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }


        public void ExecuteButton()
        {
            if (CarConstructor.CurrentPlacedCarPart != null)
            {
                GameObject.Destroy(CarConstructor.CurrentPlacedCarPart.gameObject);
                CarConstructor.CurrentPlacedCarPart = null;
            }

            CarConstructor.RemoverToolsIsOn = false;

            if (CarPartPrefabToPlace.Type != CarPartType.CarPartTypes.Frame)
            {   //Spawning part object
                var newCarPart = GameObject.Instantiate(CarPartPrefabToPlace.gameObject);
                newCarPart.transform.position = new Vector3(-4.5f, 0, 0);
                try
                {
                    newCarPart.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    CarConstructor.CurrentPlacedCarPart = newCarPart.GetComponent<CarPartType>();
                }
                catch { }

                CarConstructor.CurrentConstructedCar.TotalCost += CarPartPrefabToPlace.Cost;
            }
            else
            {   //frame replaces whole car
                string oldCarName = CarConstructor.CurrentConstructedCar.CarName;
                Destroy(CarConstructor.CurrentConstructedCar.gameObject);
                CarConstructor.CurrentConstructedCar = GameObject.Instantiate(CarPartPrefabToPlace.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Car>();
                CarConstructor.CurrentConstructedCar.CarName = oldCarName;
                CarConstructor.CurrentConstructedCar.TotalCost = CarPartPrefabToPlace.Cost;
            }
        }
    }
}
