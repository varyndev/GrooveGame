using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BoogieDownGames {
	
	public class SceneController : UnitySingleton<SceneController> {
		
		[SerializeField]
		private List<int> m_scenes; // these map to the scene id's in Build Settings to match each scene
		
		[SerializeField]
		private List<Sprite> m_sceneIcons; // these textures match the scene id's to represent the scene in the menu display
		
		[SerializeField]
		private List<string> m_sceneNames; // these strings match the scene id's to represent the scene in the menu display
		
		[SerializeField]
		private int m_initialScene; //Index of the initial sound
		
		[SerializeField]
		private int m_currentIndex;

		public Image sceneIcon;
		public Text sceneName;

		#region PROPERTIES
		
		public List<int> Scenes
		{
			get { return m_scenes; }
		}
		
		#endregion
		
		void Awake()
		{
			m_initialScene = 0;
			m_currentIndex = 0;
			SetCurrentScene ();
		}
		
		public void NextScene ()
		{
			m_currentIndex ++;
			if (m_currentIndex > m_scenes.Count -1) {
				m_currentIndex = 0;
			}
			SetCurrentScene ();
		}
		
		public void PrevScene ()
		{
			m_currentIndex --;
			if (m_currentIndex <= 0) {
				m_currentIndex = m_scenes.Count -1;
			}
			SetCurrentScene ();
		}

		public int GetCurrentSceneFromIndex (int sceneIndex) {
			if (sceneIndex > m_scenes.Count - 1) {
				sceneIndex = m_scenes.Count - 1;
			} else if (sceneIndex < 0) {
				sceneIndex = 0;
			}
			return m_scenes [sceneIndex];
		}

		private void SetCurrentScene () {
			sceneIcon.sprite = m_sceneIcons [m_currentIndex];
			sceneName.text = m_sceneNames [m_currentIndex];
			GameMaster.Instance.CurrentScene = m_currentIndex;
		}

		public void PostMessage(string method, string message)
		{
			Hashtable messageData = new Hashtable();
			messageData.Add("msg", message);
			NotificationCenter.DefaultCenter.PostNotification(this, method, messageData);
		}
	}
}