using UnityEngine;

[CreateAssetMenu(menuName = "Data SO/Admob Data", fileName = "Admob Data")]
public class AdsDataSO : ScriptableObject
{
    [SerializeField] int _interstitialAdInterval;
    [Tooltip("the minimum delay between interstitial ad in seconds.")]
    [SerializeField] float _minDelayBetweenInterstitial = 30;

    [Space]
    [SerializeField] [Range(0f, 1f)] float _rewardedAdFrequency = .5f;

    [Header("Admob Ad Units :")]
    // Test App Id = "ca-app-pub-3940256099942544~3347511713";
    [SerializeField] [TextArea(1, 2)] string idBanner = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] [TextArea(1, 2)] string idInterstitial = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] [TextArea(1, 2)] string idReward = "ca-app-pub-3940256099942544/5224354917";

    [Header("Enable Ads :")]
    [SerializeField] bool _enableBanner = true;
    [SerializeField] bool _enableInterstitial = true;
    [SerializeField] bool _enableRewarded = true;

    public int InterstitialAdInterval => _interstitialAdInterval;
    public float RewardedAdFrequency => _rewardedAdFrequency;

    public float MinDelayBetweenInterstitial => _minDelayBetweenInterstitial;

    public string BannerID => idBanner;
    public string InterstitialID => idInterstitial;
    public string RewardedID => idReward;

    public bool BannerEnabled => _enableBanner;
    public bool InterstitialEnabled => _enableInterstitial;
    public bool RewardedEnabled => _enableRewarded;
}
