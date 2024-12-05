using System;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _pauseButton;

    private void Start()
    {
        OnButtonPressed(_pauseButton, PauseButtonPressed);
    }

    public override void SetEnable()
    {
        base.SetEnable();

        _pauseButton.interactable = true;
    }

    private void PauseButtonPressed()
    {
        _pauseButton.interactable = false;

        InputHandler.IsPaused = true;

        Time.timeScale = 0f;
        MenuManager.Instance.SwitchMenu(MenuType.Pause);
    }
}
