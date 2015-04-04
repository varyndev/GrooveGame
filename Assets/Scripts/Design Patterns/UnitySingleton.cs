/*
 * Misael Aponte Jan 25, 2014
 * This is a non persistent SingleTon Template
 * Great for creating static objects. Will be destroid at the end of the scene.
 */
using UnityEngine;
using System.Collections;
using System;

public class UnitySingleton<T> : MonoBehaviour
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
}