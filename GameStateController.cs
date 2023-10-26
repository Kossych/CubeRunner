using UnityEngine;
using System;

public class GameStateController : MonoBehaviour
{
    private GameStates _currentGameState = GameStates.Start;
    private static GameStateController _instance;


    public static GameStateController Instance => _instance;
    public GameStates CurrentState
    {
        get => _currentGameState;
        private set
        {
            _currentGameState = value;
            GameStateChanged?.Invoke(_currentGameState);
            

        }
    }

    public event Action<GameStates> GameStateChanged;

}

public enum GameStates
{
    Start,
    Play,
    Pause,
    Fail,
}
