using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoogieDownGames {
	
	public class CharacterController : MonoBehaviour {
		
		public bool demoAnimation;
		
		[SerializeField]
		private Animator m_anime;
		
		[SerializeField]
		private GameObject [] m_models = null;
		
		[SerializeField]
		private int m_currentIndex;
		
		private bool m_triggerFired;
		private string m_lastMove;
		
		// These are private for now as every dancer Animator Controller must support the exact smae number, this is not configurable
		private const int basicAnimations = 4;
		private const int goodAnimations = 4;
		private const int bestAnimations = 4;
		private const int lameAnimations = 2;
		private const int cheerAnimations = 2;
		
		
		//testing timers
		float m_fCurrentAnimLength = 0.0f;
		float m_fCurrentAnimTimer = 0.0f;
		int m_nState = 0; //0 - Basic, 1- Good, 2- Best, 3- Lame
		int m_nNextState = -1;
		
		void Start()
		{
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
			PlayIdle ();
			
			
		}
		
		void Update()
		{
			if(m_fCurrentAnimLength > 0)
			{
				m_fCurrentAnimTimer += Time.deltaTime;
				if(m_fCurrentAnimTimer >= m_fCurrentAnimLength)
				{
					if(m_nNextState != -1)
					{
						m_nState = m_nNextState;
						m_nNextState = -1;
					}
					m_fCurrentAnimTimer = 0.0f;
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
			m_models[m_currentIndex].SetActive(true);
			m_anime = m_models[m_currentIndex].GetComponent<Animator>();
			m_triggerFired = false;
			GameMaster.Instance.CurrentModel = m_currentIndex;
		}
		
		public void PrevModel()
		{
			//Turn off the current model
			m_models[m_currentIndex].SetActive(false);
			m_currentIndex--;
			if (m_currentIndex < 0) {
				m_currentIndex = m_models.Length-1;
			}
			m_models[m_currentIndex].SetActive(true);
			m_anime = m_models[m_currentIndex].GetComponent<Animator>();
			m_triggerFired = false;
			GameMaster.Instance.CurrentModel = m_currentIndex;
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
			int lameTrigger = Random.Range (1, lameAnimations);
			//string triggerId = m_szLameAnims[lameTrigger];
			string triggerId = "Lame" + lameTrigger.ToString ();
			IterateAnimation (triggerId);
			m_nNextState = 3;
		}
		
		public void PlayCheer()
		{
			int cheerTrigger = Random.Range (1, cheerAnimations);
			string triggerId = "Cheer" + cheerTrigger.ToString ();
			SetAnimationTrigger (triggerId);
		}
		
		public void PlayWin()
		{
			SetAnimationTrigger ("Win");
		}
		
		public void PlayLose()
		{
			SetAnimationTrigger ("Lose");
		}
		
		public void PlayIdle()
		{
			SetAnimationTrigger ("StandIdle");
		}
		
		public bool TriggerFired ()
		{
			return m_triggerFired;
		}
		
		private void SetAnimationTrigger (string animationTrigger)
		{
			if (m_anime != null) {
				if (animationTrigger == "") {
					animationTrigger = "StandIdle";
				}
				if (animationTrigger != m_lastMove) 
				{
					Debug.Log ("Setting animation to " + animationTrigger);
					m_lastMove = animationTrigger;
					m_anime.SetTrigger (animationTrigger);
					m_triggerFired = true;
					SetAnimationLength();
				}
			}
			
		}
		
		private void IterateAnimation(string animationTrigger)
		{
			if (m_anime != null) {
				if (animationTrigger == "") {
					animationTrigger = "StandIdle";
				}
				if (animationTrigger != m_lastMove) 
				{
					m_lastMove = animationTrigger;
					SetAnimationLength();
					m_models[m_currentIndex].transform.position = transform.position;
				}
			}
		}
		
		private void SetAnimationLength()
		{
			m_fCurrentAnimLength = m_models[m_currentIndex].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
			m_fCurrentAnimTimer = 0.0f;
		}
	}
}