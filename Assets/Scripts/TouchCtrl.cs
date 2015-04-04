/*
 * Attach this script to any static gameobject then set the settings
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ControlType { mouse, touch };


public class TouchCtrl : MonoBehaviour {

	public GameObject m_obj;
	public Text m_debubgTxt;

	public ControlType m_controlType;


	public float m_zPos = 10.0f;//Used this to set the z positions of the object

	delegate void ControlDelegate();
	ControlDelegate controlDelegate;

	// Use this for initialization
	void Start () 
	{
		if(m_controlType == ControlType.mouse) {
			controlDelegate = mouseControl;
		} else {
			controlDelegate = touchControl;
		}
	
	}

	public void mouseControl()
	{
		Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,m_zPos));
		if(m_obj != null) {
			Ray testRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y, m_zPos));
			RaycastHit testHit;
			
			if(Physics.Raycast(testRay, out testHit, 100))
			{
				//Might need to set X and Z depending on how your game is set up as touch.position is a 2D Vector
				m_obj.transform.position = p;
			}
		} else {
			Debug.LogWarning("Please add a game object to m_obj");
		}

	}

	public void touchControl()
	{
		for(int index = 0; index < Input.touchCount; ++index)  {

			//Handle touch 1 here
			if(index == 0) {

				Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.touches[index].position.x,Input.touches[index].position.y,m_zPos));
				m_obj.transform.position = p;
			}else if(index == 1) {
				//handle touch 2 here
				Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.touches[index].position.x,Input.touches[index].position.y,m_zPos));
				m_obj.transform.position = p;
			}

			/*
			 * If you need to only move when it hits something then use this code

			Ray testRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y, camera.farClipPlane));
			RaycastHit testHit;
			
			Vector3 p = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f));
			m_debubgTxt.text = "first=> " + p.ToString();
			
			
			
			if(Physics.Raycast(testRay, out testHit, 100))
			{
				//Might need to set X and Z depending on how your game is set up as touch.position is a 2D Vector
				m_obj.transform.position = p;
			}

			*/

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		controlDelegate();
	}
}
