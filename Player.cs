using UnityEngine;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(PlayerAnimator))]
public class Player : Moveable
{
    [SerializeField] TowerOfCubes _towerOfCubes;
    [SerializeField] PlayerAnimator _animator;
    public event Action DieEvent;
    
    protected void Awake()
    {
        if(_towerOfCubes == null) _towerOfCubes = GetComponentInParent<TowerOfCubes>();
    }

    public void SetToCube(Cube cube)
    {
        transform.position = cube.transform.position + Vector3.up * 0.5f;
        transform.SetParent(cube.transform);
        _animator.IsJumping = true;
    }

    public void Die()
    {
        _animator.DisableAnimation();
        DieEvent?.Invoke();
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

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _))
        {
           GameStateController.Instance.CurrentState = GameStates.Fail;
        }
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
