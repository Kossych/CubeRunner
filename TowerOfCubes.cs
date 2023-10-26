using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TowerOfCubes : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player _player;
    [SerializeField] private Cube _initializeCube;
    [SerializeField] private Transform _cubeHolder;
    private readonly List<Cube> _cubes = new(8);

    [Header("Parameters")]
    [SerializeField] private float _speed;

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
    }

    public void FixedUpdate()
    {
        _player.Move(_speed);
        _cubes.ForEach(cube => cube.Move(_speed));
    }

    public void AddCube(Cube cube)
    {
        cube.transform.SetParent(_cubeHolder);
        cube.transform.position = _cubes[^1].transform.position + Vector3.up;
        SetCube(cube);
    }

    private void SetCube(Cube cube)
    {
        _cubes.Add(cube);
        cube.IsAttached = true;

        cube.WallCollisionEvent += RemoveCube;
        cube.CubeCollisionEvent += AddCube;
        CubeAdditionEvent?.Invoke(cube);
    }

    public void RemoveCube(Cube cube)
    {
        cube.transform.SetParent(null);
        _cubes.Remove(cube);

        cube.WallCollisionEvent -= RemoveCube;
        cube.CubeCollisionEvent -= AddCube;
        CubeRemovalEvent?.Invoke(cube);
    }

}
