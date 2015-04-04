/*
 * Misael Aponte Feb 4, 2014
 * Object life time gotten tired of rewrtiting the same script 
 * over and over.  So now you can set the object's life time it can either be 
 * destroyed or inactive.
 */
using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class ObjectLifetime : MonoBehaviour {

		enum DEATHYPE { None, Destroy, Sleep };
		[SerializeField]
		DEATHYPE m_deathType = DEATHYPE.None;

		[SerializeField]
		private float m_timeTillDeath;

		delegate void deathTypeDelegate();

		deathTypeDelegate m_deathTypeDelegate;

		void setTheDeathType()
		{
			switch(m_deathType) {
			case DEATHYPE.None:
				m_deathTypeDelegate = DeathByNone;
				break;
			
			case DEATHYPE.Sleep:
				m_deathTypeDelegate = DeathBySleeping;
				break;

			case DEATHYPE.Destroy:
				m_deathTypeDelegate = DeathByDestruction;
				break;
			}

			StartCoroutine(myTimeIsOver(m_timeTillDeath,m_deathTypeDelegate));
		}

		void OnEnable()
		{
			setTheDeathType();
		}

		// Use this for initialization
		void Start () 
		{
			setTheDeathType();
		}
	

		//Deletes the object by destroying
		void DeathByDestruction ()
		{
			DestroyObject(this.gameObject);
		}

		//Death by setting inactive
		void DeathBySleeping ()
		{
			this.gameObject.SetActive(false);
		}

		void DeathByNone()
		{

		}

		IEnumerator myTimeIsOver(float p_sec,deathTypeDelegate p_del )
		{
			yield return new WaitForSeconds(p_sec);
			p_del();
		}
	}
}