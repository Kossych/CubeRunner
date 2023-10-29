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
    [Range(8, 12)]
    [SerializeField] private int _maxCubesCount;

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
        CubeAdditionEvent?.Invoke(_cubes[^1]);
    }

    private void SetCube(Cube cube)
    {
        cube.IsAttached = true;
        if(_cubes.Count == _maxCubesCount)
        {
            cube.Delete(0);
            return;
        }
        _cubes.Add(cube);

        cube.CubeCollisionEvent += AddCube;
        cube.WallCollisionEvent += RemoveCube;
    }

    public void RemoveCube(Cube cube)
    {
        cube.transform.SetParent(null);
        _cubes.Remove(cube);
        cube.Delete();

        cube.CubeCollisionEvent -= AddCube;
        cube.WallCollisionEvent -= RemoveCube;
        CubeRemovalEvent?.Invoke(cube);
    }

}
