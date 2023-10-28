using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreNumberText;
    [SerializeField] private TowerOfCubes _towerOfCubes;
    private readonly StringBuilder _scoreText = new("Score: ");
    private int _currentScore = 0;

    private void Awake()
    {
        if(_scoreText == null) _scoreNumberText = GetComponent<TextMeshProUGUI>();
        if(_towerOfCubes == null) enabled = false;
    }
    
    public void AddScore(Cube _)
    {
        _currentScore++;
        _scoreNumberText.text = _scoreText + _currentScore.ToString();
    }

    public void OnEnable()
    {
        _towerOfCubes.CubeAdditionEvent += AddScore;
    }

    public void OnDisable()
    {
        _towerOfCubes.CubeAdditionEvent -= AddScore;
    }
}
