using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;
using TMPro;

namespace UI
{
    public static class GameLevelsListFormer
    {
        private static List<GameObject> levelPanels = new List<GameObject>();

        public static void FormList()
        {
            for (int i = 0; i < levelPanels.Count; i++) GameObject.Destroy(levelPanels[i]);
            levelPanels.Clear();

            if (PlayerRepresentation.LocalPlayer.LevelProgressData.Count < GameLevelSaverLoader.AllLoadedGameLevels.Count) 
                PlayerRepresentation.LocalPlayer.ExpandLevelsProgressData(GameLevelSaverLoader.AllLoadedGameLevels.Count);

            foreach (var gameLevel in GameLevelSaverLoader.AllLoadedGameLevels)
            {
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
            }
        }
    }
}
