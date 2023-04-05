using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

namespace Cars.CarEditor
{
    [RequireComponent(typeof(Button))]
    public class ButtonPlaceCarPart : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }



        public void ExecuteButton()
        {
            CarConstructor.CurrentPlacedCarPart.GetComponent<PrePlacedCarPart>().PlaceCarPart();
            CarConstructor.CurrentPlacedCarPart = null;
            gameObject.SetActive(false);
        }
    }
}
