using UnityEngine;
using System.Collections;

// This class abstracts sending in-app beacons related to purchase and game play events for data analysis.
// TODO: Map currency to app store selection. will require a call back from teh app store.
// TODO: Map price from app store call back, we do not want to hard-code prices here


namespace BoogieDownGames {

	public class GameEventBeaconController : UnitySingletonPersistent<GameEventBeaconController> {

		public static string priceCurrency = "USD";

		void Start () {
			Fiksu.SetAppTrackingEnabled (true);
		}

		public static void FirstLaunchApp () {
			// call this only the first time this user has launched the app.
			Fiksu.UploadRegistration(Fiksu.FiksuRegistrationEvent.EVENT1);
		}

		public static void DanceModelPurchase (string modelId) {
			// call when a dancer is purchased, provide the id of which one was purchased

			double price = 0.99f;
			Fiksu.UploadPurchase(Fiksu.FiksuPurchaseEvent.EVENT2, price, priceCurrency);
		}

		public static void RoomPurchase (string roomId) {
			// call when a room scene is purchased

			double price = 1.99f;
			Fiksu.UploadPurchase(Fiksu.FiksuPurchaseEvent.EVENT1, price, priceCurrency);
		}

		public static void SongPurchase (string songId) {
			// call when a song is purchased

			double price;
			// price is 2.49 or 2.99
			if (songId == "PACK1") {
				price = 2.49f;
			} else {
				price = 2.99f;
			}
			Fiksu.UploadPurchase(Fiksu.FiksuPurchaseEvent.EVENT3, price, priceCurrency);
		}

		public static void GameStart (string roomId, string modelId, string songId) {
			// call at beginning of game indicating selections
			Fiksu.UploadCustomEvent();
		}
		
		public static void GameEnd (float duration, int score, bool win, int coinTotal) {
			// call when a song is completed with how long played, final score, won or lost game, how many coins finished with

			double price = (double)duration;
			Fiksu.UploadPurchase(Fiksu.FiksuPurchaseEvent.EVENT5, price, priceCurrency);
		}
	}
}