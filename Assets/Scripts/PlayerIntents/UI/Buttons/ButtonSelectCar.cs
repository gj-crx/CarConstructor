using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cars.CarEditor;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonSelectCar : MonoBehaviour
    {
        public Cars.CarEditor.CarData representedCarData;
        public string CarDataDirectory;
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private void ExecuteButton()
        {
            if (gameObject.scene.name != "CarConstructorScene")
            {
                if (PlayerRepresentation.LocalPlayer.SelectedCar != null) Destroy(PlayerRepresentation.LocalPlayer.SelectedCar);
                PlayerRepresentation.LocalPlayer.SelectedCar = CarConstructor.LoadCar(representedCarData);
            }
            else
            {
                if (CarConstructor.CurrentConstructedCar != null) Destroy(CarConstructor.CurrentConstructedCar.gameObject);
                CarConstructor.CurrentConstructedCar = CarConstructor.LoadCar(representedCarData);
            }
        }
    }
}
