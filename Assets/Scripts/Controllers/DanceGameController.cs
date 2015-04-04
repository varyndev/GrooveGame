using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace BoogieDownGames {

	public class DanceGameController : UnitySingleton<DanceGameController> {

		[SerializeField]
		private TimeKeeper m_timer;

		[SerializeField]
		private GameObject m_cut;

		[SerializeField]
		private int m_totalNotes;

		[SerializeField]
		private int m_missNotes; //The total number of notes missed

		[SerializeField]
		private int m_hitNotes; //The total number of notes hit

		[SerializeField]
		private int m_originalSong;

		[SerializeField]
		private float m_rightPowerUp;

		[SerializeField]
		private float m_leftPowerUp;

		[SerializeField]
		private Slider m_1NoteSlider;

		[SerializeField]
		private Slider m_2NoteSlider;

		[SerializeField]
		private Slider m_3NoteSlider;

		[SerializeField]
		private Slider m_leftSlider;

		private float noteSpawnTime;
		private float nextNoteSpawnTime;

		public int MissNotes
		{
			get { return m_missNotes; }
			set { m_missNotes = value; }
		}

		public int HitNotes
		{
			get { return m_hitNotes; }
			set { m_hitNotes = value; }
		}

		public int TotalNotes
		{
			get { return m_totalNotes; }
			set { m_totalNotes = value;}
		}

		//Initiate the phase
		public  void Init()
		{
			m_timer.startClock();
			noteSpawnTime = 0.0f;
		}

		public  void RunStart()
		{
			if (m_timer.Counter > 0) {
				m_timer.run();
			} else {
				NotificationCenter.DefaultCenter.PostNotification(this, "ChangeStateToRun");
				m_timer.m_trigger = true;
			}
		}

		void Start()
		{
			NotificationCenter.DefaultCenter.AddObserver(this, "BoogieDownMessage");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitEnter");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitFixedUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunEnter");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunFixedUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "NoteWasHit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateCutSceneEnter");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateCutSceneUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateCutSceneFixedUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateCutSceneExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateTutorialEnter");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateTutorialExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateTutorialUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateTutorialFixedUpdate");

			GameMaster.Instance.GameFsm.ChangeState(GameStateInit.Instance);
		}

		/*
		 * Functions used with messaging
		 */

		public void OnStateTutorialEnter()
		{
			Time.timeScale = 0f;
			//AudioController.Instance.PauseSong();
		}

		public void OnStatePauseEnter()
		{
			AudioController.Instance.PauseSong();
		}

		public void OnStateInitEnter()
		{
			this.Init();
		}
		
		public void OnStateInitUpdate()
		{

			this.RunStart();
		}
		
		public void OnStateInitFixedUpdate()
		{

		}
		
		public void OnStateInitExit()
		{

		}

		public void OnStateRunEnter()
		{
			Time.timeScale = 1f;
			noteSpawnTime = 0.0f;
			AudioController.Instance.playAtIndex(GameMaster.Instance.CurrentSong);
		}

		public void OnStateRunUpdate()
		{
			this.RunGame();
		}

		public void OnStateFixedUpdate()
		{

		}

		public void OnStateRunExit()
		{
			Debug.Log("exiting run");
			NotificationCenter.DefaultCenter.PostNotification(this,"PauseSong");
		}

		public void RunGame()
		{

			//Check to see if the song is finished
			if (AudioController.Instance.DetectEndOfSong () && ! AudioController.Instance.IsPaused) {

				//Check for the number of number of notes
				var missedPercantage = (m_hitNotes / m_totalNotes) * 100;
				Debug.LogError ("Missed percentage ==> " + missedPercantage.ToString ());
				if (missedPercantage < 50) {
					//change state to lost
					GameMaster.Instance.GameFsm.ChangeState (GameStateLostSong.Instance);
				} else {
					//Change state to won song state
					GameMaster.Instance.GameFsm.ChangeState (GameStateWonSong.Instance);
				}
			} else {
				if (m_1NoteSlider != null && m_1NoteSlider.value >= 1.0f) {
					//run the event needed for this
					m_leftSlider.value += 0.05f;
					//The event is done reset the gauge
					m_1NoteSlider.value = 0f;
					SoundController.Instance.playAtIndex (6);
				}
				if (m_2NoteSlider != null && m_2NoteSlider.value >= 1.0f) {
					//run the event needed for this
					m_leftSlider.value += 0.08f;
					//The event is done reset the gauge
					m_2NoteSlider.value = 0f;
					SoundController.Instance.playAtIndex (6);
				}
				if (m_3NoteSlider != null && m_3NoteSlider.value >= 1.0f) {
					//run the event needed for this
					m_leftSlider.value += 0.1f;
					//The event is done reset the gauge
					m_3NoteSlider.value = 0f;
					SoundController.Instance.playAtIndex (6);
				}
				if (Input.GetKeyDown (KeyCode.P)) {
					//AudioController.Instance.PauseSong();
					GameMaster.Instance.GameFsm.ChangeState (GameStateCutScene.Instance);
				}
				if (m_leftSlider != null && m_leftSlider.value >= 1.0f) {
					m_leftSlider.value = 0f;
					//Create prefab bonus object
					NotificationCenter.DefaultCenter.PostNotification (this, "spawnPrefab");
					SoundController.Instance.playAtIndex (6);
					GameMaster.Instance.GameFsm.ChangeState (GameStateCutScene.Instance);
				}
				nextNoteSpawnTime += Time.deltaTime;
				if (nextNoteSpawnTime >= noteSpawnTime) {
					nextNoteSpawnTime = 0.0f;
					noteSpawnTime = 1.0f;
					NotificationCenter.DefaultCenter.PostNotification (this, "spawnPrefab");
				}
			}
		}

		public void OnStateCutSceneUpdate()
		{

		}

		public void OnStateCutSceneEnter()
		{
			m_cut.SetActive(true);
			SoundController.Instance.playAtIndex(7);
			//AudioController.Instance.PauseSong();
			StartCoroutine(CutSceneDelay(2.05f));
		}

		public void OnStateCutSceneExit()
		{
			//NotificationCenter.DefaultCenter.PostNotification(this,"PlayCurrentSong");
			m_cut.SetActive(false);
		}

		public void OnStateCutSceneFixedUpdate()
		{

		}

		IEnumerator CutSceneDelay(float p_sec)
		{
			yield return new WaitForSeconds(p_sec);
			m_cut.SetActive(false);
			GameMaster.Instance.GameFsm.ChangeState(GameStateRun.Instance);
		}

		public void NoteWasHit(NotificationCenter.Notification p_note)
		{
			string msg = (string)p_note.data["msg"];
			Debug.Log(msg);
			if(msg ==  "LowScore") {
				m_1NoteSlider.value += 0.05f;

				//send message to increase score
			} else if(msg ==  "MidScore") {
				m_2NoteSlider.value += 0.05f;
				//Send message to increase score
			} else if(msg ==  "HighScore") {
				m_3NoteSlider.value += 0.05f;
			}
		}

		public void InitLostSongSequence()
		{
			Debug.Log("entering Game state lost song");
			UIManager.Instance.SetDeadAllBut(3);
		}

		public void RunLostSongSequence()
		{
			Debug.Log("Game state lost song");
		}

		public void InitLostSequence()
		{

		}

		public void RunLostSequence()
		{

		}

		public void InitWonSequence()
		{

		}

		public void RunWonSequence()
		{

		}

		public void InitWonSongSequence()
		{

		}

		public void RunWonSongSequence()
		{

		}

		public void Pause()
		{
			GameMaster.Instance.Pause();
		}

		public void UnPause()
		{
			GameMaster.Instance.UnPause();
		}

		//Waits on a message then works on it
		public void BoogieDownMessage(NotificationCenter.Notification p_notes)
		{
			//Get the message
			string msg = (string)p_notes.data["msg"];
			if(!string.IsNullOrEmpty(msg)) {

				if(msg == "Missed Note") {
					m_missNotes++;
				} else if(msg == "Hit Note") {
					m_hitNotes++;
				} else {
					m_totalNotes++;
				}
			}
		}

		public void RestartLevel()
		{
			GameMaster.Instance.GoToScene(2);
		}

		public void QuitToMenu()
		{
			GameMaster.Instance.GameFsm.ChangeState(GameStateIdle.Instance);
			GameMaster.Instance.SceneFsm.ChangeState(CtrlStateMenu.Instance);
		}
	}
}