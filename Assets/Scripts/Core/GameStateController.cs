using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls real-time game
/// </summary>
public static class GameStateController 
{
    public static bool GameInitted = false;

    public static OnGameStateChanges GameStateChangesEvent;

    private static GameState currentGameState = GameState.InMenu;
    public static GameState CurrentGameState
    {
        get { return currentGameState; }
        set
        {
            currentGameState = value;
            GameStateChangesEvent?.Invoke();
        }
    }

    public delegate void OnGameStateChanges();

    public enum GameState : byte
    {
        InMenu = 0,
        Live = 1,
        Paused = 2
    }
}
