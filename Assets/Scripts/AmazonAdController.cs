using UnityEngine;
using System.Collections;
using com.amazon.mas.cpt.ads;
using com.amazon.mas.cpt.ads.json;

/// <summary>
/// Controller for the Amazon Ads. Ads must be created and be loaded before playing.
/// Loaded ads expire after 10 minutes. Ads are created on start, loaded 3 seconds after creation and played
/// when "Play" buttons are pressed. AmazonAds Controller prefab must be in each scene with ads.
/// 
/// IF REMOVING AMAZON ADS:
/// Adjust Lines 27-30 of the StaticButtonControl script and
/// Lines 30-33 in MenuCtrl script
/// </summary>

public class AmazonAdController : MonoBehaviour {

    IAmazonMobileAds mobileAds = AmazonMobileAdsImpl.Instance;

    ShouldEnable noEnable = new ShouldEnable();
    ShouldEnable enable = new ShouldEnable();
    Ad createResponse;
    LoadingStarted loadResponse;
    bool isReady;
    bool adShown;
    long identifier;

    public static bool adTime = true;
    public static bool playAd = false;
    float loadTimer = 3.0f;
    bool loadAd = true;

    // Use this for initialization
    void Start()
    {
        noEnable.BooleanValue = false;
        enable.BooleanValue = true;

        //Set game ID
        SetApplicationKey();
        //Set loggig, testing and geo location
        mobileAds.EnableLogging(noEnable);
        mobileAds.EnableTesting(noEnable);
        mobileAds.EnableGeoLocation(noEnable);

        CreateAd();

        adTime = true;
        playAd = false;
        loadTimer = 3.0f;
        loadAd = true;

    }

    //Set App key to track ads
    void SetApplicationKey()
    {
        // Use Android app key for Android apps and iOS
        // app key for iOS apps
        string appKey;

#if UNITY_ANDROID
        appKey = "c8eebc7b1bcf40d38a2d364966bf5b63";
#elif UNITY_IOS
        appKey = "4JV7BQFQQ2";
#endif

        // Construct object passed to sync operation as input
        ApplicationKey key = new ApplicationKey();

        // Set input value
        key.StringValue = appKey;

        // Call method, passing in required input structure
        // This method does not return a response
        mobileAds.SetApplicationKey(key);
    }

    public void CreateAd()
    {
        Ad interstialAd = mobileAds.CreateInterstitialAd();
        // string adType = interstialAd.AdType.ToString();
        identifier = interstialAd.Identifier;

        // Debug.Log("The ad is being created");
        // Debug.Log("id: " + identifier + "  adType: " + adType);
    }

    public void LoadAd()
    {
        LoadingStarted response = mobileAds.LoadInterstitialAd();
        isReady = response.BooleanValue;
        // Debug.Log("The ad is loading");
    }

    public void ShowAd()
    {
        AdShown shownInterstitialAd = mobileAds.ShowInterstitialAd();
        adShown = shownInterstitialAd.BooleanValue;

        // Debug.Log("The ad is showing");
    }

    // Update is called once per frame
    void Update()
    {
        if (playAd || UnityAdsController.playAmazonAd)
        {
            adTime = false;
            ShowAd();
            CreateAd();
            loadTimer = 30.0f;
            playAd = false;
            UnityAdsController.playAmazonAd = false;

        }

        if (loadTimer > 0)
        {
            loadTimer -= 1 * Time.deltaTime;
        }
        if (loadTimer <= 0 && loadAd == true)
        {
            LoadAd();
            adTime = true;
            UnityAdsController.UnityAdTime = true;
            loadAd = false;
        }
        // Debug.Log("playAd: " + playAd);
    }
}
