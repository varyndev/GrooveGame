using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public enum TriggerType { Top, Middle, Bottom, Die }

	public class BoundTrigger : MonoBehaviour {

		[SerializeField]
		private TriggerType m_type;

		public TriggerType MyType
		{
			get { return m_type; }
			set { m_type = value; }
		}

		void OnCollisionEnter(Collision collisionInfo)
		{
			Debug.LogError("&&&&&&&&&");
		}
	}
}