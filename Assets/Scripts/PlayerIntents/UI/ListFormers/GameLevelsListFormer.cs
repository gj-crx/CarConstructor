using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace UI
{
    public static class GameLevelsListFormer
    {
        public static int totalStarsAchievedThisStage = 0;
        public static int totalStarsThisStage = 0;

        private static List<GameObject> levelPanels = new List<GameObject>();

        public static void FormList()
        {
            if (CarListFormer.HasAnyCarsToLoad() == false) //no cars to load
            {  //sending player right into car constructor scene to actually build some cars
                Debug.Log("no cars to load");
                UIManager.Singleton.LoadingPanel.gameObject.SetActive(true);
                SceneManager.LoadSceneAsync("CarConstructorScene", LoadSceneMode.Single);
                return;
            }

            for (int i = 0; i < levelPanels.Count; i++) GameObject.Destroy(levelPanels[i]);
            levelPanels.Clear();

            if (PlayerRepresentation.LocalPlayer.LevelProgressData.Count < GameLevelSaverLoader.AllLoadedGameLevels.Count) 
                PlayerRepresentation.LocalPlayer.ExpandLevelsProgressData(GameLevelSaverLoader.AllLoadedGameLevels.Count);

            totalStarsAchievedThisStage = 0;
            totalStarsThisStage = 0;

            foreach (var gameLevel in GameLevelSaverLoader.AllLoadedGameLevels)
            {
                totalStarsThisStage += 3;
                totalStarsAchievedThisStage += (int)PlayerRepresentation.LocalPlayer.LevelProgressData[gameLevel.LevelID];

                GameObject gameLevelPanel = null;

                if (gameLevel.AlwaysUnlocked == false 
                    && (PlayerRepresentation.LocalPlayer.LevelProgressData.Count <= gameLevel.LevelID 
                        || PlayerRepresentation.LocalPlayer.LevelProgressData[gameLevel.LevelID] == PlayerRepresentation.Player.LevelStatus.Locked))
                { //locked stage
                    gameLevelPanel = GameObject.Instantiate(PrefabManager.GameLevelItemPrefabs[4]);
                }
                else
                {
                    gameLevelPanel = GameObject.Instantiate(PrefabManager.GameLevelItemPrefabs[(sbyte)PlayerRepresentation.LocalPlayer.LevelProgressData[gameLevel.LevelID]]);
                    gameLevelPanel.GetComponent<Buttons.ButtonGenerateGameLevel>().RepresentedGameLevel = gameLevel;

                    gameLevelPanel.transform.Find("Text").GetComponent<TMP_Text>().text = (gameLevel.LevelID + 1).ToString();
                }

                gameLevelPanel.transform.SetParent(UIManager.Singleton.contentGameLevels.transform);
                gameLevelPanel.transform.localScale = Vector3.one;
                levelPanels.Add(gameLevelPanel);

                UIManager.Singleton.StarStatusIndicator.text = totalStarsAchievedThisStage.ToString() + "/" + totalStarsThisStage.ToString();
            }
        }

        
    }
}
