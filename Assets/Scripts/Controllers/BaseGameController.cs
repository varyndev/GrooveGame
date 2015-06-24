using UnityEngine;
using System;
using System.Collections;

namespace BoogieDownGames {

	public class BaseGameController : UnitySingletonPersistent<BaseGameController> {

		private FiniteStateMachine<BaseGameController> m_sceneFSM = new FiniteStateMachine<BaseGameController>();
		private FiniteStateMachine<BaseGameController> m_gameFSM = new FiniteStateMachine<BaseGameController>();

		[SerializeField]
		private int m_currentSong;

		[SerializeField]
		private int m_currentModel;

		[SerializeField]
		private int m_currentScene;

		[SerializeField]
		private RuntimeAnimatorController m_defaultAnimatorController; // Use this if no other AC is set for the selected song

		[SerializeField]
		private RuntimeAnimatorController[] m_songAnimatorControllers; // This MUST MATCH the song or the default should be used.

		private bool m_songComplete;
		private bool m_songWon;
		private DateTime m_songStartTime;
		private DateTime m_songEndTime;

		#region PROPERTIES
		
		public int CurrentSong
		{
			get { return m_currentSong; }
			set { m_currentSong = value; }
		}

		public int CurrentModel
		{
			get { return m_currentModel; }
			set { m_currentModel = value; }
		}
		
		public int CurrentScene
		{
			get { return m_currentScene; }
			set { m_currentScene = value; }
		}

		public bool SongWon
		{
			get { return m_songWon; }
		}
		
		public bool SongComplete
		{
			get { return m_songComplete; }
		}

		#endregion

		#region PROPERTIES

		public FiniteStateMachine<BaseGameController> SceneFsm
		{
			get { return m_sceneFSM; }
		}

		public FiniteStateMachine<BaseGameController> GameFsm
		{
			get { return m_gameFSM; }
		}

		#endregion


		void OnLevelWasLoaded(int level)
		{
			if (level == 2) {
				NotificationCenter.DefaultCenter.AddObserver(this, "ChangeStateToRun");
			}
		}

		public override void Awake()
		{
			//Need to call this before start on any other object doess
			m_sceneFSM.Configure(this,CtrlStateIdle.Instance);
			m_gameFSM.Configure(this,GameStateIdle.Instance);
			NotificationCenter.DefaultCenter.AddObserver(this, "ChangeStateToRun");
		}
		
		public virtual void Update () 
		{
			m_sceneFSM.runOnUpdate();
			m_gameFSM.runOnUpdate();
		}

		public virtual void FixedUpdate()
		{
			m_sceneFSM.runOnFixedUpdate();
			m_gameFSM.runOnFixedUpdate();
		}

		public virtual void GoToScene(int p_scene)
		{
			Application.LoadLevel(p_scene);
		}

		public virtual void GoToScene(string p_scene)
		{
			Application.LoadLevel(p_scene);
		}
		
		public void SongStarted ()
		{
			m_songComplete = false;
			m_songStartTime = DateTime.Now;
			m_songEndTime = m_songStartTime;
			m_songWon = false;
		}

		public void SongCompleted (bool won)
		{
			m_songComplete = true;
			m_songEndTime = DateTime.Now;
			m_songWon = won;
		}

		public void PostMessage(string p_func, string p_message)
		{
			Hashtable dat = new Hashtable();
			dat.Add("msg",p_message);
			NotificationCenter.DefaultCenter.PostNotification(this,p_func,dat);
		}

		public virtual void QuitGame()
		{
			Application.Quit();
		}

		public virtual void ShowMenu ()
		{
			// this is called when the Menu button is tapped, change state back to the menu
			GameFsm.ChangeState(CtrlStateMenu.Instance);
		}
		
		public virtual void Pause()
		{
			GameFsm.ChangeState(GameStatePause.Instance);
		}

		public virtual void UnPause()
		{
			GameFsm.ChangeState(GameFsm.PreviousState);
		}

		public void ChangeStateToRun()
		{
			GameFsm.ChangeState(GameStateRun.Instance);
		}

		public void PostMessage(string p_message)
		{
			NotificationCenter.DefaultCenter.PostNotification(this,p_message);
		}
	}
}