using UnityEngine;
using System.Collections;


namespace BoogieDownGames
{

	public class BuildingController : MonoBehaviour 
	{

		enum LoadType { String, Int }
		[SerializeField]
		private LoadType m_loadBy = LoadType.String;

		[SerializeField]
		private int m_levelIndex;

		[SerializeField]
		private string m_levelName;

		public void OnTriggerEnter(Collider p_coll)
		{
			if(m_loadBy == LoadType.Int) {
				Application.LoadLevel(m_levelIndex);
			} else {
				if(m_levelName.Length > 0) {
					Application.LoadLevel(m_levelName);
				} else {
					Application.LoadLevel(m_levelIndex);
				}
			}
		}
	}
}