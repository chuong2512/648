using System;
using UnityEngine;
using UnityEngine.UI;

public class RateMenu : Menu
{
    [Header("Inherit References :")]
    [SerializeField] private Button _laterButton;
    [SerializeField] private Button _rateButton;

    RateUs _rate;

    protected override void Awake()
    {
        base.Awake();

        _rate = GetComponent<RateUs>();
    }

    public override void SetEnable()
    {
        base.SetEnable();
        _laterButton.interactable = true;
        _rateButton.interactable = true;
    }

    private void Start()
    {
        OnButtonPressed(_laterButton, LaterButtonListener);
        OnButtonPressed(_rateButton, RateButtonListener);
    }

    private void LaterButtonListener()
    {
        _laterButton.interactable = false;
        MenuManager.Instance.CloseMenu();
    }

    private void RateButtonListener()
    {
        _rateButton.interactable = false;
        _rate.RateNow();
    }
}
