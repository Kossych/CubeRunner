using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[DefaultExecutionOrder(-1)]
public class GameStateController: MonoBehaviour
{
    [SerializeField] private GameStates _currentGameState = GameStates.None;
    private static GameStateController _instance = null;
    public static GameStateController Instance => _instance;
    public event Action<GameStates> GameStateChangedEvent;
    public GameStates CurrentState
    {
        get => _currentGameState;
        set
        {
            if(_currentGameState == value) return;
            _currentGameState = value;
            GameStateChangedEvent?.Invoke(_currentGameState);
        }
    }

    public void Awake()
    {
        if(_instance == null) _instance = this;
    }

    public void Start()
    {
        Debug.Log("2");
        CurrentState = GameStates.Start;
    }
}

public enum GameStates
{
    Start,
    Play,
    Pause,
    Fail,
    None
}
