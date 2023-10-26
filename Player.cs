using UnityEngine;
using System;
using Unity.VisualScripting;

public class Player : Moveable
{
    [SerializeField] TowerOfCubes _towerOfCubes;
    public event Action CollisionEvent;

    protected override void Awake()
    {
        base.Awake();
        if(_towerOfCubes == null) _towerOfCubes = GetComponentInParent<TowerOfCubes>();
    }

    public void SetToCube(Cube cube)
    {
        transform.position = cube.transform.position + Vector3.up * 0.5f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube _))
        {
           return;
        }
        CollisionEvent?.Invoke(); 
    }

    public void OnEnable()
    {
        _towerOfCubes.CubeAdditionEvent += SetToCube;
    }

    public void OnDisable()
    {
        _towerOfCubes.CubeAdditionEvent -= SetToCube;
    }
}
