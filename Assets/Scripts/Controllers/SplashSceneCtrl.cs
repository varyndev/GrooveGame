/*
 * Splash controller sets the initial finite state machine state for the game master
 * And controls the flow of the splash scene
 */
using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class SplashSceneCtrl : BaseSceneController {

		[SerializeField]
		private float m_timeToNextScene;
	
		[SerializeField]
		private float m_timeToControllable;

		[SerializeField]
		private int m_songToPlay; //The index to the song to play

		void Start()
		{
			GameMaster.Instance.SceneFsm.ChangeState(CtrlStateSplash.Instance);
		}

		public override void Init ()
		{
			AudioController.Instance.playAtIndex(m_songToPlay);
			StartCoroutine(ChangeState(m_timeToNextScene));
			StartCoroutine(CanControl(m_timeToControllable));
		}
		//Allows more control of the states
		public override void Run()
		{
			if(m_isControllable == true) {
				if(Input.anyKeyDown) {
					//Debug.LogError("Im pressing the dam key");
					GameMaster.Instance.SceneFsm.ChangeState(CtrlStateMenu.Instance);
				}
			}
		}

		public void Update()
		{
			if (m_timeToNextScene > 0.0f)
			{
				m_timeToNextScene -= 0.1f;
			}

			if (m_timeToNextScene <= 0.0f)
			{
				Application.LoadLevel("Menu");
			}
		}

		IEnumerator CanControl(float p_sec)
		{
			yield return new WaitForSeconds(p_sec);
			m_isControllable = true;
		}

		IEnumerator ChangeState(float p_sec)
		{
			yield return new WaitForSeconds(p_sec);

			// Disabling the intro video for now as we cannot get it to work on mobile devices.

			if (false && Player.Instance.IsFirstPlay ()) {
#if UNITY_IOS || UNITY_ANDROID
				GameMaster.Instance.SceneFsm.ChangeState (CtrlStateIntro.Instance);
//				GameMaster.Instance.SceneFsm.ChangeState (CtrlStateMenu.Instance);
#else
				GameMaster.Instance.SceneFsm.ChangeState (CtrlStateIntro.Instance);
#endif
			} else {
				GameMaster.Instance.SceneFsm.ChangeState (CtrlStateMenu.Instance);
			}
		}
	}
}