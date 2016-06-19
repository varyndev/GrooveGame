using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public class GameMaster : BaseGameController {
		
		public static string VERSION = "1.1.2";
		public static string GAME_SKU = "GrooveGame";
		
		public override void Awake()
		{
			//Debug.Log ("Starting " + GAME_SKU + " Version " + VERSION);
			base.Awake();
		}
		public  void Start(){
            if (!Soomla.Store.SoomlaStore.Initialized){
                Soomla.Store.StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
                Soomla.Store.SoomlaStore.Initialize(new Soomla.Store.StoreAssets());
			    Soomla.Store.SoomlaStore.StartIabServiceInBg ();
            }
		}

		public override void Update () 
		{
			GameFsm.runOnUpdate();
			SceneFsm.runOnUpdate();
		}
		
		public override void FixedUpdate()
		{
			GameFsm.runOnFixedUpdate();
			SceneFsm.runOnFixedUpdate();
		}
		
		public override void Pause()
		{
			GameFsm.ChangeState(GameStatePause.Instance);
		}
		
		public override void UnPause()
		{
			base.UnPause();
			GameFsm.ChangeState(GameStateRun.Instance);
		}
		
		public override void QuitGame()
		{
			Soomla.Store.SoomlaStore.StopIabServiceInBg ();
			base.QuitGame ();
		}
		
		public void onSoomlaStoreInitialized()
		{
			Debug.Log("STORE INITIALIZED");

			if (Soomla.Store.StoreInventory.GetItemBalance(Soomla.Store.StoreAssets.DANCER_PACK_PRODUCT_ID) >= 1) 
			{
				Soomla.Store.InAppStore.UnlockDancer();
			}
			
			if (Soomla.Store.StoreInventory.GetItemBalance(Soomla.Store.StoreAssets.FLOOR_PACK_PRODUCT_ID) >= 1) 
			{
				Soomla.Store.InAppStore.UnlockARRoom();
			}
			
			if (Soomla.Store.StoreInventory.GetItemBalance(Soomla.Store.StoreAssets.SONG_4PACK_PRODUCT_ID) >= 1) 
			{
				Soomla.Store.InAppStore.UnlockFourPack ();
			}
			
			if (Soomla.Store.StoreInventory.GetItemBalance(Soomla.Store.StoreAssets.SONG_6PACK_PRODUCT_ID) >= 1) 
			{
				Soomla.Store.InAppStore.UnlockSixPack();
			}
			
		}
	}
}