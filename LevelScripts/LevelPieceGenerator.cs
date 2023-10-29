using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class LevelPieceGenerator : MonoBehaviour
{
    [SerializeField] private Queue<LevelPiece> _levelPieces = new(4);
    [SerializeField] private LevelPiece _startedLevelPiece;
    [SerializeField] private TowerOfCubes _towerOfCubes;

    [Range(3, 8)]
    [SerializeField] private int _maxCountLevelPieces;
    private Vector3 lastPosition;

    public event Action SpawnLevelPieceEvent;

    public void Awake()
    {
        if(_startedLevelPiece == null) _startedLevelPiece = GetComponentInChildren<LevelPiece>();
    }

    public void Start()
    {
        lastPosition = _startedLevelPiece.transform.position - _startedLevelPiece.transform.localScale.z * Vector3.forward;
        _levelPieces.Enqueue(_startedLevelPiece);
        _startedLevelPiece.GenerateLevelPeaceEvent += GenerateLevelPiece;
        while(_levelPieces.Count < _maxCountLevelPieces)
        {
            GenerateLevelPiece();
        }
    }
    
    public void OnGenerateLevelPiece()
    {
        GenerateLevelPiece();
        
        LevelPiece lastLevel = _levelPieces.Dequeue();
        lastPosition = lastLevel.transform.position;
        Destroy(lastLevel.gameObject);
        SpawnLevelPieceEvent?.Invoke();
    }

    public void GenerateLevelPiece()
    {
        LevelPiece levelPiece = LevelPiecePool.Instance.GetRandomLevelPiece();
        LevelPiece newLevelPiece = Instantiate(levelPiece, lastPosition + ((_levelPieces.Count + 1) * levelPiece.transform.localScale.z * Vector3.forward), Quaternion.identity);
        newLevelPiece.GenerateLevelPeaceEvent += OnGenerateLevelPiece;
        _levelPieces.Enqueue(newLevelPiece);
    }

    

}
