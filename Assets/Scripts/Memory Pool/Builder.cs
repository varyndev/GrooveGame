/*
 * Misael Aponte Jan 25, 2014
 * Builder class used to store the game objects in the scene
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BoogieDownGames
{
	
	[Serializable]
	public class Builder : PropertyAttribute
	{
		[SerializeField]
		private string m_name;	//Used to tag the builder to for use with notification center
		
		[SerializeField]
		private GameObject m_prefab; //The prefab we instantiate from
		
		[Range (0, 1000)]
		public int m_buffer;	//The limit before we have to instantiate
		
		[SerializeField]
		private int m_activeCount; //How many active 
		
		[SerializeField]
		private int m_deadCount;	//How many dead
		
		[SerializeField]
		private bool m_okToInstantiate;	//Is it ok to instantiate
		
		[SerializeField]
		private bool m_initialState = true;	//The initial state of the object
		
		[SerializeField]
		private bool m_findOrInstatiate = true;
		
		[SerializeField]
		private List<GameObject> m_objBank;
		
		enum BuildState { Instantiate, Find, DropIn };

		[SerializeField]
		private BuildState m_buildState;

		[SerializeField]
		private Queue<GameObject> m_deadBank = new Queue<GameObject>();
		
		#region properties

		public Queue<GameObject> DeadBank
		{
			get { return m_deadBank; }
		}

		public List<GameObject> ObjBank
		{
			get{ return m_objBank; }
		}
		
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}
		
		public bool InitialState
		{
			get { return this.m_initialState; }
			set {this.m_initialState = value;
				foreach(GameObject obj in m_objBank) {
					obj.SetActive(m_initialState);
				}
			}
		}
		
		//This returns the total counts of objects
		public uint TotalCount 
		{
			get{ return (uint)m_objBank.Count; }
			
		}
		
		public int Limit
		{
			get{ return m_buffer; }
		}
		
		public bool Instantiate
		{
			get{ return m_okToInstantiate; }
			set{ m_okToInstantiate = value; }
		}
		
		public GameObject PreFab
		{
			get{ return m_prefab; }
			set{ m_prefab = value;}
		}
		
		public int ActiveCount
		{
			get { return m_activeCount; }
			set { m_activeCount = value; }
		}
		
		public int DeadCount
		{
			get { return m_deadCount; }
			set { m_deadCount = value; }
		}
		
		#endregion
		
		public Builder()
		{
		}
		
		~Builder()
		{
			/*
			foreach(GameObject obj in m_objBank) {
				MonoBehaviour.DestroyImmediate(obj);
			}
			m_objBank.Clear();
			*/
		}
		
		public void build()
		{
			if( String.IsNullOrEmpty(m_name) == true) {
				m_name = m_prefab.name;
			}
			switch(m_buildState)
			{
				
			case BuildState.Instantiate:
				if(m_prefab != null) {
					m_name = m_prefab.name;
					for(int index = 0; index < m_buffer; index++) {
						GameObject obj = GameObject.Instantiate(m_prefab,Vector3.zero, Quaternion.identity) as GameObject;
						obj.SetActive(m_initialState);
						m_objBank.Add(obj);
					}
					m_buffer = m_objBank.Count;
				}
				break;
				
			case BuildState.Find:
				if(m_name != null) {
					GameObject [] objs = GameObject.FindGameObjectsWithTag(m_name);
					foreach(GameObject obj in objs) {
						obj.SetActive(m_initialState);
						m_objBank.Add(obj);
					}
					m_buffer = m_objBank.Count;
				}
				break;
				
			case BuildState.DropIn:
				
				if(m_objBank.Count > 0) {
					//m_name = m_objBank[0].name;
				}
				foreach(GameObject obj in m_objBank) {
					obj.SetActive(m_initialState);
				}
				m_buffer = m_objBank.Count;
				break;
			}
		}
		
		public void decreaseLimit(int p_amount = 1)
		{
			GameObject obj = m_objBank.Last();
			if(m_objBank.Count > 0) {
				m_objBank.RemoveAt(m_objBank.Count-1);
				GameObject.Destroy(obj);
			}
		}
		
		public void increaseLimit(int p_amount)
		{
			if(m_prefab != null) {
				for(int index = 0; index < p_amount; ++index) {
					GameObject obj = MonoBehaviour.Instantiate(m_prefab) as GameObject;
					obj.SetActive(m_initialState);
					m_objBank.Add(obj);
					m_buffer++;
				}
			}
		}
		
		public void checkDifference()
		{
			int diff = Mathf.Abs( m_buffer - m_objBank.Count );
			if(diff == 0) {
				//do nothing
			}
			else if(m_buffer > m_objBank.Count) {
				for(int index = 0; index < diff; ++index) {
					GameObject obj = MonoBehaviour.Instantiate(m_prefab) as GameObject;
					obj.SetActive(m_initialState);
					m_objBank.Add(obj);
				}
			}
		}
		
		//try and find a live one
		public GameObject findAlive()
		{
			GameObject temp = null;
			foreach(GameObject obj in m_objBank) {
				if(obj.activeSelf == true) {
					temp = obj;
					break;
				}
			}
			if(temp == null && m_okToInstantiate == true) {
				temp = MonoBehaviour.Instantiate(m_prefab) as GameObject;
				temp.SetActive(true);
				m_objBank.Add(temp);
				m_buffer++;
			}
			return temp;
		}

		public GameObject findInQue()
		{
			GameObject obj = (GameObject)m_deadBank.Peek();
			m_deadBank.Dequeue();
			obj.SetActive(true);
			return obj;
		}
		//Try and find a live one
		public GameObject findDead()
		{
			GameObject temp = null;
			foreach(GameObject obj in m_objBank) {
				if(obj.activeSelf == false) {
					temp = obj;
					temp.SetActive(true);
					break;
				}
			}
			if(temp == null && m_okToInstantiate == true) {
				temp = MonoBehaviour.Instantiate(m_prefab) as GameObject;
				temp.SetActive(true);
				m_objBank.Add(temp);
				m_buffer++;
			}
			return temp;
		}
	}
}