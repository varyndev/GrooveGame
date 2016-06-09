﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Soomla.Store
{
	
	public class InAppStore : MonoBehaviour
	{
		
		public static int LockedDancerID = 3;
		public static int ARRoomID;
		//public static int[] FourPackID = {5, 6, 7, 8};
		//public static int[] SixPackID = {5, 6, 7, 8, 9, 10};
		public static int[] FourPackID = {6, 7, 8, 9};
		public static int[] SixPackID = {6, 7, 8, 9, 10, 11};
		public static int CoinPackID;
		
		public GameObject	DancerButton;
		public GameObject	FourSongButton;
		public GameObject	SixSongButton;
		public GameObject	SongLock;
		public GameObject	ModelLock;
		
		
		
		
		void Start ()
		{
			StoreEvents.OnItemPurchased += OnItemPurchased;
			StoreEvents.OnMarketPurchase += onMarketPurchase;
			
			////For debugging in scene
			//Soomla.Store.StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
			//
			Soomla.Store.SoomlaStore.Initialize (new Soomla.Store.StoreAssets ());
			//Soomla.Store.SoomlaStore.StartIabServiceInBg ();
		}
		
		void Update ()
		{
			if (BoogieDownGames.GameMaster.Instance.CurrentModel == LockedDancerID) {
				DancerButton.SetActive (!BoogieDownGames.Player.Instance.IsCharacterUnlocked (LockedDancerID));
				ModelLock.SetActive (!BoogieDownGames.Player.Instance.IsCharacterUnlocked (LockedDancerID));
			} else {
				DancerButton.SetActive (false);
			}
			if (BoogieDownGames.GameMaster.Instance.CurrentSong >= FourPackID [0] &&
			    BoogieDownGames.GameMaster.Instance.CurrentSong <= FourPackID [3]) {
				FourSongButton.SetActive (!BoogieDownGames.Player.Instance.IsSongUnlocked (BoogieDownGames.GameMaster.Instance.CurrentSong));
				SongLock.SetActive (!BoogieDownGames.Player.Instance.IsSongUnlocked (BoogieDownGames.GameMaster.Instance.CurrentSong));
			} else {
				FourSongButton.SetActive (false);
			}
			if (BoogieDownGames.GameMaster.Instance.CurrentSong > FourPackID [3]) {
				SixSongButton.SetActive (!BoogieDownGames.Player.Instance.IsSongUnlocked (BoogieDownGames.GameMaster.Instance.CurrentSong));
				SongLock.SetActive (!BoogieDownGames.Player.Instance.IsSongUnlocked (BoogieDownGames.GameMaster.Instance.CurrentSong));
			} else {
				SixSongButton.SetActive (false);
			}
			
			//DancerButton.SetActive (!(BoogieDownGames.Player.Instance.IsCharacterUnlocked (BoogieDownGames.GameMaster.Instance.CurrentModel)));
			//SongButton.SetActive (!BoogieDownGames.Player.Instance.IsSongUnlocked (BoogieDownGames.GameMaster.Instance.CurrentSong));		
		}
		
		public void Destroy ()
		{
			//For debugging in scene
			//SoomlaStore.StopIabServiceInBg ();
		}
		
		public static void UnlockFourPack ()
		{
			for (int x = 0; x < 4; x++) {
				BoogieDownGames.Player.Instance.UnlockSong (FourPackID [x]);
			}
		}
		public static void UnlockSixPack ()
		{
			for (int x = 0; x < 6; x++) {
				BoogieDownGames.Player.Instance.UnlockSong (SixPackID [x]);
			}
		}
		
		public static void UnlockDancer ()
		{
			BoogieDownGames.Player.Instance.UnlockCharacter (LockedDancerID);
		}
		
		public static void UnlockARRoom ()
		{
			BoogieDownGames.Player.Instance.UnlockScene (ARRoomID);
		}
		
		public void OnItemPurchased (PurchasableVirtualItem pvi, string payload)
		{
			
			Debug.Log ("ItemPurchased" + payload);
			
			if (pvi.ItemId == StoreAssets.DANCER_PACK_PRODUCT_ID) {
				UnlockDancer ();
			} else if (pvi.ItemId == StoreAssets.FLOOR_PACK_PRODUCT_ID) {
				UnlockARRoom ();
			} else if (pvi.ItemId == StoreAssets.SONG_4PACK_PRODUCT_ID) {
				SongLock.SetActive (false);
				UnlockFourPack ();
			} else if (pvi.ItemId == StoreAssets.SONG_6PACK_PRODUCT_ID) {
				SongLock.SetActive (false);
				UnlockSixPack ();
			} else if (pvi.ItemId == StoreAssets.COIN_CURRENCY_ITEM_ID) {
				BoogieDownGames.Player.Instance.AddCoins (10000000);
			}
			
			BoogieDownGames.Player.Instance.Save ();
		}
		
		public void onMarketPurchase (PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra)
		{
			Debug.Log ("onMarketPurchase Item Id: " + pvi.ItemId);
			
			if (pvi.ItemId == StoreAssets.DANCER_PACK_PRODUCT_ID) {
				UnlockDancer ();
			} else if (pvi.ItemId == StoreAssets.FLOOR_PACK_PRODUCT_ID) {
				UnlockARRoom ();
			} else if (pvi.ItemId == StoreAssets.SONG_4PACK_PRODUCT_ID) {
				SongLock.SetActive (false);
				UnlockFourPack ();
			} else if (pvi.ItemId == StoreAssets.SONG_6PACK_PRODUCT_ID) {
				SongLock.SetActive (false);
				UnlockSixPack ();
			} else if (pvi.ItemId == StoreAssets.COIN_CURRENCY_ITEM_ID) {
				BoogieDownGames.Player.Instance.AddCoins (10000000);
			}
			
			BoogieDownGames.Player.Instance.Save ();
		}
		
		public void BuyDancer ()
		{
			StoreInventory.BuyItem (StoreAssets.DANCER_PACK_PRODUCT_ID);
			Debug.Log ("Dancer Bought");
		}
		
		public  void BuyARRoom ()
		{
			StoreInventory.BuyItem (StoreAssets.FLOOR_PACK_PRODUCT_ID);
		}
		public void BuyFourPack ()
		{
			StoreInventory.BuyItem (StoreAssets.SONG_4PACK_PRODUCT_ID);
			Debug.Log ("4Pack Bought");
		}
		
		public void BuySixPack ()
		{
			StoreInventory.BuyItem (StoreAssets.SONG_6PACK_PRODUCT_ID);
			Debug.Log ("6Pack Bought");
		}
		
		public void BuyCoinPack ()
		{
			StoreInventory.BuyItem (StoreAssets.COIN_CURRENCY_ITEM_ID);
		}
		
		
		public void BuySongPack ()
		{
			if (BoogieDownGames.GameMaster.Instance.CurrentSong >= FourPackID [0] &&
			    BoogieDownGames.GameMaster.Instance.CurrentSong <= FourPackID [3]) {
				BuyFourPack ();
			} else if (BoogieDownGames.GameMaster.Instance.CurrentSong > FourPackID [3]) {
				BuySixPack ();
			}
		}
		
		////For debugging in scene
		//public void onSoomlaStoreInitialized ()
		//{
		//	Debug.Log ("STORE INITIALIZED");
		//}
	}
}