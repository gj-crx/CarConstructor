using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Buttons
{
    public class GamePauser : MonoBehaviour
    {
        private static float normalTimeScale = 1;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && (GameStateController.CurrentGameState == GameStateController.GameState.Live || GameStateController.CurrentGameState == GameStateController.GameState.Paused))
            {
                if (UIManager.Singleton.PausePopup.activeInHierarchy == false)
                {
                    PauseGame();
                }
                else
                {
                    UnpauseGame();
                }
            }
        }
        public static void PauseGame()
        {
            UIManager.Singleton.PausePopup.SetActive(true);

            GameStateController.CurrentGameState = GameStateController.GameState.Paused;

            normalTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        public static void UnpauseGame()
        {
            UIManager.Singleton.PausePopup.SetActive(false);

            GameStateController.CurrentGameState = GameStateController.GameState.Live;

            Time.timeScale = normalTimeScale;
        }
    }
}
