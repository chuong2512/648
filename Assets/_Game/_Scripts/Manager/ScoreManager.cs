using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Board _board;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _lineText;
    [SerializeField] TMP_Text _levelText;

    int _linesPerLevel = 4;
    int _score = 0;
    int _lines;
    int _level = 1;

    public int GetScore => _score;

    public static event Action<int> OnLevelUp;

    private void OnEnable()
    {
        _board.OnClearLines += UpdateScore;
    }

    private void Start()
    {
        _lines = _linesPerLevel * _level;

        UpdateUIText();
    }

    void UpdateUIText()
    {
        _scoreText.text = _score.ToString();
        _levelText.text = _level.ToString();
        _lineText.text = _lines.ToString();
    }

    void UpdateScore(int lineClear)
    {
        switch (lineClear)
        {
            case 1:
                _score += 20;
                break;
            case 2:
                _score += 50;
                break;
            case 3:
                _score += 100;
                break;
            case 4:
                _score += 200;
                break;
            default:
                break;
        }

        _lines -= lineClear;
        if(_lines <= 0)
        {
            LevelUp();
        }

        UpdateUIText();
    }

    private void LevelUp()
    {
        _level++;
        _lines = _linesPerLevel * _level;

        OnLevelUp?.Invoke(_level);
    }

    public void UpdateBestScore(GameMode mode)
    {
        int bestScore;

        switch (mode)
        {
            case GameMode.Classic:
                bestScore = SaveData.GetClassicBestScore();
                if (_score < bestScore) return;
                SaveData.SetClassicBestScore(_score);
                break;
            case GameMode.Fast:
                bestScore = SaveData.GetFastBestScore();
                if (_score < bestScore) return;
                SaveData.SetFastBestScore(_score);
                break;
            case GameMode.Timer:
                bestScore = SaveData.GetTimerBestScore();
                if (_score < bestScore) return;
                SaveData.SetTimerBestScore(_score);
                break;
        }
    }
}
