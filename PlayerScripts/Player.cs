using UnityEngine;
using System;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [SerializeField] TowerOfCubes _towerOfCubes;
    [SerializeField] PlayerAnimator _animator;
    private Cube _parent;
    public event Action DieEvent;
    
    protected void Awake()
    {
        if(_towerOfCubes == null) _towerOfCubes = GetComponentInParent<TowerOfCubes>();
    }

    public void SetToCube(Cube cube)
    {
        transform.position = cube.transform.position + Vector3.up * 0.51f;
        //transform.SetParent(cube.transform);
        _parent = cube;
        _animator.IsJumping = true;
    }

    public void RemoveSetToCube(Cube cube)
    {
        if(_parent != cube) return;
        //transform.SetParent(_towerOfCubes.transform);
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _))
        {
            GameStateController.Instance.CurrentState = GameStates.Fail;
        }
    }

    public void OnEnable()
    {
        _towerOfCubes.CubeAdditionEvent += SetToCube;
        _towerOfCubes.CubeRemovalEvent += RemoveSetToCube;
        GameStateController.Instance.GameStateChangedEvent += GameStateChangedHandle;
    }

    public void OnDisable()
    {
        _towerOfCubes.CubeAdditionEvent -= SetToCube;
        _towerOfCubes.CubeRemovalEvent -= RemoveSetToCube;
        GameStateController.Instance.GameStateChangedEvent -= GameStateChangedHandle;
    }
}
