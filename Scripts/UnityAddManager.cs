/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class UnityAddManager : MonoBehaviour
{
    public Text AdsText;
    private BannerView bannerView;
    private InterstitialAd interstitial;

    //string AppId = "ca-app-pub-6663009321135001~9095850101";
    string BannerId = "ca-app-pub-3940256099942544/6300978111";
    string InterstitialId = "ca-app-pub-3940256099942544/1033173712";
    // Start is called before the first frame update

    void Start()
    {
        RequestInterstitial();

        MobileAds.Initialize(initStatus => { });
        this.ShowBanner();
      
    }
    
    private void RequestBanner()
    {

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(BannerId, AdSize.Banner, AdPosition.Bottom);
        // Called when an ad request has successfully loaded.
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;


    }
    public void ShowBanner()
	{
        RequestBanner();
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);

    }

    private void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(InterstitialId);
        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void ShowInterstitial()
	{
         
       if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }

    }
    // Update is called once per frame
    void Update()
    {
        // this.RequestBanner();
       //this.ShowBanner();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        AdsText.text = "Add Loaded";
*//*       if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }*//*
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        AdsText.text = "Add Failed To Load Loaded";
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        AdsText.text = "Add Opened";
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        AdsText.text = "Add Closed";
    }

}
*/