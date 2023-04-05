using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonCarDeletionDialogue : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private void ExecuteButton()
        {
            GameObject carDeletionDialogue = transform.parent.parent.parent.parent.parent.Find("CarDeletionDialogue").gameObject;
            carDeletionDialogue.gameObject.SetActive(true);
            carDeletionDialogue.transform.Find("Button_Sell").GetComponent<ButtonDeleteCar>().CarPreviewPanel = transform.parent.gameObject;

        }
    }
}
