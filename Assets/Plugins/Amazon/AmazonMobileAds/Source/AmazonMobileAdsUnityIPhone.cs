
/* 
* Copyright 2014 Amazon.com,
* Inc. or its affiliates. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the
* "License"). You may not use this file except in compliance
* with the License. A copy of the License is located at
*
* http://aws.amazon.com/apache2.0/
*
* or in the "license" file accompanying this file. This file is
* distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
* CONDITIONS OF ANY KIND, either express or implied. See the
* License for the specific language governing permissions and
* limitations under the License.
*/



/* 
* Copyright 2014 Amazon.com,
* Inc. or its affiliates. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the
	* "License"). You may not use this file except in compliance
* with the License. A copy of the License is located at
*
* http://aws.amazon.com/apache2.0/
*
* or in the "license" file accompanying this file. This file is
* distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
* CONDITIONS OF ANY KIND, either express or implied. See the
* License for the specific language governing permissions and
	* limitations under the License.
	*/


	using System.Runtime.InteropServices;
#if UNITY_IPHONE
using UnityEngine;
#endif
namespace com.amazon.mas.cpt.ads
{
	public abstract partial class AmazonMobileAdsImpl
	{
		#if UNITY_IPHONE
		private class AmazonMobileAdsUnityIPhone : AmazonMobileAdsUnityBase
		{
			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeRegisterCallbackGameObject(string name);


			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeInit();

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeSetApplicationKeyJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeRegisterApplicationJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeEnableLoggingJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeEnableTestingJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeEnableGeoLocationJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeCreateFloatingBannerAdJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeCreateInterstitialAdJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeLoadAndShowFloatingBannerAdJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeLoadInterstitialAdJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeShowInterstitialAdJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeCloseFloatingBannerAdJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeIsInterstitialAdReadyJson(string jsonMessage);

			[DllImport ("AmazonMobileAdsBridge")]
			private static extern string nativeAreAdsEqualJson(string jsonMessage);

			public static new AmazonMobileAdsUnityIPhone Instance
			{
				get
				{
					return AmazonMobileAdsUnityBase.getInstance<AmazonMobileAdsUnityIPhone>();
				}
			}

			protected override void NativeInit()
			{
				//do nothing
			}

			protected override void RegisterCallback()
			{
				AmazonMobileAdsUnityIPhone.nativeRegisterCallbackGameObject(gameObject.name);
			}

			protected override void RegisterEventListener()
			{
				AmazonMobileAdsUnityIPhone.nativeRegisterCallbackGameObject(gameObject.name);
			}

			protected override void NativeRegisterCrossPlatformTool(string crossPlatformTool)
			{
				//AmazonMobileAdsUnityIPhone.nativeRegisterCrossPlatformTool(crossPlatformTool);
			}

			protected override string NativeSetApplicationKeyJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeSetApplicationKeyJson(jsonMessage);
			}

			protected override string NativeRegisterApplicationJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeRegisterApplicationJson(jsonMessage);
			}

			protected override string NativeEnableLoggingJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeEnableLoggingJson(jsonMessage);
			}

			protected override string NativeEnableTestingJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeEnableTestingJson(jsonMessage);
			}

			protected override string NativeEnableGeoLocationJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeEnableGeoLocationJson(jsonMessage);
			}

			protected override string NativeCreateFloatingBannerAdJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeCreateFloatingBannerAdJson(jsonMessage);
			}

			protected override string NativeCreateInterstitialAdJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeCreateInterstitialAdJson(jsonMessage);
			}

			protected override string NativeLoadAndShowFloatingBannerAdJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeLoadAndShowFloatingBannerAdJson(jsonMessage);
			}

			protected override string NativeLoadInterstitialAdJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeLoadInterstitialAdJson(jsonMessage);
			}

			protected override string NativeShowInterstitialAdJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeShowInterstitialAdJson(jsonMessage);
			}

			protected override string NativeCloseFloatingBannerAdJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeCloseFloatingBannerAdJson(jsonMessage);
			}

			protected override string NativeIsInterstitialAdReadyJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeIsInterstitialAdReadyJson(jsonMessage);
			}

			protected override string NativeAreAdsEqualJson(string jsonMessage)
			{
				return AmazonMobileAdsUnityIPhone.nativeAreAdsEqualJson(jsonMessage);
			}

		}
		#endif
	}
}

