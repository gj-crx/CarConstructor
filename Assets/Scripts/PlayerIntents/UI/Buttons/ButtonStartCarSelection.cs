using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonStartCarSelection : MonoBehaviour
    {
        public bool StageIsUnlocked = false;
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private void ExecuteButton()
        {
            if (StageIsUnlocked)
            {
                UIManager.Singleton.MainMenu.SetActive(false);
                UIManager.Singleton.CarSelection.SetActive(true);
                UIManager.Singleton.InGameControls.SetActive(false);
                UIManager.Singleton.StageSelection.SetActive(false);
            }
            else
            {
                Debug.Log("Need to do an animation here");
            }
        }
    }
}
