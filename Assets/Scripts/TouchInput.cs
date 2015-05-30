using UnityEngine;
using System.Collections;

namespace BoogieDownGames{

	public class TouchInput : UnitySingleton<TouchInput> {

		private Ray ray;
		private RaycastHit hit;

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
			if (Input.GetMouseButtonDown(0)) {
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit, 1000)) {
					Debug.DrawLine(ray.origin, hit.point);
					if (hit.collider.gameObject.tag == "Note") {
						//Send message to who needs it
						hit.collider.gameObject.GetComponent<NotesControl>().death();
					}
                    if (hit.collider.gameObject.tag == "Respawn") {
                        //Send message to who needs it
                        hit.collider.gameObject.GetComponent<ArNotesControl>().death();////
                    }
				}
			}
			foreach (Touch touch in Input.touches) {
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
					ray = Camera.main.ScreenPointToRay(touch.position);

					if (Physics.Raycast(ray, out hit, 1000)) {
						Debug.DrawLine(ray.origin, hit.point);
						if(hit.collider.gameObject.tag == "Note") {
							//Send message
							hit.collider.gameObject.GetComponent<NotesControl>().death();
						}
                        if (hit.collider.gameObject.tag == "Respawn") {
                            //Send message to who needs it
                            hit.collider.gameObject.GetComponent<ArNotesControl>().death();
                        }
					}
				}
			}
		}
	}
}