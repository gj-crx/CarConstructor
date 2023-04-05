using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonStartLevelSelection : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private void ExecuteButton()
        {
            UIManager.Singleton.MainMenu.SetActive(false);
            UIManager.Singleton.CarSelection.SetActive(false);
            UIManager.Singleton.InGameControls.SetActive(false);
            UIManager.Singleton.StageSelection.SetActive(true);

            GameLevelsListFormer.FormList();
        }
    }
}
