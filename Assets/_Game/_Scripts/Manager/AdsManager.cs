using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager : Singleton<AdsManager>
{
    [SerializeField] int GDPRSceneIndex = -1;
    [SerializeField] int gameplaySceneIndex;

    [SerializeField] AdsDataSO _data;
    [SerializeField] AdmobAds _ads;

    bool _isInterstitialShouldShow = false;
    bool _isInterstitialTimerPassed = false;
    float _interstitialTimer;

    static int _gameplayCount;

    public static event Action OnRewardedAdWatchedComplete;
    public static void HandleRewardedAdWatchedComplete() => OnRewardedAdWatchedComplete?.Invoke();

    private void OnEnable() => SceneManager.sceneLoaded += HandleSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= HandleSceneLoaded;

    private void HandleSceneLoaded(Scene s, LoadSceneMode sm)
    {
        if (s.buildIndex == gameplaySceneIndex)
        {
            _gameplayCount++;
            int interval = Mathf.Clamp(_data.InterstitialAdInterval, 1, 100);

            if (_gameplayCount % interval == 0)
            {
                _isInterstitialShouldShow = true;
            }

            ShowBanner();
        }
        else if (s.buildIndex == GDPRSceneIndex)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _interstitialTimer = _data.MinDelayBetweenInterstitial;
    }

    private void Update()
    {
        if (!_isInterstitialTimerPassed && Time.time > _interstitialTimer)
        {
            _isInterstitialTimerPassed = true;
        }
    }

    public bool IsRewardedAdLoaded()
    {
        if (!_isInterstitialShouldShow && UnityEngine.Random.Range(0f, 1f) <= _data.RewardedAdFrequency)
        {
            return _ads.IsRewardedAdLoaded();
        }
        else
        {
            return false;
        }
    }

    public void ShowBanner()
    {
        // _ads.ShowBanner();
    }

    public void ShowInterstitial()
    {
        if (_isInterstitialTimerPassed && _isInterstitialShouldShow)
        {
            _isInterstitialShouldShow = false;

            _isInterstitialTimerPassed = false;
            _interstitialTimer = Time.time + _data.MinDelayBetweenInterstitial;

            // _ads.ShowInterstitialAd();
        }
    }

    public void ShowRewarded()
    {
        // _ads.ShowRewardedAd();
    }
}