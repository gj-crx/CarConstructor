using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SaveLoadSystem;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonDeleteCar : MonoBehaviour
    {
        public GameObject CarPreviewPanel;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private void ExecuteButton()
        {
            File.Delete(CarPreviewPanel.gameObject.GetComponent<ButtonSelectCar>().CarDataDirectory);

            Destroy(CarPreviewPanel.gameObject);

            if (PlayerRepresentation.LocalPlayer != null) PlayerRepresentation.LocalPlayer.Money += CarPreviewPanel.GetComponent<ButtonSelectCar>().representedCarData.TotalCost;

            transform.parent.gameObject.SetActive(false);
        }
    }
}
