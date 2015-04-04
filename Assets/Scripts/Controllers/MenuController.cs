using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class MenuController : UnitySingletonPersistent<MenuController> 
	{
		[SerializeField]
		private GameObject m_mainPanel;

		[SerializeField]
		private GameObject m_topMenu;

		[SerializeField]
		private iTweenEvent m_itweenEvent;


		public override void Awake()
		{
			this.setUp();
			setDefaults();
		}

		public void setDefaults ()
		{
			m_topMenu.SetActive(false);
		}

		public void goToNextScene()
		{
			Application.LoadLevel( 2 );
		}

		public void togglePanel()
		{
			m_mainPanel.SetActive(!m_mainPanel.activeSelf);
		}

		public void toggleTopPanel ()
		{
			m_topMenu.SetActive(!m_topMenu.activeSelf);
		}


		public void quitGame()
		{
			//Do Save stuff here
			Application.Quit();
		}

		public void changeScene()
		{
			Application.LoadLevel(2);
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.M)) {
				togglePanel();
			}
		}

		void OnLevelWasLoaded(int level) 
		{
			if(level == 1) {
				m_topMenu.SetActive(false);
				gameObject.GetComponent<MouseLook>().enabled = false;
				m_itweenEvent.Play();
			} else {
				GameObject startPoint =  GameObject.Find("StartPoint") as GameObject;
				if(startPoint != null) {
					transform.position = startPoint.transform.position;
					transform.rotation = startPoint.transform.rotation;
				}
				gameObject.GetComponent<MouseLook>().enabled = true;
				m_topMenu.SetActive(true);
				m_itweenEvent.Stop();

			}
		}
	}
}