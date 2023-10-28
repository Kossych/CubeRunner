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
    private readonly List<Cube> _cubes = new(8);

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
    }

    public void Start()
    {
        SetCube(_initializeCube);
        _player.SetToCube(_initializeCube);
    }

    public void AddCube(Cube cube)
    {
        cube.transform.SetParent(_cubeHolder);
        cube.transform.position = _cubes[^1].transform.position + Vector3.up; 

        SetCube(cube);
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

}
