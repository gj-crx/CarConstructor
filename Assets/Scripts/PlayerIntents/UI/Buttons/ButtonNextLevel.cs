using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;
using System.Threading.Tasks;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonNextLevel : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private async void ExecuteButton()
        {
            if (GameLevelSaverLoader.AllLoadedGameLevels.IndexOf(GameLevelSaverLoader.CurrentLoadedLevel) < GameLevelSaverLoader.AllLoadedGameLevels.Count - 1)
            {
                int nextLevelID = GameLevelSaverLoader.AllLoadedGameLevels.IndexOf(GameLevelSaverLoader.CurrentLoadedLevel) + 1;

                UIManager.Singleton.LoadingPanel.SetActive(true);
                await Task.Delay(50);
                await GameLevelSaverLoader.AllLoadedGameLevels[nextLevelID].GenerateLevel();
                UIManager.Singleton.LoadingPanel.SetActive(false);

                if (PlayerRepresentation.LocalPlayer != null && PlayerRepresentation.LocalPlayer.SelectedCar != null)
                {
                    PlayerRepresentation.LocalPlayer.SelectedCar.transform.position = SaveLoadSystem.GameLevelSaverLoader.CurrentLoadedLevel.StartingPosition.ToVector3();
                    PlayerRepresentation.LocalPlayer.SelectedCar.transform.rotation = Quaternion.identity;
                }
            }
            else
            {
                Debug.Log("No more levels left!");

            }
        }
    }
}
