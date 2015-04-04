using UnityEngine;
using System.Collections;

namespace BoogieDownGames{

	public class TouchInput : UnitySingleton<TouchInput> {

		// Use this for initialization
		void Start () 
		{
			NotificationCenter.DefaultCenter.AddObserver(this, "OnStateRunUpdate");
		}

		public void OnStateRunUpdate()
		{
			run();
		}

		public void run()
		{

			if(Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				int layerMask = 1 << 7;
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 100)) {

					Debug.DrawLine(ray.origin, hit.point);
					if(hit.collider.gameObject.tag == "Note") {
						//Send message to who needs it
						hit.collider.gameObject.GetComponent<NotesControl>().death();
					}
				}
				
			}

			foreach (Touch touch in Input.touches) {
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
					int layerMask = 1 << 9;
					Ray ray = Camera.main.ScreenPointToRay(touch.position);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit, 100)) {
						Debug.DrawLine(ray.origin, hit.point);
						if(hit.collider.gameObject.tag == "Note") {
							//Send message
							hit.collider.gameObject.GetComponent<NotesControl>().death();
						}
					}
				}
			}

		}
	}
}