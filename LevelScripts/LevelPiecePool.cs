using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelPiecePool : MonoBehaviour
{
    [SerializeField] private List<LevelPiece> _pool;

    private static LevelPiecePool _instance = null;

    public static LevelPiecePool Instance => _instance;

    public void Awake()
    {
        if(_instance == null) _instance = this;
        if(_pool.Count == 0) throw new Exception("LevelPieceGenerator hasnt a level piece");
    }

    public LevelPiece GetRandomLevelPiece()
    {
        return _pool[UnityEngine.Random.Range(0,_pool.Count)];
    }
    
}
