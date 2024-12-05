using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _homeButton;
    [SerializeField] Button _restartButton;

    private void Start()
    {
        OnButtonPressed(_resumeButton, ResumeButtonPressed);
        OnButtonPressed(_homeButton, HomeButtonPressed);
        OnButtonPressed(_restartButton, RestartButtonPressed);
    }

    public override void SetEnable()
    {
        base.SetEnable();

        _resumeButton.interactable = true;
        _homeButton.interactable = true;
        _restartButton.interactable = true;
    }

    private void ResumeButtonPressed()
    {
        _resumeButton.interactable = false;
        Time.timeScale = 1f;

        InputHandler.IsPaused = false;

        MenuManager.Instance.SwitchMenu(MenuType.Gameplay);
    }

    private void HomeButtonPressed()
    {
        _homeButton.interactable = false;
        Time.timeScale = 1f;

        InputHandler.IsPaused = false;

        StartCoroutine(ReloadLevelAsync(() =>
        {
            AdsManager.Instance.ShowInterstitial();

            MenuManager.Instance.SwitchMenu(MenuType.Main);
        }));
    }

    private void RestartButtonPressed()
    {
        _restartButton.interactable = false;
        Time.timeScale = 1f;

        InputHandler.IsPaused = false;

        StartCoroutine(ReloadLevelAsync(() =>
        {
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