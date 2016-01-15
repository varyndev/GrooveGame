using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FiksuInit : MonoBehaviour 
{
    /// <summary>
    /// The ID of your application in iTunesConnect. 
    /// 9-digit number as a string e.g. "000000000"
    /// REQUIRED FOR iOS
    /// </summary>
    public string ITunesApplicationID;

    /// <summary>
    /// Outputs diagnostic info to the log. 
    /// If iOS, pops up ViewController to show status of SDK 
    /// </summary>
    public bool DebugModeEnabled = false;
    
    /// <summary>
    /// An array of valid product identifiers recognizable by the App Store.
    /// The Fiksu SDK can determine the country of the user's App Store by
    /// querying these products, which allows the client to target specific
    /// App Store users.
    /// </summary>
    public string[] ProductIdentifiers;
    
    void Awake()
    {
        Fiksu.Initialize(new Dictionary<string, object>()
        {
            { Fiksu.ITunesApplicationIDKey, ITunesApplicationID },
            { Fiksu.DebugModeEnabledKey, DebugModeEnabled },
            { Fiksu.ProductIdentifiersKey, ProductIdentifiers }
        });

        Destroy(gameObject);
    }
}
