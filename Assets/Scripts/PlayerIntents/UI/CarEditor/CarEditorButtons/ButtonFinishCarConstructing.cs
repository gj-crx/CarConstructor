using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

namespace Cars.CarEditor
{
    [RequireComponent(typeof(Button))]
    public class ButtonFinishCarConstructing : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        public void ExecuteButton()
        {
            if (PlayerRepresentation.LocalPlayer.Money < (CarConstructor.CurrentConstructedCar.TotalCost - CarConstructor.OldCarCost))
            {
                CarConstructor.CarEditorErrorHint.ShowError("Not enough money!");
                return;
            }

            if (PlayerRepresentation.LocalPlayer != null) PlayerRepresentation.LocalPlayer.Money -= (CarConstructor.CurrentConstructedCar.TotalCost - CarConstructor.OldCarCost);
            CarConstructor.OldCarCost = CarConstructor.CurrentConstructedCar.TotalCost;

            SerializationManager.SaveObject(CarConstructor.CurrentConstructedCar.CarName, Application.persistentDataPath + "/Cars/", new CarData(CarConstructor.CurrentConstructedCar));

            CarConstructor.CarEditorErrorHint.ShowError("Car constructed!", true);
        }
    }
}
