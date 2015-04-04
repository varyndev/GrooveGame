using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BoogieDownGames {

	public class UIManager : UnitySingleton<UIManager> {

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
				if(index != p_index) {
					Debug.Log("Setting ==> " + index.ToString());
					m_canvas[index].SetActive(false);
				} else {
					m_canvas[index].SetActive(true);
				}
			}
		}

		public void SetAliveAllBut(int p_index) {
			for(int index = 0; index < m_canvas.Count; ++index) {
				if(index != p_index) {
					m_canvas[index].SetActive(true);
				} else {
					m_canvas[index].SetActive(false);
				}
			}
		}

		public void SetAll(bool p_state)
		{
			foreach(GameObject obj in m_canvas) {
				obj.SetActive(p_state);
			}
		}
	}
}