using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private Transform _startScreen;
    [SerializeField] private Transform _pauseScreen;
    [SerializeField] private Transform _failScreen;

    public void GameStateChangedHandle(GameStates gameState)
    {
        _startScreen.gameObject.SetActive(false);
        _pauseScreen.gameObject.SetActive(false);
        _failScreen.gameObject.SetActive(false);
        switch(gameState)
        {
            case GameStates.Start:
                _startScreen.gameObject.SetActive(true);
                break;
            case GameStates.Pause:
                _pauseScreen.gameObject.SetActive(true);
                break;
            case GameStates.Fail:
                _failScreen.gameObject.SetActive(true);
                break;
        }
    }

    public void OnEnable()
    {
        GameStateController.Instance.GameStateChangedEvent += GameStateChangedHandle;
    }

    public void OnDisable()
    {
        GameStateController.Instance.GameStateChangedEvent -= GameStateChangedHandle;
    }
}
