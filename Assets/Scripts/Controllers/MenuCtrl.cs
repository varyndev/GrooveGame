using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoogieDownGames {

	public class MenuCtrl : BaseSceneController {

		public float characterDemoInterval = 5.0f;
		public Text coinsText;
		public AudioClip m_badNote;

		private float timeToNextCharacterDemo;

		public override void GoToScene (int p_scene)
		{
			// Make sure all items are unlocked
			BaseGameController gameMaster = GameMaster.Instance;
			CharacterController characterController = (CharacterController) GameObject.FindObjectOfType(typeof(CharacterController));
			TextMachine textMachine = (TextMachine) GameObject.FindObjectOfType (typeof(TextMachine));
			SceneController sceneController = (SceneController) GameObject.FindObjectOfType (typeof(SceneController));

                if (sceneController.IsSceneLocked() || characterController.IsCharacterLocked() || textMachine.IsSongLocked(gameMaster.CurrentSong))
                {
                    GetComponent<AudioSource>().PlayOneShot(m_badNote);
                }
                else
                {
                  Player.Instance.SetLastPlayed(gameMaster.CurrentScene, gameMaster.CurrentModel, gameMaster.CurrentSong);
                  GameMaster.Instance.SceneFsm.ChangeState(CtrlStateGame.Instance);
                }
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