using System;
using UnityEngine;

public enum GameMode
{
    Classic,
    Fast,
    Timer
}

public class GameManager : MonoBehaviour
{
    [SerializeField] Board _board;
    [SerializeField] Ghost _ghost;
    [SerializeField] TimerMode _timerMode;

    [Space]
    [SerializeField] GameModeSO _mode;

    float _currentStepDelay;
    bool _isGameEnd = false;

    InputHandler _input;
    ScoreManager _scoreManager;

    public ScoreManager GetScoreManager => _scoreManager;

    public static GameMode CurrentGameMode { get; private set; }

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        _scoreManager = GetComponent<ScoreManager>();
    }

    private void OnEnable()
    {
        _board.OnGameEnd += HandleGameEnd;
        ScoreManager.OnLevelUp += HandleLevelUp;
        _timerMode.OnTimerEnd += HandleGameEnd;
    }

    private void OnDisable()
    {
        _board.OnGameEnd -= HandleGameEnd;
        ScoreManager.OnLevelUp -= HandleLevelUp;
        _timerMode.OnTimerEnd -= HandleGameEnd;
    }

    private void HandleGameEnd()
    {
        if (_isGameEnd) return;
        _isGameEnd = true;

        _scoreManager.UpdateBestScore(CurrentGameMode);
        _board.GameEnd();
        MenuManager.Instance.SwitchMenu(MenuType.GameOver);

        if (CurrentGameMode == GameMode.Timer) _timerMode.Reset();

        SoundController.Instance.PlayAudio(AudioType.GAMEOVER);
    }

    public void StartGame(GameMode mode)
    {
        CurrentGameMode = mode;

        switch (CurrentGameMode)
        {
            case GameMode.Classic:
                _currentStepDelay = _mode.ClassicSpeedList[0];
                break;
            case GameMode.Fast:
                _currentStepDelay = _mode.FastSpeedList[0];
                break;
            case GameMode.Timer:
                _currentStepDelay = _mode.ClassicSpeedList[0];
                _timerMode.gameObject.SetActive(true);
                break;
        }

        _board.GetPiece.UpdateStepDelay(_currentStepDelay);
        _board.StartGame();
        _ghost.IsGameStart = true;
        _input.StartGame();
    }

    private void HandleLevelUp(int level)
    {
        switch (CurrentGameMode)
        {
            case GameMode.Classic:
                _currentStepDelay = _mode.ClassicSpeedList[level - 1];
                break;
            case GameMode.Fast:
                _currentStepDelay = _mode.FastSpeedList[level - 1];
                break;
            case GameMode.Timer:
                _currentStepDelay = _mode.ClassicSpeedList[level - 1];
                break;
        }

        _board.GetPiece.UpdateStepDelay(_currentStepDelay);
    }
}
