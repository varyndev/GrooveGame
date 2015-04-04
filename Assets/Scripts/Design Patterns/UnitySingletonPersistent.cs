/*
 * Misael Aponte Jan 25, 2013
 * This is a persistent singleton it will not die between scenes
 */
using UnityEngine;
using System.Collections;


public class UnitySingletonPersistent<T> : MonoBehaviour
	where T : Component
{
	private static T _instance;
	public static T Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType (typeof(T)) as T;
				if (_instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent (typeof(T)) as T;
				}
			}
			return _instance;
		}
	}
	
	public virtual void Awake ()
	{
		this.setUp();
	}

	public void setUp()
	{
		DontDestroyOnLoad (this.gameObject);
		if (_instance == null) {
			_instance = this as T;
		} else {
			Destroy (gameObject);
		}
	}
}