using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;
using System.Threading.Tasks;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonGenerateGameLevel : MonoBehaviour
    {
        public GameLevelData RepresentedGameLevel { private get; set; }
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ExecuteButton);
        }


        private async void ExecuteButton()
        {
            UIManager.Singleton.LoadingPanel.SetActive(true);
            await Task.Delay(50);

            await RepresentedGameLevel.GenerateLevel();
            UIManager.Singleton.LoadingPanel.SetActive(false);

            PlayerLevelEditor.NewLevelToSave = RepresentedGameLevel;
        }
    }
}
