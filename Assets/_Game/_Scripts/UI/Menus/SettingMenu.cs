using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : Menu
{
    [Header("Inherit References :")]
    [SerializeField] private Button _adsButton;
    [SerializeField] private Button _policyButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _toggleMusicButton;
    [SerializeField] private Button _toggleSFXButton;

    [Header("Game Data :")]
    [SerializeField] private PolicyDataSO _data;

    [Header("Image References Toggle")]
    [SerializeField] Image _sfxImage;
    [SerializeField] Image _MusicImage;

    [Header("Icon Toggle")]
    [SerializeField] Sprite _iconTrue;
    [SerializeField] Sprite _iconFalse;

    private bool _sfxState;
    private bool _musicState;

    public override void SetEnable()
    {
        base.SetEnable();
        _adsButton.interactable = true;
        _policyButton.interactable = true;
        _closeButton.interactable = true;
    }

    private void Start()
    {
        OnButtonPressed(_adsButton, AdsButtonListener);
        OnButtonPressed(_policyButton, PolicyButtonListener);
        OnButtonPressed(_closeButton, CloseButtonListener);
        OnButtonPressed(_toggleMusicButton, ToggleMusicButtonListener);
        OnButtonPressed(_toggleSFXButton, ToggleSFXButtonListener);

        SetIconToggle();
    }

    private void SetIconToggle()
    {
        _musicState = PlayerPrefs.GetInt("musicState", 0) == 0;
        _sfxState = PlayerPrefs.GetInt("sfxState", 0) == 0;

        _MusicImage.sprite = _musicState ? _iconTrue : _iconFalse;
        _sfxImage.sprite = _sfxState ? _iconTrue : _iconFalse;
    }

    private void ToggleMusicButtonListener()
    {
        SoundController.Instance.ToggleMusic(ref _musicState);
        _MusicImage.sprite = _musicState ? _iconTrue : _iconFalse;
    }

    private void ToggleSFXButtonListener()
    {
        SoundController.Instance.ToggleFX(ref _sfxState);
        _sfxImage.sprite = _sfxState ? _iconTrue : _iconFalse;
    }

    private void CloseButtonListener()
    {
        _closeButton.interactable = false;
        MenuManager.Instance.CloseMenu();
    }

    private void AdsButtonListener()
    {
        _adsButton.interactable = false;

        PlayerPrefs.SetInt("npa", -1);

        SoundController.Instance.DestroyObject();
        AdsManager.Instance.DestroyObject();

        //load gdpr scene
        StartCoroutine(LoadGDPRAsyncScene());
    }

    IEnumerator LoadGDPRAsyncScene()
    {
        yield return SceneManager.LoadSceneAsync(0);
        MenuManager.Instance.DestroyObject();
    }

    private void PolicyButtonListener()
    {
        _policyButton.interactable = false;

        string policy = _data.PrivacyPolicyURL;
        string protocol = "https://";
        if (policy.Contains(protocol))
        {
            policy = policy.Replace(protocol, "");
        }
        Application.OpenURL($"https://{policy}");
    }
}
