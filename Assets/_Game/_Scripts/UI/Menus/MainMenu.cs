using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Inherit References :")]
    [SerializeField] private Button _classicButton;
    [SerializeField] private Button _fastButton;
    [SerializeField] private Button _timerModeButton;
    [SerializeField] private Button _creditButton;
    [SerializeField] private Button _rateButton;
    [SerializeField] private Button _settingsButton;

    private void Start()
    {
        OnButtonPressed(_classicButton, ClassicButtonListener);
        OnButtonPressed(_fastButton, FastButtonListener);
        OnButtonPressed(_timerModeButton, TimerModeButtonListener);
        OnButtonPressed(_creditButton, CreditButtonListener);
        OnButtonPressed(_settingsButton, SettingsButtonListener);
        OnButtonPressed(_rateButton, HandleRateButtonPressed);
    }

    private void SettingsButtonListener()
    {
        MenuManager.Instance.OpenMenu(MenuType.Setting);
    }

    private void HandleRateButtonPressed()
    {
        MenuManager.Instance.OpenMenu(MenuType.Rate);
    }

    private void CreditButtonListener()
    {
        MenuManager.Instance.OpenMenu(MenuType.Credit);
    }

    private void FastButtonListener()
    {
        MenuManager.Instance.SwitchMenu(MenuType.Gameplay);

        GameObject obj = GameObject.FindWithTag("GameController");
        GameManager gm = obj.GetComponent<GameManager>();
        if (gm != null) gm.StartGame(GameMode.Fast);
    }

    private void TimerModeButtonListener()
    {
        MenuManager.Instance.SwitchMenu(MenuType.Gameplay);

        GameObject obj = GameObject.FindWithTag("GameController");
        GameManager gm = obj.GetComponent<GameManager>();
        if (gm != null) gm.StartGame(GameMode.Timer);
    }

    private void ClassicButtonListener()
    {
        MenuManager.Instance.SwitchMenu(MenuType.Gameplay);

        GameObject obj = GameObject.FindWithTag("GameController");
        GameManager gm = obj.GetComponent<GameManager>();
        if (gm != null) gm.StartGame(GameMode.Classic);
    }
}
