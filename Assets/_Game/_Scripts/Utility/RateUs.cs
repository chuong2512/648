using UnityEngine;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class RateUs : MonoBehaviour
{
    [SerializeField] int _countToRate = 3;

    int _playCount;

    static bool _rateOff = false;

    public void SetRateOff(bool value)
    {
        _rateOff = value;
    }

    public void ClickPlay()
    {
        _playCount++;
        if(_playCount % _countToRate == 0 && !_rateOff)
        {
#if UNITY_IOS
            Device.RequestStoreReview();
#else
            // open rate panel
            MenuManager.Instance.OpenMenu(MenuType.Rate);
#endif
        }
    }

    public void RateNow()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#else
        Application.OpenURL($"market://details?id={Application.identifier}");
#endif
    }
}
