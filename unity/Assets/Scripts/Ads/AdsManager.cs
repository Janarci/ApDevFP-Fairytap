using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager _AdsManager = null;
    public EventHandler<AdFinishEventArgs> onAdDone;
    public bool adEnabled = true;
    public bool internet = false;
    public string GameID
    {
        get {

            return "4291057";
        
        }

    }
    private void Awake()
    {
        if (_AdsManager == null)
        {
            _AdsManager = this;
        }
        else if (_AdsManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        
        
        //true for testing false for publish
        Advertisement.Initialize(GameID, true);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            adEnabled = false;
            internet = false;
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            adEnabled = true;
            internet = true;
            Debug.Log("There's Internet!");
        }
    }


    public const string SampleRewarded = "Rewarded_Android";
    public const string SampleInterstitial = "Interstitial_Android";
    public const string SampleBanner = "Banner_Android";



	
	private void Start()
	{
        Advertisement.AddListener(this);
	}
	public void ShowInterstitialAd()
    {
        if (adEnabled)
        {
            if (Advertisement.IsReady(SampleInterstitial))
            {
                Advertisement.Show(SampleInterstitial);
            }
            else
            {
                Debug.Log("No Ads");
            }
        }
    }
    public void ShowRewardedAd()
    {
        if (adEnabled)
        {
            if (Advertisement.IsReady(SampleRewarded))
            {
                Advertisement.Show(SampleRewarded);
            }
            else
            {
                Debug.Log("No Ads");
            }
        }
        
    }
    public void ShowBanner()
    {
        if (adEnabled)
            StartCoroutine(ShowBannerRoutine());
    }

    public void HideBanner(bool value)
    {
        if(!value)
            Advertisement.Banner.Hide();
    }

    public void enableAds(bool value)
    {
        adEnabled = value;
    }

    IEnumerator ShowBannerRoutine()
    {
        while (!Advertisement.isInitialized)
        {

            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(SampleBanner);

    }

	public void OnUnityAdsReady(string placementId)
	{
        Debug.Log($"Loading done {placementId}");
	}

	public void OnUnityAdsDidError(string message)
	{
        Debug.Log($"Ad error: {message}");
    }

	public void OnUnityAdsDidStart(string placementId)
	{
        Debug.Log($"Ad shown: {placementId}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
        if (onAdDone != null)
        {
            AdFinishEventArgs args = new AdFinishEventArgs(placementId, showResult);
            onAdDone(this, args);
        }
	}
}
