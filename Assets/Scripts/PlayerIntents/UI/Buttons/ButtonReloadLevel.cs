using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;
using System.Threading.Tasks;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonReloadLevel : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private async void ExecuteButton()
        {
            UIManager.Singleton.LoadingPanel.SetActive(true);
            await Task.Delay(25);
            await GameLevelSaverLoader.CurrentLoadedLevel.GenerateLevel();
            await Task.Delay(25);
            UIManager.Singleton.LoadingPanel.SetActive(false);


            if (PlayerRepresentation.LocalPlayer != null && PlayerRepresentation.LocalPlayer.SelectedCar != null)
            {
                PlayerRepresentation.LocalPlayer.SelectedCar.transform.position = SaveLoadSystem.GameLevelSaverLoader.CurrentLoadedLevel.StartingPosition.ToVector3();
                PlayerRepresentation.LocalPlayer.SelectedCar.transform.rotation = Quaternion.identity;
            }

            if (gameObject.tag == "DialogueButton")
            {

            }
            else
            {
                GamePauser.UnpauseGame();
            }
        }
    }
}
