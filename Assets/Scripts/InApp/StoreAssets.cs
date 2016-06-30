using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Soomla.Store
{
	
	
	public class StoreAssets : IStoreAssets
	{		
		public int GetVersion ()
		{
			return 1;
		}
		
		public VirtualCurrency[] GetCurrencies ()
		{
			return new VirtualCurrency[]{GGCOIN_CURRENCY};
		}
		
		public VirtualGood[] GetGoods ()
		{
			return new VirtualGood[] {FOURPACK_GOOD, SIXPACK_GOOD, DANCER_GOOD, FLOOR_GOOD};
        }
		
		public VirtualCurrencyPack[] GetCurrencyPacks ()
		{
			return new VirtualCurrencyPack[] {GGTEN_PACK};
		}
		
		public VirtualCategory[] GetCategories ()
		{
			return new VirtualCategory[]{GENERAL_CATEGORY};
		}
		
		/** Static Final Members **/
		public const string COIN_CURRENCY_ITEM_ID = "GGCoinPack";
		public const string FLOOR_PACK_PRODUCT_ID = "ARroomGrooveGame199";


#if UNITY_ANDROID || UNITY_WINRT_8_1
        //Activate if build is for ANDROID DEVICES ONLY
        public const string DANCER_PACK_PRODUCT_ID = "ariadancer099";
        public const string SONG_4PACK_PRODUCT_ID = "fourpacksongs249";
        public const string SONG_6PACK_PRODUCT_ID = "sixpacksongs299";
#endif
#if UNITY_IOS
        public const string DANCER_PACK_PRODUCT_ID = "Ariadancer099";
        public const string SONG_4PACK_PRODUCT_ID = "Fourpacksongs249";
        public const string SONG_6PACK_PRODUCT_ID = "Sixpacksongs299";
#endif

        /** Virtual Currencies **/

        public static VirtualCurrency GGCOIN_CURRENCY = new VirtualCurrency (
			"GGCoins",                               // Name
			"GG Coin currency",                      // Description
			"GGcoin_currency_ID"                    // Item ID
			);
		
		/** Virtual Currency Packs **/
		
		public static VirtualCurrencyPack GGTEN_PACK = new VirtualCurrencyPack (
			"100 Coins",                          // Name
			"100 coin currency units",            // Description
			"coins_100_ID",                       // Item ID
			10000000,                                  // Number of currencies in the pack
			"coin_currency_ID",                   // ID of the currency associated with this pack
			new PurchaseWithMarket (// Purchase type (with real money $)
		                        COIN_CURRENCY_ITEM_ID,                   // Product ID
		                        4.99                                   // Price (in real money $)
		                        )
			);
		
		/** Virtual Goods **/
		
		public static VirtualGood DANCER_GOOD = new LifetimeVG (
			"AriaDancer",                             // Name
			"Dancer Model",      // Description
			DANCER_PACK_PRODUCT_ID,                          // Item ID
			new PurchaseWithMarket (// Purchase type (with real money $)
		                        DANCER_PACK_PRODUCT_ID,                      // Product ID
		                        0.99                                   // Price (in real money $)
		                        )
			);
		
		public static VirtualGood FLOOR_GOOD = new LifetimeVG (
			"AR Room",                             // Name
			"Alt Dance Room",      // Description
			FLOOR_PACK_PRODUCT_ID,                          // Item ID
			new PurchaseWithMarket (// Purchase type (with real money $)
		                        FLOOR_PACK_PRODUCT_ID,                      // Product ID
		                        1.99                                   // Price (in real money $)
		                        )
			);
		
		public static VirtualGood FOURPACK_GOOD = new LifetimeVG (
			"4 Song Pack",                             // Name
			"Four New Songs",      						// Description
			SONG_4PACK_PRODUCT_ID,                         // Item ID
			new PurchaseWithMarket (// Purchase type (with real money $)
		                        SONG_4PACK_PRODUCT_ID,                      // Product ID
		                        2.49                                   // Price (in real money $)
		                        )
			);
		
		public static VirtualGood SIXPACK_GOOD = new LifetimeVG (
			"6 Song Pack",                              // Name
			"Six New Songs",      // Description
			SONG_6PACK_PRODUCT_ID,                          // Item ID
			new PurchaseWithMarket (// Purchase type (with real money $)
		                        SONG_6PACK_PRODUCT_ID,                      // Product ID
		                        2.99                                   // Price (in real money $)
		                        )
			);
		
		/** Virtual Categories **/
		// The muffin rush theme doesn't support categories, so we just put everything under a general category.
		public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory (
			"General", new List<string> (new string[] {
			COIN_CURRENCY_ITEM_ID,
			FLOOR_PACK_PRODUCT_ID,
			DANCER_PACK_PRODUCT_ID,
			SONG_4PACK_PRODUCT_ID,
			SONG_6PACK_PRODUCT_ID
		})
			);
		
	}
	
}