using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonStartGame : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }


        private void ExecuteButton()
        {
            if (gameObject.scene.name != "CarConstructorScene")
            {
                UIManager.Singleton.MainMenu.SetActive(false);
                UIManager.Singleton.CarSelection.SetActive(false);
                UIManager.Singleton.InGameControls.SetActive(true);
                UIManager.Singleton.StageSelection.SetActive(false);

                if (PlayerRepresentation.LocalPlayer != null && PlayerRepresentation.LocalPlayer.SelectedCar != null)
                {
                    PlayerRepresentation.LocalPlayer.SelectedCar.transform.position = SaveLoadSystem.GameLevelSaverLoader.CurrentLoadedLevel.StartingPosition.ToVector3();
                }
                GameStateController.CurrentGameState = GameStateController.GameState.Live;
            }
            else
            { //close car selection panel
                transform.parent.parent.parent.parent.gameObject.SetActive(false);
            }
        }
    }
}
