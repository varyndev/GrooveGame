using UnityEngine;
using System.Collections;

namespace BoogieDownGames{

	public class Spawner : MonoBehaviour {

		void Start()
		{
			NotificationCenter.DefaultCenter.AddObserver(this,"spawnPrefab");
		}

		[SerializeField]
		private GameObject m_prefab;

		public void spawnPrefab()
		{
			GameObject obj = MemoryPool.Instance.findAndGetObjs(m_prefab.name,false);
			obj.transform.position = transform.position;
			DanceGameController.Instance.TotalNotes ++;
		}
	}
}