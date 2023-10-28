using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerOfCubes))]
public class ParticleController : MonoBehaviour
{
    [SerializeField] private TowerOfCubes _towerOfCubes;
    [SerializeField] private ParticleSystem _cubeStackEffect;
    [SerializeField] private ParticleSystem _warpEffect;

    private void Awake()
    {
        if(_towerOfCubes == null) _towerOfCubes = GetComponent<TowerOfCubes>();
    }

    public void EnableCubeStactEffect(Cube _)
    {
        _cubeStackEffect.Play();
    }

    public void GameStateChangedHandle(GameStates gameState)
    {
        if(gameState == GameStates.Play)
        {
            _warpEffect.Play();
            return;
        }
        _warpEffect.Pause();
    }

    public void OnEnable()
    {
        GameStateController.Instance.GameStateChangedEvent += GameStateChangedHandle;
        _towerOfCubes.CubeAdditionEvent += EnableCubeStactEffect;
    }

    public void OnDisable()
    {
        GameStateController.Instance.GameStateChangedEvent -= GameStateChangedHandle;
        _towerOfCubes.CubeAdditionEvent -= EnableCubeStactEffect;
    }
}
