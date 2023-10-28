using System;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    [SerializeField] InputController _input;
    [SerializeField] LevelPieceGenerator _levelPieceGenerator;
    private bool _isActive = false;

    [Header("Parameters")]
    [Range(5, 10)]
    [SerializeField] private float _speed;
    [Range(20, 30)]
    [SerializeField] private float _maxSpeed;
    [Range(0.3f, 1f)]
    [SerializeField] private float _increaseSpeedValue;
    [Range(1f, 2.5f)]
    [SerializeField] private float _levelFieldX;

    public void Awake()
    {
        if(_input == null) _input = GetComponent<InputController>();
        if(_levelPieceGenerator == null) enabled = false;
    }

    public void FixedUpdate()
    {
        if(!_isActive) return;
        Move(_speed, _input.Dx);
    }

    public void Move(float speed, float sidewaysSpeed)
    {
        Vector3 offset = Vector3.forward * (speed * Time.fixedDeltaTime);
        offset += Vector3.left * (sidewaysSpeed * _levelFieldX * speed * Time.fixedDeltaTime);
        offset.x = MathF.Abs(transform.position.x + offset.x) < _levelFieldX ? offset.x : 0;
        transform.position += offset;
    } 

    public void IncreaseSpeed()
    {
        if(_speed < _maxSpeed)
        {
            _speed += _increaseSpeedValue;
        }
    }

    public void GameStateChangedHandle(GameStates gameState)
    {
        if(gameState == GameStates.Play)
        {
            _isActive = true;
            return;
        }
        _isActive = false;
    } 

    public void OnEnable()
    {
        GameStateController.Instance.GameStateChangedEvent += GameStateChangedHandle;
        _levelPieceGenerator.SpawnLevelPieceEvent += IncreaseSpeed;
    }

    public void OnDisable()
    {
        GameStateController.Instance.GameStateChangedEvent -= GameStateChangedHandle;
        _levelPieceGenerator.SpawnLevelPieceEvent -= IncreaseSpeed;
    }
}
