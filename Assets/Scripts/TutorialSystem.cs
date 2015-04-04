using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoogieDownGames {

	public class TutorialSystem : UnitySingleton<TutorialSystem> {

		[SerializeField]
		private Text m_message;

		[SerializeField]
		private GameObject m_panel;

		// Use this for initialization
		void Start () 
		{
			NotificationCenter.DefaultCenter.AddObserver(this,"DisplayTut");
		}

		public void DisplayTut(NotificationCenter.Notification p_not)
		{
			string msg = (string)p_not.data["msg0"];
			m_message.text = msg;
			GameMaster.Instance.GameFsm.ChangeState(GameStateTutorial.Instance);
			m_panel.SetActive(true);
		}

		public void CloseTut()
		{
			GameMaster.Instance.GameFsm.ChangeState(GameStateRun.Instance);
			m_panel.SetActive(false);
		}
	}
}