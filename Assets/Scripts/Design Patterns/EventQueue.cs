/*
 * My implementation of an event system
 * This first iteration waits on a call from
 * the notification center to execute an event
 */
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BoogieDownGames {

	public class EventQueue : MonoBehaviour {
		[SerializeField]
		private List<Event> m_que;

		private Dictionary<string, Event> m_eventBank = new Dictionary<string,Event>();

		// Use this for initialization
		void Start () 
		{
			//Add the array to the dictionary
			foreach(Event e in m_que) {
				m_eventBank.Add(e.EventName,e);
			}
			NotificationCenter.DefaultCenter.AddObserver(this,"PlayEvent");
		}

		//This Calls the event and if it's in the dictionary then it gets called
		public void PlayEvent(NotificationCenter.Notification p_not)
		{
			//Get the event's name
			string eventName = (string)p_not.data["eventName"];
			if(m_eventBank.ContainsKey(eventName)) {
				m_eventBank[eventName].ExecuteEvent();
				if(m_eventBank[eventName].IsRunnedOnce) {
					m_eventBank.Remove(eventName);
				}
			}
		}
	}
}