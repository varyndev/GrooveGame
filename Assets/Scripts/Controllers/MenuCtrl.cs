using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoogieDownGames {

	public class MenuCtrl : BaseSceneController {

		public float characterDemoInterval = 5.0f;
		public Text coinsText;

		private float timeToNextCharacterDemo;

		public override void GoToScene (int p_scene)
		{
			GameEventBeaconController.GameStart ((GameMaster.Instance.CurrentScene + 1).ToString (), (GameMaster.Instance.CurrentModel + 1).ToString (), (GameMaster.Instance.CurrentSong + 1).ToString ());
			GameMaster.Instance.SceneFsm.ChangeState(CtrlStateGame.Instance);
		}

		public void QuitGame()
		{
			GameMaster.Instance.QuitGame();
		}

		void Start ()
		{
			timeToNextCharacterDemo = 0.0f;
			coinsText.text = Player.Instance.coinsTotal.ToString ();
		}

		void Update () 
		{
			timeToNextCharacterDemo += Time.deltaTime;
			if (timeToNextCharacterDemo >= characterDemoInterval) {
				timeToNextCharacterDemo = 0.0f;
				NotificationCenter.DefaultCenter.PostNotification (this, "PlayGood");
			}
		}
	}
}