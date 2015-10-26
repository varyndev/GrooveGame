using UnityEngine;
using System.Collections;
using com.amazon.mas.cpt.ads;
using com.amazon.mas.cpt.ads.json;

public class AmazonAdController : MonoBehaviour {

    IAmazonMobileAds mobileAds = AmazonMobileAdsImpl.Instance;
    ShouldEnable noEnable = new ShouldEnable();
    ShouldEnable enable = new ShouldEnable();
    Ad createResponse;
    LoadingStarted loadResponse;
    bool isReady;
    bool adShown;
    long identifier;

    //int for playing ads
    public static int playAd = 1;

    void Awake()
    {
        DontDestroyOnLoad(this);

        Ad interstialAd = mobileAds.CreateInterstitialAd();
        string adType = interstialAd.AdType.ToString();
        identifier = interstialAd.Identifier;
    }

    // Use this for initialization
    void Start ()
    {
        noEnable.BooleanValue = false;
        enable.BooleanValue = true;

        //Set game ID
        SetApplicationKey();
        //Set loggig, testing and geo location
        mobileAds.EnableLogging(noEnable);
        mobileAds.EnableTesting(noEnable);
        mobileAds.EnableGeoLocation(noEnable);
    }

    //Set App key to track ads
   void SetApplicationKey()
    {
        // Use Android app key for Android apps and iOS
        // app key for iOS apps
        string appKey;

        #if UNITY_ANDROID
        appKey = "c8eebc7b1bcf40d38a2d364966bf5b63";
        #elif UNITY_IPHONE
        appKey = "c8eebc7b1bcf40d38a2d364966bf5b63";
        #endif

        // Construct object passed to sync operation as input
        ApplicationKey key = new ApplicationKey();

        // Set input value
        key.StringValue = appKey;

        // Call method, passing in required input structure
        // This method does not return a response
        mobileAds.SetApplicationKey(key);
    }

    // Update is called once per frame
    void Update ()
    {
        if (playAd == 0)
        {
            Ad interstialAd = mobileAds.CreateInterstitialAd();
            string adType = interstialAd.AdType.ToString();
            identifier = interstialAd.Identifier;
            // Debug.Log("id: " + identifier + "  adType: " + adType);
        }

        if (playAd == 1)
        {
            IsReady responseReady = mobileAds.IsInterstitialAdReady();
            // Get return value
            isReady = responseReady.BooleanValue;
            //Debug.Log(loadingStarted);
        }

        if (playAd == 2)
        {
            if (isReady)
            {
                AdShown shownInterstitialAd = mobileAds.ShowInterstitialAd();
                adShown = shownInterstitialAd.BooleanValue;

                if (adShown)
                {
                    playAd = 0;
                }

                // Debug.Log("adShown: " + adShown);
            }
            else
            {
                playAd = 0;
            }
        }
    }
}
