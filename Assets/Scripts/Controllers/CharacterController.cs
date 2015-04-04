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

		void Start()
		{
			SetCurrentModel(GameMaster.Instance.CurrentModel);
			NotificationCenter.DefaultCenter.AddObserver(this,"PlayAnime");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunExit");
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunEnter");
			m_anime.SetTrigger("StandIdle");
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
			for(int index = 0; index < m_models.Length; index++) {
				m_models[index].SetActive(false);
			}
			m_models[p_index].SetActive(true);
			m_currentIndex = p_index;
			m_anime = m_models[m_currentIndex].GetComponent<Animator>();
		}

		public void NextModel()
		{
			//turn off current model
			m_models[m_currentIndex].SetActive(false);
			m_currentIndex++;
			if (m_currentIndex >= m_models.Length) {
				m_currentIndex = 0;
			}
			m_models[m_currentIndex].SetActive(true);
			m_anime = m_models[m_currentIndex].GetComponent<Animator>();
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
			GameMaster.Instance.CurrentModel = m_currentIndex;
		}

		public void PlayAnime()
		{
			m_anime.SetTrigger("SatNightFever");
		}
	}
}