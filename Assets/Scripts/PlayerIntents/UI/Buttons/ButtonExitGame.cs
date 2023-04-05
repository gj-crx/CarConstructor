using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;
using UnityEngine.EventSystems;

namespace UI.Buttons
{
    public class ButtonExitGame : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }


        private void ExecuteButton()
        {
            Application.Quit();
        }

    }
}
