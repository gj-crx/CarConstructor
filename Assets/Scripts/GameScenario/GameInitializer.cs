using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{


    private void Awake() => GameInit();

    private void GameInit()
    {
        if (GameStateController.GameInitted) return;

        GameStateController.GameStateChangesEvent += GameScenarioController.LevelWinConditionCheckAsync;
        GameStateController.GameStateChangesEvent += GameScenarioController.CarStuckCheck;

        StartCoroutine(PostInitialization());
    }

    private IEnumerator PostInitialization()
    {
        yield return new WaitForSeconds(0.2f);

        GameStateController.GameInitted = true;
    }
}
