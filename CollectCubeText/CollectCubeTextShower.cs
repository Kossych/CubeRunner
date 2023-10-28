using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TowerOfCubes))]
public class CollectCubeTextShower : MonoBehaviour
{
    [SerializeField] private TowerOfCubes _towerOfCubes;
    [SerializeField] private GameObject _collectCubeText;

    private void Awake()
    {
        if(_towerOfCubes == null)_towerOfCubes = GetComponent<TowerOfCubes>();
    }
    
    public void OnShowCollectCubeText(Cube cube)
    {
        Instantiate(_collectCubeText, cube.transform);
    }

    public void OnEnable()
    {
        _towerOfCubes.CubeAdditionEvent += OnShowCollectCubeText;
    }

    public void OnDisable()
    {
        _towerOfCubes.CubeAdditionEvent -= OnShowCollectCubeText;
    }
}
