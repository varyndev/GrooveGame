using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class CharacterController : MonoBehaviour {

		[SerializeField]
		private Animator m_anime;

		[SerializeField]
		private GameObject [] m_models;

		[SerializeField]
		private int m_currentIndex;

		private bool m_triggerFired;
		private string m_lastMove;

		void Start()
		{
			SetCurrentModel(GameMaster.Instance.CurrentModel);
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayStart");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayGood");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayBetter");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayBest");
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayLame");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunEnter");
			m_triggerFired = false;
			SetAnimationTrigger ("StandIdle");
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

		public void PlayGood()
		{
			int move = Random.Range (1, 4);
			switch (move) {
			case 1:
				SetAnimationTrigger ("Russian");
				break;
			case 2:
				SetAnimationTrigger ("SatNightFever");
				break;
			case 3:
			default:
				SetAnimationTrigger ("Running");
				break;
			}
		}
		
		public void PlayBetter()
		{
			int move = Random.Range (1, 4);
			switch (move) {
			case 1:
				SetAnimationTrigger ("Running");
				break;
			case 2:
				SetAnimationTrigger ("2000");
				break;
			case 3:
			default:
				SetAnimationTrigger ("SixStep");
				break;
			}
		}
		
		public void PlayBest()
		{
			int move = Random.Range (1, 4);
			switch (move) {
			case 1:
				SetAnimationTrigger ("Windmill");
				break;
			case 2:
				SetAnimationTrigger ("2000");
				break;
			case 3:
			default:
				SetAnimationTrigger ("SixStep");
				break;
			}
		}
		
		public void PlayLame()
		{
			SetAnimationTrigger ("CheerJump");
		}

		private void SetAnimationTrigger (string animationTrigger)
		{
			if (m_anime != null) {
				if (animationTrigger == "") {
					animationTrigger = "StandIdle";
				}
				if (animationTrigger != m_lastMove) {
					Debug.Log ("Setting animation to " + animationTrigger);
					m_lastMove = animationTrigger;
					m_anime.SetTrigger (animationTrigger);
					m_triggerFired = true;
				}
			}
		}
	}
}