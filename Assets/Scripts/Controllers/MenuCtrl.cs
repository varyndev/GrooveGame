using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class MenuCtrl : BaseSceneController {

		public float characterDemoInterval = 5.0f;

		private float timeToNextCharacterDemo;

		public override void GoToScene (int p_scene)
		{
			GameMaster.Instance.SceneFsm.ChangeState(CtrlStateGame.Instance);
		}

		public void QuitGame()
		{
			GameMaster.Instance.QuitGame();
		}

		void Start ()
		{
			timeToNextCharacterDemo = 0.0f;
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