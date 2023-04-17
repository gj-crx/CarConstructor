using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;
using System.Threading.Tasks;

public static class GameScenarioController
{
    public static int CurrentAchievedStarsAmount = 3;

    public static int checkIntervalMilliseconds = 500;



    private static float lastCheckPositionX = 0;
    
    private static float distanceInterval = 0.25f;

    private static byte accumulatedStuckMarkers = 0;


    public static void LevelBegunEvent()
    {
        if (GameStateController.CurrentGameState == GameStateController.GameState.Live) CameraFollowing.ObjectToFollow = PlayerRepresentation.LocalPlayer.SelectedCar.gameObject;
    }

    public static async void LevelWinConditionCheckAsync()
    {
        while (GameStateController.CurrentGameState == GameStateController.GameState.Live && PlayerRepresentation.LocalPlayer != null)
        {
            if (GameLevelSaverLoader.CurrentLoadedLevel != null && PlayerRepresentation.LocalPlayer.SelectedCar != null &&      //loading panel should not be active to check win condition
                GameLevelSaverLoader.CurrentLoadedLevel.LevelIsActive && UI.UIManager.Singleton.LoadingPanel.activeInHierarchy == false 
               && GameLevelSaverLoader.CurrentLoadedLevel.WinCondition.CheckWinCondition(PlayerRepresentation.LocalPlayer.SelectedCar.transform.position))
            {
                int achievedStarDifference = CurrentAchievedStarsAmount - (int)PlayerRepresentation.LocalPlayer.LevelProgressData[GameLevelSaverLoader.CurrentLoadedLevel.LevelID];
                if (achievedStarDifference > 0) //if player got more stars than before he gets a reward
                {
                    PlayerRepresentation.LocalPlayer.Money += GameLevelSaverLoader.CurrentLoadedLevel.CoinRewardPerStar * achievedStarDifference;
                    PlayerRepresentation.LocalPlayer.LevelProgressData[GameLevelSaverLoader.CurrentLoadedLevel.LevelID] = (PlayerRepresentation.Player.LevelStatus)CurrentAchievedStarsAmount;
                }

                string[] arguments = new string[2];
                arguments[0] = CurrentAchievedStarsAmount.ToString();
                arguments[1] = (GameLevelSaverLoader.CurrentLoadedLevel.CoinRewardPerStar * achievedStarDifference).ToString(); //players gets rewards only once

                UI.UIManager.Singleton.ShowDialogue("MissionCompleted", arguments);

                GameLevelSaverLoader.CurrentLoadedLevel.LevelIsActive = false;
            }
            await Task.Delay(checkIntervalMilliseconds);
        }
    }

    public static async void CarStuckCheck()
    {
        while (GameStateController.CurrentGameState == GameStateController.GameState.Live && PlayerRepresentation.LocalPlayer != null)
        {
            if (GameStateController.CurrentGameState == GameStateController.GameState.Live && PlayerRepresentation.LocalPlayer.SelectedCar != null && PlayerRepresentation.LocalPlayer.SelectedCar.gameObject.activeInHierarchy)
            {
                Transform carTransform = PlayerRepresentation.LocalPlayer.SelectedCar.gameObject.transform;

                if (Mathf.Abs(carTransform.position.x - lastCheckPositionX) < distanceInterval)
                {
                    accumulatedStuckMarkers++;
                }
                else
                {
                    if (accumulatedStuckMarkers > 1) UI.UIManager.Singleton.ShowDialogue("CarStuck", null, false);
                    accumulatedStuckMarkers = 0;
                }

                lastCheckPositionX = carTransform.position.x;
                
                if (accumulatedStuckMarkers > 5)
                {
                    UI.UIManager.Singleton.ShowDialogue("CarStuck", null, true);
                    accumulatedStuckMarkers = 0;
                }
            }

            await Task.Delay(checkIntervalMilliseconds);
        }
    }
}
 