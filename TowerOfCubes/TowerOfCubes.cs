using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerOfCubes : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player _player;
    [SerializeField] private Cube _initializeCube;
    [SerializeField] private Transform _cubeHolder;
    [SerializeField] private ParticleSystem _cubeStackEffect;
    [SerializeField] private ParticleSystem _warpEffect;
    [SerializeField] InputController _input;
    private readonly List<Cube> _cubes = new(8);
    private bool _isActive = false;

    [Header("Parameters")]
    [Range(5, 10)]
    [SerializeField] private float _speed;
    [Range(1f, 2.5f)]
    [SerializeField] private float _levelFieldX;

    public event Action<Cube> CubeAdditionEvent;
    public event Action<Cube> CubeRemovalEvent;

    public void Awake()
    {
        if(_player == null) _player = GetComponentInChildren<Player>();
        if(_initializeCube == null) _initializeCube = GetComponentInChildren<Cube>();
        if(_cubeStackEffect == null) _cubeStackEffect = GetComponentInChildren<ParticleSystem>();
        if(_input == null) _input = GetComponent<InputController>();
    }

    public void Start()
    {
        SetCube(_initializeCube);
        _player.SetToCube(_initializeCube);
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

    public void AddCube(Cube cube)
    {
        cube.transform.SetParent(_cubeHolder);
        cube.transform.position = _cubes[^1].transform.position + Vector3.up; 

        SetCube(cube);
        _cubeStackEffect.Play();
        CubeAdditionEvent?.Invoke(cube);
    }

    private void SetCube(Cube cube)
    {
        _cubes.Add(cube);
        cube.IsAttached = true;

        cube.CubeCollisionEvent += AddCube;
        cube.WallCollisionEvent += RemoveCube;
    }

    public void RemoveCube(Cube cube)
    {
        cube.transform.SetParent(null);
        _cubes.Remove(cube);

        cube.CubeCollisionEvent -= AddCube;
        cube.WallCollisionEvent -= RemoveCube;
        CubeRemovalEvent?.Invoke(cube);
    }

    public void GameStateChangedHandle(GameStates gameState)
    {
        if(gameState == GameStates.Play)
        {
            _isActive = true;
            _warpEffect.Play();
            return;
        }
        _isActive = false;
        _warpEffect.Pause();
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
