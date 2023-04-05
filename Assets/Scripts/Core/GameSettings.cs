using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    private void Awake() => GameSettingsInit();
    public void GameSettingsInit()
    {
        Application.targetFrameRate = 60;
    }
}
