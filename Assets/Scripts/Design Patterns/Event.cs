/* 
 * a single event
 */
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BoogieDownGames {

	[Serializable]
	public class Event : PropertyAttribute {

		[SerializeField]
		private string m_eventName; //The name

		[SerializeField]
		private string m_funcCall; //The name of the function to call

		[SerializeField]
		private string [] m_messages; //The message to send

		[SerializeField]
		private bool m_isRunnedOnce;

		#region PROPERTIES

		public bool IsRunnedOnce
		{
			get { return m_isRunnedOnce; }
			set { m_isRunnedOnce = value; }
		}

		public string FuncCall
		{
			get { return m_funcCall; }
			set { m_funcCall = value; }
		}

		public string EventName
		{
			get { return m_eventName; }
			set { m_eventName = value; }
		}

		#endregion

		public void ExecuteEvent()
		{
			Hashtable dat = new Hashtable();
			for(int index = 0; index < m_messages.Length; ++index) {
				dat.Add("msg" + index.ToString(),m_messages[index]);
			}
			NotificationCenter.DefaultCenter.PostNotification(null,m_funcCall, dat);
		}

	}
}