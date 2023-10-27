using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private float _dx = 0;
    private Vector3 touchPosition;
    private int _screenWidth;
    [SerializeField] private bool _isActive;

    public float Dx => _dx;

    private void Awake()
    {
        _screenWidth = Screen.width;
    }

    private void FixedUpdate()
    {
        if(!_isActive) return;
        if(Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began)
        {
            GameStateController.Instance.CurrentState = GameStates.Play;
            touchPosition = touch.position;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            Vector3 newTouchPosition = touch.position;
            _dx = (touchPosition.x - newTouchPosition.x) / _screenWidth;
            Debug.Log( newTouchPosition.x / _screenWidth);
            touchPosition = newTouchPosition;
        }

        if(touch.phase == TouchPhase.Ended)
        {
            touchPosition = Vector3.zero;
            _dx = 0;
        }
    }

    public void GameStateChangedHandle(GameStates gameState)
    {
        if(gameState == GameStates.Fail)
        {
            _isActive = false;
            return;
        }
        _isActive = true;
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
