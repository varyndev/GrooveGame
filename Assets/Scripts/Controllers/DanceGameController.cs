using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

namespace BoogieDownGames {

	public class DanceGameController : UnitySingleton<DanceGameController> {

		public AudioClip m_goodNote;
		public AudioClip m_badNote;
		public int m_menuSceneNumber = 2;
		public int m_energyFillBonus = 250;
		public int m_lowScoreEarned = 10;
		public int m_midScoreEarned = 25;
		public int m_highScoreEarned = 50;

		[SerializeField]
		private TimeKeeper m_timer;

		[SerializeField]
		private GameObject m_cut;

		[SerializeField]
		private GameObject m_otherDancersParent; // other dancers in the scene must have a common parent
		
		[SerializeField]
		private int m_totalNotes;

		[SerializeField]
		private int m_missNotes; // The total number of notes missed

		[SerializeField]
		private int m_hitNotes; // The total number of notes hit

		[SerializeField]
		private int m_score; // Current game score
		
		[SerializeField]
		private int m_coins; // Current game coins earned
		
		[SerializeField]
		private Slider m_bonusNoteSlider;

		[SerializeField]
		private Text m_scoreText;
		
		private float noteSpawnTime;
		private float nextNoteSpawnTime;
		private bool runWasStartedSoDontDoItAgain = false;
		private AudioSource m_audioSource;

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
		public void Init()
		{
			m_timer.startClock();
		}

		void Start()
		{
			m_audioSource = GetComponent<AudioSource> ();
			runWasStartedSoDontDoItAgain = false;
			noteSpawnTime = 0.0f;
			m_totalNotes = 0;
			m_hitNotes = 0;
			m_missNotes = 0;
			m_score = 0;
			m_coins = 0;

			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitEnter");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitFixedUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateInitExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunEnter");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunFixedUpdate");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunExit");
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

		public void OnStateTutorialEnter()
		{
			Time.timeScale = 0f;
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
			if (! runWasStartedSoDontDoItAgain) {
				NotificationCenter.DefaultCenter.PostNotification (this, "ChangeStateToRun");
				runWasStartedSoDontDoItAgain = true;
				m_timer.m_trigger = true;
			}
		}
		
		public void OnStateInitFixedUpdate()
		{
		}
		
		public void OnStateInitExit()
		{
		}

		public void OnStateRunEnter()
		{
			Time.timeScale = 1.0f;
			noteSpawnTime = 2.0f; // TODO: note spawn time should vary based on difficulty and how well the player is doing
			AudioController.Instance.playAtIndex(GameMaster.Instance.CurrentSong);
			NotificationCenter.DefaultCenter.PostNotification (this, "PlayStart");
			BeginSceneDancers ("Good", 0);
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
			m_timer.run ();
			// Check to see if the song is finished
			if (AudioController.Instance.DetectEndOfSong () && ! AudioController.Instance.IsPaused) {
				Player.Instance.SetLastPlayCompleted (m_score, m_coins, GameMaster.Instance.CurrentScene, GameMaster.Instance.CurrentModel, GameMaster.Instance.CurrentSong);

				// Check for the number of number of notes to determine win or lose
				var missedPercantage = (m_hitNotes / m_totalNotes) * 100;
				Debug.LogError ("Missed percentage ==> " + missedPercantage.ToString ());
				if (missedPercantage < 50) {
					GameMaster.Instance.GameFsm.ChangeState (GameStateLostSong.Instance);
				} else {
					GameMaster.Instance.GameFsm.ChangeState (GameStateWonSong.Instance);
				}
			} else {
				// Check energy feedback slider
				if (m_bonusNoteSlider != null && m_bonusNoteSlider.value >= 1.0f) {
					EnergyMeterFilled();
				}
				if (Input.GetKeyDown (KeyCode.P)) {
					// AudioController.Instance.PauseSong();
				}
				nextNoteSpawnTime += Time.deltaTime;
				if (nextNoteSpawnTime >= noteSpawnTime) {
					SpawnNote ();
				}
			}
		}

		public void BeginSceneDancers (string danceLevel, int danceMove)
		{
			string trigger;
			// Begin animating all the other dancer models in the scene
			if (m_otherDancersParent != null) {
				foreach (Transform dancer in m_otherDancersParent.transform) {
					Animator dancerAC = dancer.gameObject.GetComponentInChildren<Animator>();
					if (dancerAC != null) {
						if (danceMove == -1) {
							trigger = danceLevel + UnityEngine.Random.Range (0, 4).ToString ();
						} else {
							trigger = danceLevel + danceMove.ToString ();
						}
						dancerAC.SetTrigger(trigger);
					}
				}
			}
		}

		public void OnStateCutSceneUpdate()
		{
		}

		public void OnStateCutSceneEnter()
		{
			m_cut.SetActive(true);
			AudioController.Instance.playAtIndex(7); // What is this???
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

		public void EnergyMeterFilled ()
		{
			// do fun stuff when the energy meter is filled: fireworks, lights, dance moves, then reset the meter
			m_coins ++;
			m_score += m_energyFillBonus;
			if (m_bonusNoteSlider != null) {
				m_bonusNoteSlider.value = 0.0f;
			}
			NotificationCenter.DefaultCenter.PostNotification (this, "spawnPrefab");
			// GameMaster.Instance.GameFsm.ChangeState (GameStateCutScene.Instance);
		}
		
		public void SpawnNote ()
		{
			nextNoteSpawnTime = 0.0f;
			NotificationCenter.DefaultCenter.PostNotification (this, "spawnPrefab");
			m_totalNotes ++;
		}

		public void NoteWasHit (NoteStates noteState)
		{
			float bonusEarned = 0.0f;
			int scoreEarned = 0;
			m_hitNotes ++;
			switch (noteState) {
			case NoteStates.LowScore:
				scoreEarned = m_lowScoreEarned;
				bonusEarned = 0.025f;
				break;
			case NoteStates.MidScore:
				scoreEarned = m_midScoreEarned;
				bonusEarned = 0.05f;
				break;
			case NoteStates.HighScore:
				scoreEarned = m_highScoreEarned;
				bonusEarned = 0.075f;
				break;
			default:
				break;
			}
			if (m_audioSource != null && m_goodNote != null) {
				m_audioSource.PlayOneShot (m_goodNote);
			}
			if (m_score < 50) {
				NotificationCenter.DefaultCenter.PostNotification (this, "PlayGood");
			}
			UpdatePlayerScore (scoreEarned);
			UpdateBonusMeter (bonusEarned);
			UpdateDanceMove ();
			PostMessage ("ReadEvent", "event", "Notes");
		}

		public void NoteWasMissed ()
		{
			string animationLevel;
			if (m_score < 30) {
				NotificationCenter.DefaultCenter.PostNotification (this, "PlayLame");
			}
			m_missNotes ++;
			// TODO: Add logic to change animation trigger based on how bad the player is scoring
			if (m_missNotes > 5) {
				animationLevel = "PlayLame";
			} else {
				animationLevel = "PlayGood";
			}
			if (m_audioSource != null && m_badNote != null) {
				m_audioSource.PlayOneShot (m_badNote);
			}
			NotificationCenter.DefaultCenter.PostNotification (this, animationLevel);
			float bonusEarned = -0.01f;
			UpdateBonusMeter (bonusEarned);
		}

		public void UpdatePlayerScore (int amount)
		{
			m_score += amount;
			if (m_score < 0) {
				m_score = 0;
			}
			if (m_scoreText != null) {
				m_scoreText.text = m_score.ToString();
			}
		}

		public void UpdateBonusMeter (float amount)
		{
			if (m_bonusNoteSlider != null && m_bonusNoteSlider.value < 1.0f) {
				float newValue = m_bonusNoteSlider.value + amount;
				if (newValue < 0.0f) {
					newValue = 0.0f;
				} else if (newValue > 1.0f) {
					newValue = 1.0f;
				}
				m_bonusNoteSlider.value = newValue;
			}
		}

		public void UpdateDanceMove ()
		{
			// TODO: Add logic to change animation trigger based on how well the player is scoring
			string animationLevel = "";
			float energyLevel = m_bonusNoteSlider.value;
			if (energyLevel < 0.1f) {
				if (m_missNotes < 5) {
					animationLevel = "PlayGood";
				} else {
					animationLevel = "PlayLame";
				}
			} else if (energyLevel > 0.25f) {
				animationLevel = "PlayBetter";
			} else if (energyLevel > 0.5f) {
				animationLevel = "PlayBest";
			} else {
				animationLevel = "PlayGood";
			}
			NotificationCenter.DefaultCenter.PostNotification (this, animationLevel);
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

		public void RestartLevel()
		{
			GameMaster.Instance.GoToScene(m_menuSceneNumber);
		}

		public void QuitToMenu()
		{
			GameMaster.Instance.GameFsm.ChangeState(GameStateIdle.Instance);
			GameMaster.Instance.SceneFsm.ChangeState(CtrlStateMenu.Instance);
		}
		
		public void PostMessage(string p_func, string p_message)
		{
			Hashtable dat = new Hashtable();
			dat.Add("eventName", p_message);
			NotificationCenter.DefaultCenter.PostNotification(this, p_func, dat);
		}
		
		public void PostMessage(string p_func, string p_key, string p_message)
		{
			Hashtable dat = new Hashtable();
			dat.Add(p_key,p_message);
			NotificationCenter.DefaultCenter.PostNotification(this, p_func, dat);
		}
	}
}