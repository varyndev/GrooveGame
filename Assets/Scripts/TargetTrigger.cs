using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace BoogieDownGames {

	public class TargetTrigger : MonoBehaviour {

		public Vector3 m_vel;

		public float m_speed;


		private Plane[] planes;

		void Start() {

			planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		}
		void Update() {
			if (GeometryUtility.TestPlanesAABB(planes, gameObject.GetComponent<Collider>().bounds)) {
				//Debug.Log(gameObject.name + " has been detected!");
			}
			else{
				Debug.Log("Nothing has been detected");
				transform.Translate(m_vel * m_speed * Time.deltaTime);
			}

		}
	

		void OnTriggerEnter(Collider other)
		{
			DestroyObject(other.gameObject);
			Player.Instance.m_misses++;
		}

		void OnTriggerStay(Collider other) 
		{

		}
		
		void OnTriggerExit(Collider other) 
		{

		}
	}
}