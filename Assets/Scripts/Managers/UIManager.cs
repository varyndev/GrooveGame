using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BoogieDownGames {

	public class UIManager : UnitySingleton<UIManager> {

		public GameObject m_gameOverCanvas;
		public Text gameOverScoreText;
		public Text gameOverCoinsText;

		[SerializeField]
		private List<GameObject> m_canvas;

		#region PROPERTIES

		public List<GameObject> Canvases
		{
			get { return m_canvas; }
		}

		#endregion

		public void SetAliveAtIndex(int p_index)
		{
			m_canvas[p_index].SetActive(true);
		}

		public void SetDeadAtIndex(int p_index)
		{
			m_canvas[p_index].SetActive(false);
		}

		public void SetDeadAllBut(int p_index)
		{
			for(int index = 0; index < m_canvas.Count; ++index) {
				m_canvas[index].SetActive(index == p_index);
			}
		}

		public void SetAliveAllBut(int p_index) {
			for(int index = 0; index < m_canvas.Count; ++index) {
				m_canvas[index].SetActive(index != p_index);
			}
		}

		public void SetAll(bool p_state)
		{
			foreach(GameObject obj in m_canvas) {
				obj.SetActive(p_state);
			}
		}

		public void ShowGameOver (bool showFlag)
		{
			if (showFlag) {
				DanceGameController danceGameContoller = DanceGameController.Instance;
				if (danceGameContoller != null) {
					if (gameOverScoreText != null) {
						gameOverScoreText.text = danceGameContoller.FinalScore.ToString();
					}
					if (gameOverCoinsText != null) {
						gameOverCoinsText.text = danceGameContoller.CoinsEarned.ToString();
					}
				}
			}
			if (m_gameOverCanvas != null) {
				m_gameOverCanvas.SetActive (showFlag);
			}
		}
	}
}