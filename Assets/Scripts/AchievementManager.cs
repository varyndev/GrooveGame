/*
 * Achievement manager 
 * Manages all of the achievments.
 */
using UnityEngine;
using System.Collections.Generic;
using System;

namespace BoogieDownGames {

	public class AchievementManager : UnitySingletonPersistent<AchievementManager> {

		[SerializeField]
		private Achievement [] m_achievements;

		[SerializeField]
		private List<Achievement> m_unlocks = new List<Achievement>();

		//This is called upon notification
		public void ReadEvent( NotificationCenter.Notification p_not)
		{
			//get the event
			string e = (string)p_not.data["event"];

			foreach(Achievement a in m_achievements) {
				//Don't bother if it is unlocked
				if(a.IsCompleted == false) {
					bool isunlocked = a.CheckProperty(e);
					Debug.LogError("UNlock =========> " + isunlocked.ToString());
					if(isunlocked == true) {
						//save it and send a message
						m_unlocks.Add(a);
						Debug.LogError("We unlocked************");
					}
				}
			}
		}

		void OnLevelWasLoaded(int level)
		{
			NotificationCenter.DefaultCenter.AddObserver(this,"ReadEvent");
		}
	}
}