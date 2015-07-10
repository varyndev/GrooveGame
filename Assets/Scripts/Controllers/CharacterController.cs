using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BoogieDownGames {
	
	public class CharacterController : MonoBehaviour {
		
		public bool demoAnimation;
		public Image characterLockedIcon;

		// TODO: consider organizing this into an array of objects {characterModel, locked, characterName}

		[SerializeField]
		private GameObject [] m_models = null;

		[SerializeField]
		private List<bool> m_characterLocks; // these bools match the character id's to represent which characters should be locked
		
		[SerializeField]
		private RuntimeAnimatorController [] m_animationControllers;

		[SerializeField]
		private int m_currentIndex;
		
		private Animator m_anime;
		private bool m_triggerFired;

		// These are private for now as every dancer Animator Controller must support the exact same number, this is not configurable
		private const int basicAnimations = 4;
		private const int goodAnimations = 4;
		private const int bestAnimations = 4;
		private const int lameAnimations = 2;
		private const int cheerAnimations = 2;
		
		
		//testing timers
		float m_fCurrentAnimLength = 0.0f;
		float m_fNextAnimLength = 0.0f;
		float m_fCurrentAnimTimer = 0.0f;
		int m_nState = 0; //0 - Basic, 1- Good, 2- Best, 3- Lame, -1= Idle
		int m_nNextState = -1;
		
		void Start()
		{
			int i;
			for (i = 0; i < m_models.Length; i ++) {
				m_models[i].SetActive(false);
			}
			SetCurrentModel(GameMaster.Instance.CurrentModel);
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayStart");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayGood");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayBetter");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayBest");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayLame");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayCheer");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayWin");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayLose");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayIdle");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayRandom");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunEnter");
			m_triggerFired = false;
			if (demoAnimation) {
				ShowDemo (true);
			} else {
				PlayIdle ();
			}
			if (characterLockedIcon != null) {
				characterLockedIcon.enabled = IsCharacterLocked ();
			}
		}
		
		void Update()
		{
			if(Vector3.Magnitude(new Vector3(0,0,0) - m_models[m_currentIndex].transform.localPosition) > 0.22f)
				m_models[m_currentIndex].transform.localPosition = Vector3.Lerp(m_models[m_currentIndex].transform.localPosition, new Vector3(0,0,0), 5.0f * Time.deltaTime);
			if(m_fCurrentAnimLength > 0)
			{
				m_fCurrentAnimTimer += Time.deltaTime;
				if(m_fCurrentAnimTimer >= m_fCurrentAnimLength || m_nState == -1)
				{
					if(m_nNextState != -1)
					{
						m_nState = m_nNextState;
						//m_nNextState = -1;
					}
					if(m_fNextAnimLength > 0)
					{
						m_fCurrentAnimLength = m_fNextAnimLength;
						m_fNextAnimLength = 0.0f;
					}
					switch(m_nState)
					{
					case 0:
					{
						//Basic
						int basicTrigger = Random.Range (0, basicAnimations);
						string triggerId = "Basic" + basicTrigger.ToString ();
						SetAnimationTrigger (triggerId);
					}
						break;
					case 1:
					{
						//Good
						int goodTrigger = Random.Range (0, goodAnimations);
						string triggerId = "Good" + goodTrigger.ToString ();
						SetAnimationTrigger (triggerId);
					}
						break;
					case 2:
					{
						//Best
						int bestTrigger = Random.Range (0, bestAnimations);
						string triggerId = "Best" + bestTrigger.ToString ();
						SetAnimationTrigger (triggerId);
					}
						break;
					case 3:
					{
						//Lame
						int lameTrigger = Random.Range (0, lameAnimations);
						string triggerId = "Lame" + lameTrigger.ToString ();
						SetAnimationTrigger (triggerId);
					}
						break;
					}
				}
			}
		}
		
		public void OnStateRunExit()
		{
			m_anime.speed = 0f;
		}
		
		public void OnStateRunEnter()
		{
			m_anime.speed = 1f;
			SetAnimatorController ();
		}
		
		public void SetCurrentModel(int p_index)
		{
			for (int index = 0; index < m_models.Length; index ++) {
				m_models[index].SetActive(false);
			}
			m_models[p_index].SetActive(true);
			m_currentIndex = p_index;
			m_anime = m_models[m_currentIndex].GetComponent<Animator>();
			m_triggerFired = false;
		}
		
		public void NextModel()
		{
			//turn off current model
			m_models[m_currentIndex].SetActive(false);
			m_currentIndex ++;
			if (m_currentIndex >= m_models.Length) {
				m_currentIndex = 0;
			}
			ActivateCurrentModel ();
		}
		
		public void PrevModel()
		{
			//Turn off the current model
			m_models[m_currentIndex].SetActive(false);
			m_currentIndex --;
			if (m_currentIndex < 0) {
				m_currentIndex = m_models.Length - 1;
			}
			ActivateCurrentModel ();
		}

		private void ActivateCurrentModel()
		{
			m_models[m_currentIndex].SetActive(true);
			m_anime = m_models[m_currentIndex].GetComponent<Animator>();
			m_triggerFired = false;
			GameMaster.Instance.CurrentModel = m_currentIndex;
			characterLockedIcon.enabled = IsCharacterLocked ();
		}

		public bool IsCharacterLocked ()
		{
			bool isLocked = m_characterLocks [m_currentIndex];
			if (isLocked) {
				// TODO: now determine if the player has purchased the unlock for this item.
				isLocked = ! Player.Instance.IsCharacterUnlocked(m_currentIndex);
			}
			return isLocked;
		}
		
		public void PlayRandom () {
			
			// Play one animation at random
			
			int randomAnimation = Random.Range (1, 7);
			switch (randomAnimation) {
			case 1:
				PlayLame ();
				break;
			case 2:
				PlayGood ();
				break;
			case 3:
				PlayBetter ();
				break;
			case 4:
				PlayBest ();
				break;
			case 5:
				PlayWin ();
				break;
			case 6:
				PlayLose ();
				break;
			case 7:
				PlayCheer ();
				break;
			}
		}

		public void PlayStart ()
		{
			// Enter here when the song starts
			PlayIdle ();
		}

		public void PlayGood()
		{
			if( m_nState != 0 && m_nNextState != 0)
			{
				int basicTrigger = Random.Range (1, basicAnimations);
				string triggerId = "Basic" + basicTrigger.ToString ();
				IterateAnimation (triggerId);
				m_nNextState = 0;
			}
		}
		
		public void PlayBetter()
		{
			if(m_nState != 1 && m_nNextState != 1)
			{
				int goodTrigger = Random.Range (1, goodAnimations);
				string triggerId = "Good" + goodTrigger.ToString ();
				IterateAnimation (triggerId);
				m_nNextState = 1;
			}
		}
		
		public void PlayBest()
		{
			if(m_nState != 2 && m_nNextState != 2)
			{
				int bestTrigger = Random.Range (1, bestAnimations);
				string triggerId = "Best" + bestTrigger.ToString ();
				IterateAnimation (triggerId);
				m_nNextState = 2;
			}
		}
		
		public void PlayLame()
		{
			if(m_nState != 3 && m_nNextState != 3)
			{
				int lameTrigger = Random.Range (1, lameAnimations);
				//string triggerId = m_szLameAnims[lameTrigger];
				string triggerId = "Lame" + lameTrigger.ToString ();
				IterateAnimation (triggerId);
				m_nNextState = 3;
			}
		}
		
		public void PlayCheer()
		{
			int cheerTrigger = Random.Range (1, cheerAnimations);
			string triggerId = "Cheer" + cheerTrigger.ToString ();
			m_nNextState = -1;
			m_nState = -1;
			SetAnimationTrigger (triggerId);
		}
		
		public void PlayWin()
		{
			m_nNextState = -1;
			m_nState = -1;
			SetAnimationTrigger ("Win");
		}
		
		public void PlayLose()
		{
			m_nNextState = -1;
			m_nState = -1;
			SetAnimationTrigger ("Lose");
		}
		
		public void PlayIdle()
		{
			m_nState = -1;
			SetAnimationTrigger ("StandIdle");
		}
		
		public bool TriggerFired ()
		{
			return m_triggerFired;
		}

		public void ShowDemo (bool demoFlag)
		{
			// control the character demo mode. Demo mode is just a bool trigger on the animator. It is used to play a demo sequence of animations.
			if (m_anime != null) {
				m_anime.SetBool ("Demo", demoFlag);
			}
		}
		
		public void SetAnimatorController ()
		{
			// Set the animator controller based on the selected song
			if (m_anime != null && m_animationControllers != null && m_animationControllers.Length > 0) {
				int currentSongId = GameMaster.Instance.CurrentSong;
				if (currentSongId < 0) {
					currentSongId = 0;
				} else if (currentSongId > m_animationControllers.Length) {
					currentSongId = m_animationControllers.Length;
				}
				Debug.Log (">>>> Setting animation controller for song " + currentSongId.ToString () + " to " + m_animationControllers[currentSongId].ToString());
				m_anime.runtimeAnimatorController = m_animationControllers[currentSongId];
			}
		}

		private void SetAnimationTrigger (string animationTrigger)
		{
			if (m_anime != null) {
				if (animationTrigger == "") {
					animationTrigger = "StandIdle";
				}
				Debug.Log (">>>> Setting animation to " + animationTrigger);
				m_anime.SetTrigger (animationTrigger);
				m_triggerFired = true;
				m_fCurrentAnimTimer = 0.0f;
				m_fCurrentAnimLength = m_models[m_currentIndex].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
				m_models[m_currentIndex].transform.rotation = new Quaternion(0,0,0,0);
			}
		}

		private int GetTierID(string trigger)
		{
			//0 - Basic, 1- Good, 2- Best, 3- Lame, -1= Idle
			if(trigger.Contains("Basic"))
				return 0;
			else if(trigger.Contains("Good"))
				return 1;
			else if(trigger.Contains("Best"))
				return 2;
			else if(trigger.Contains("Lame"))
				return 3;
			else
				return -1;
		}

		private void IterateAnimation(string animationTrigger)
		{
			if (m_anime != null)
			{
				if (animationTrigger == "") 
				{
					animationTrigger = "StandIdle";
				}
				if (GetTierID(animationTrigger) != m_nState && GetTierID(animationTrigger) != m_nNextState) 
				{
					SetAnimationLength();
					m_models[m_currentIndex].transform.position = transform.position;
				}
			}
		}
		
		private void SetAnimationLength()
		{
			if(m_fCurrentAnimLength < 0.1f)
			{
				m_fCurrentAnimTimer = 0.0f;
				m_fCurrentAnimLength = m_models[m_currentIndex].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
			}
			else
			{
				m_fNextAnimLength = m_models[m_currentIndex].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
			}
		}
	}
}