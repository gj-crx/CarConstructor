using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonNextLevel : MonoBehaviour
    {

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }

        
        private void ExecuteButton()
        {
            if (GameLevelSaverLoader.AllLoadedGameLevels.IndexOf(GameLevelSaverLoader.CurrentLoadedLevel) < GameLevelSaverLoader.AllLoadedGameLevels.Count - 1)
            {
                GameLevelSaverLoader.CurrentLoadedLevel = GameLevelSaverLoader.AllLoadedGameLevels[GameLevelSaverLoader.AllLoadedGameLevels.IndexOf(GameLevelSaverLoader.CurrentLoadedLevel) + 1];
                GameLevelSaverLoader.CurrentLoadedLevel.GenerateLevel();
            }
            else
            {
                Debug.Log("No more levels left!");

            }
        }
    }
}
