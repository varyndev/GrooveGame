using UnityEngine;
using System.Collections;

namespace BoogieDownGames{

	/// <summary>
	/// Spawner.
	/// Responsible for creating and distributing notes
	/// </summary>
	public class Spawner : MonoBehaviour {

		/// <summary>
		/// The distance between the Spawner and the Camera about the Z axis
		/// </summary>
		public float spawnDistance = 5f;

		void Start()
		{
			// Spawner's prefab should be invisible

			NotificationCenter.DefaultCenter.AddObserver(this, "spawnPrefab");
			NotificationCenter.DefaultCenter.AddObserver(this, "destroyAllPrefab");

			// Set the position based on the Main Camera's Set Position
			Transform mainCameraTransform = Camera.main.transform;
			transform.position = mainCameraTransform.position + (mainCameraTransform.forward*spawnDistance);
		}

		/// <summary>
		/// Prefab for the notes
		/// </summary>
		[SerializeField]
		private GameObject m_prefab;

		/// <summary>
		/// Spawns the notes from the Spawner's position
		/// </summary>
		public void spawnPrefab()
		{
			GameObject obj = MemoryPool.Instance.findAndGetObjs(m_prefab.name, false);
			obj.transform.position = transform.position;
		}

		/// <summary>
		/// Destroy all active notes
		/// </summary>
		public void destroyAllPrefab()
		{
			Debug.Log ("Destroy all notes");
		}
	}
}