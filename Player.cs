using UnityEngine;
using System;
using Unity.VisualScripting;

public class Player : Moveable
{
    [SerializeField] TowerOfCubes _towerOfCubes;
    [SerializeField] PlayerAnimator _animator;
    public event Action CollisionEvent;
    

    protected override void Awake()
    {
        base.Awake();
        if(_towerOfCubes == null) _towerOfCubes = GetComponentInParent<TowerOfCubes>();
    }

    public void SetToCube(Cube cube)
    {
        transform.position = cube.transform.position + Vector3.up * 0.5f;
        _animator.IsJumping = true;
    }

    public void Die()
    {

    }

    public void GameStateChangedHandle(GameStates gameState)
    {
        switch(gameState)
        {
            case GameStates.Start:
                break;
            case GameStates.Fail:
                Die();
                break;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube _))
        {
           return;
        }
        GameStateController.Instance.CurrentState = GameStates.Fail;
    }

    public void OnEnable()
    {
        _towerOfCubes.CubeAdditionEvent += SetToCube;
        GameStateController.Instance.GameStateChangedEvent += GameStateChangedHandle;
    }

    public void OnDisable()
    {
        _towerOfCubes.CubeAdditionEvent -= SetToCube;
        GameStateController.Instance.GameStateChangedEvent -= GameStateChangedHandle;
    }
}
