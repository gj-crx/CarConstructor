using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonBackToMenu : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private void ExecuteButton()
        {
            UIManager.Singleton.MainMenu.SetActive(true);
            UIManager.Singleton.CarSelection.SetActive(false);
            UIManager.Singleton.InGameControls.SetActive(false);
            UIManager.Singleton.StageSelection.SetActive(false);

            GamePauser.UnpauseGame();

            if (PlayerRepresentation.LocalPlayer.SelectedCar != null) GameObject.Destroy(PlayerRepresentation.LocalPlayer.SelectedCar.gameObject);
            PlayerRepresentation.LocalPlayer.SelectedCar = null;

            GameStateController.CurrentGameState = GameStateController.GameState.InMenu;
        }
    }
}
