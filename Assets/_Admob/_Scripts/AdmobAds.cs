
using System;
using UnityEngine;

public class AdmobAds : MonoBehaviour
{
    [SerializeField] AdsDataSO _data;

 
    bool _isInitComplete = false;

    private void Start()
    {
   
    }

    private void OnDestroy()
    {
    }

    public bool IsRewardedAdLoaded()
    {
       
            return false;
    }
}
