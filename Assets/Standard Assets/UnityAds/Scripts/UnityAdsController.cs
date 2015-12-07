using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAdsController : MonoBehaviour {

    public static bool playAmazonAd = false;
    public static bool UnityAdTime = true;
    public static bool UnityPlayAd = false;

    // Use this for initialization
    void Start()
    {
        Advertisement.Initialize("92639", false);
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Show();

    }

   /* private static void PlayUnityVideoAd(Callback<bool> onVideoPlayed)
    {
        string adString = PlayerPrefs.GetString("UnityAds");
        if (Advertisement.IsReady())
        {
           Advertisement.Show(adString, new ShowOptions
           {
             resultCallback = result => {
             switch (result)
              {
                case (ShowResult.Finished):
                      onVideoPlayed(true);
                      break;
                case (ShowResult.Failed):
                      onVideoPlayed(false);
                      break;
                case (ShowResult.Skipped):
                      onVideoPlayed(false);
                      break;
               }
             }
           });
        }
        onVideoPlayed(false);
    }

    public delegate void Callback<T>(T value);

    void MyCodeWorkflow()
    {
        //Doing somehing...
        PlayUnityVideoAd(delegate (bool result)
        {
            if (result)
            {
                playAmazonAd = true;
            }
            else
            {
                //Debug.Log("The ad DID NOT play");
                playAmazonAd = true;
            }
            
        });
    }*/

    // Update is called once per frame
    void Update ()
    {
        if (UnityPlayAd)
        {
            StartCoroutine(ShowAdWhenReady());
           // MyCodeWorkflow();
            UnityAdTime = false;
            UnityPlayAd = false;
            playAmazonAd = true;
        }

    }
}
