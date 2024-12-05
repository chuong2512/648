using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverMenu : Menu
{
    [Header("Inherit References :")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;

    [Space]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _bestScoreText;
    [SerializeField] TMP_Text _gameModeText;

    private void Start()
    {
        OnButtonPressed(_homeButton, HomeButtonListener);
        OnButtonPressed(_restartButton, RestartButtonListener);
    }

    public override void SetEnable()
    {
        base.SetEnable();

        _homeButton.interactable = true;
        _restartButton.interactable = true;

        SetScoreDisplay();
    }

    private void SetScoreDisplay()
    {
        GameObject obj = GameObject.FindWithTag("GameController");
        GameManager gm = obj.GetComponent<GameManager>();
        if (gm == null) return;

        _scoreText.text = gm.GetScoreManager.GetScore.ToString();
        GameMode mode = GameManager.CurrentGameMode;

        switch (mode)
        {
            case GameMode.Classic:
                _gameModeText.text = "Classic Mode";
                _bestScoreText.text = SaveData.GetClassicBestScore().ToString();
                break;
            case GameMode.Fast:
                _gameModeText.text = "Fast Mode";
                _bestScoreText.text = SaveData.GetFastBestScore().ToString();
                break;
            case GameMode.Timer:
                _gameModeText.text = "Timer Mode";
                _bestScoreText.text = SaveData.GetTimerBestScore().ToString();
                break;

        }
    }

    private void HomeButtonListener()
    {
        _homeButton.interactable = false;

        StartCoroutine(ReloadLevelAsync(() =>
        {
            AdsManager.Instance.ShowInterstitial();

            MenuManager.Instance.SwitchMenu(MenuType.Main);
        }));
    }

    private void RestartButtonListener()
    {
        _restartButton.interactable = false;

        StartCoroutine(ReloadLevelAsync(() =>
        {
            AdsManager.Instance.ShowInterstitial();

            MenuManager.Instance.SwitchMenu(MenuType.Gameplay);

            GameObject obj = GameObject.FindWithTag("GameController");
            GameManager gm = obj.GetComponent<GameManager>();
            if (gm != null) gm.StartGame(GameManager.CurrentGameMode);
        }));
    }

    IEnumerator ReloadLevelAsync(Action OnSceneLoaded = null)
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        OnSceneLoaded?.Invoke();
    }
}
