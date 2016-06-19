using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class BaseSceneController : UnitySingleton<BaseSceneController> {

		[SerializeField]
		protected string m_nextSceneName;

		[SerializeField]
		protected bool m_isControllable; //Boolean that allows controls to be used
	
		public virtual void Init()
		{
			//Debug.LogWarning("Init needs to be declared");
		}

		public virtual void Run()
		{
			//Debug.LogWarning("Run needs to be declared");
		}

		public virtual void GoToScene(string p_scene)
		{
			Application.LoadLevel(p_scene);
		}

		public virtual void GoToScene(int p_scene)
		{
			Application.LoadLevel(p_scene);
		}

		public virtual void QuitScene()
		{

		}

		public virtual void RunGame()
		{

		}

		public void PostMessage(string p_func, string p_message)
		{
			Hashtable dat = new Hashtable();
			dat.Add("msg",p_message);
			NotificationCenter.DefaultCenter.PostNotification(this,p_func,dat);
		}
	}
}