using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        //Used to move the spawner around
        public float theX, theY, theZ, theHeight;
        Vector3 theLeft;
        Vector3 theRight;
        private float turnTime;
        //Time to lerp
        float lerpTime;
        [SerializeField] bool movingRight, startLeft;

		private List<GameObject> m_notes;

        void Start(){
			m_notes = new List<GameObject> ();

			// Spawner's prefab should be invisible
			NotificationCenter.DefaultCenter.AddObserver(this, "spawnPrefab");
			NotificationCenter.DefaultCenter.AddObserver(this, "destroyAllPrefab");

            theLeft = new Vector3(-theX, theY, theZ);
            theRight = new Vector3(theX, theY, theZ);
            turnTime = 6.0f;
            //movingRight = true;

            /*if (startLeft)
                transform.position = theLeft;
            else transform.position = theRight;*/

			// Set the position based on the Main Camera's Set Position
			//Transform mainCameraTransform = Camera.main.transform;
			//transform.position = mainCameraTransform.position + (mainCameraTransform.forward*spawnDistance);
		}

        void Update(){

            if (movingRight){
                lerpTime += Time.deltaTime * 0.15f;
                Vector3 curPos = Vector3.Lerp(theLeft, theRight, lerpTime);
                curPos.y += theHeight * Mathf.Sin(Mathf.Clamp01(lerpTime) * Mathf.PI);
                transform.position = curPos;

                //if (transform.position.x > theRight.x - 5f)
                if (turnTime <= 0){
                    movingRight = false;
                    lerpTime = 0;
                    turnTime = 6.0f;
                }
            }
            if (!movingRight){
                lerpTime += Time.deltaTime * 0.15f;
                Vector3 curPos = Vector3.Lerp(theRight, theLeft, lerpTime);
                curPos.y += theHeight * Mathf.Sin(Mathf.Clamp01(lerpTime) * Mathf.PI);
                transform.position = curPos;

                // if (transform.position.x < theLeft.x + 5f)
                if (turnTime <= 0){
                    movingRight = true;
                    lerpTime = 0;
                    turnTime = 6.0f;
                }
            }

            if (turnTime > 0)
                turnTime -= 1 * Time.deltaTime;
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
			if(!m_notes.Contains(obj))
				m_notes.Add (obj);
		}

		/// <summary>
		/// Destroy all active notes
		/// </summary>
		public void destroyAllPrefab()
		{
			foreach (var obj in m_notes) 
				obj.SetActive(false);
		}
	}
}