using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class SceneManager : UnitySingletonPersistent<SceneManager> {

		[SerializeField]
		private string [] m_sceneNames;

		[SerializeField]
		private int m_sceneIndex;

		public void nextScene()
		{
			if( m_sceneIndex <  m_sceneNames.Length -1) {
				m_sceneIndex++;
			} else {
				m_sceneIndex = 0;
			}

			Application.LoadLevel(m_sceneNames[m_sceneIndex]);
		}

		public void loadScene(string p_string)
		{
			Application.LoadLevel(p_string);
		}
	}
}