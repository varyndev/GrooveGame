/*
 * Memory Pool controls the activation and deactivation of objects
 * in the game.  It can also creat more objects if need be. Can be
 * set as an option.  Works in tangent with the notification center 
 * To enchance usage.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BoogieDownGames
{
	
	public class MemoryPool : UnitySingleton<MemoryPool>{
		
		[SerializeField]
		private bool m_loadOnLevel;
		public Dictionary<string,int> m_keyBank = new Dictionary<string,int>();//Keep a bank of keys
		public Builder[] m_builder;
		bool m_bHasLoadedBefore = false;
		// Use this for initialization
		void Awake()
		{
			//search for all the objects
			createBuilderBanks();
			NotificationCenter.DefaultCenter.AddObserver(this,"findMyObjectNotification");
			
			//Debug.LogError("I've been called")
		}
		
		void Start () 
		{
			/*
			if(m_loadOnLevel == false) {
				//Create the storage banks
				createBuilderBanks();
			}
			NotificationCenter.DefaultCenter.AddObserver(this,"findMyObjectNotification");
			*/
		}

		public Builder getBank(string p_key)
		{
			return m_builder[m_keyBank[p_key]];
		}
		
		public void findObjs(string p_key,bool p_state)
		{
			GameObject obj = null;
			if(p_state == false) {
				obj = m_builder[m_keyBank[p_key]].findDead();
				//obj.SetActive(true);
			} else {
				obj = m_builder[m_keyBank[p_key]].findAlive();
				//obj.SetActive(false);
			}
			if(obj != null) {
				obj.transform.position = new Vector3(UnityEngine.Random.Range(-100,100),0,UnityEngine.Random.Range(-100,100));
			}
		}
		
		public GameObject findAndGetObjs(string p_key,bool p_state)
		{
			GameObject obj = null;
			if(p_state == false) {
				if(m_keyBank.ContainsKey(p_key)) {
					obj = m_builder[m_keyBank[p_key]].findDead();
				} else {
					Debug.LogError("Key not found => " + p_key);
				}
				//obj.SetActive(true);
			} else {
				if(m_keyBank.ContainsKey(p_key)) {
					obj = m_builder[m_keyBank[p_key]].findAlive();
				} else {
					Debug.LogError("Key not found => " + p_key);
				}
				//obj.SetActive(false);
			}
			return obj;
		}

		public GameObject findAndGetObjs(string p_key)
		{
			return m_builder[m_keyBank[p_key]].findInQue();
		}
		
		public void findMyObjectNotification(NotificationCenter.Notification p_not)
		{
			if(p_not.data.ContainsKey("name")) {
				
				string name = (string)p_not.data["name"];
				
				Vector3 pos = new Vector3();
				
				if(p_not.data.ContainsKey("pos")) {
					pos = (Vector3)p_not.data["pos"];
				}
				
				//get the state 
				bool state = false;
				if(p_not.data.ContainsKey("state")) {
					state = (bool)p_not.data["state"];
				}
				//make sure we have the data for the postioning if not set it to the calling object position
				GameObject obj = null;
				if(state == true) { 
					obj = m_builder[m_keyBank[name]].findAlive();
					if(obj != null) {
						obj.SetActive(false);
					}
				} else {
					obj = m_builder[m_keyBank[name]].findDead();
					if(obj != null) {
						obj.SetActive(true);
					}
				}
				if(obj != null) {
					obj.transform.position = pos;
				}
			}
		}
		
		//Starts off building the bank
		public void createBuilderBanks()
		{
			m_bHasLoadedBefore = true;
			for(int index =0; index < m_builder.Length; ++index) {
				m_builder[index].build();
				if(!m_keyBank.ContainsKey(m_builder[index].Name) && !m_keyBank.ContainsKey(m_builder[index].PreFab.name)) {
					if(m_builder[index].Name != null) {
						m_keyBank.Add(m_builder[index].Name,index);
					} else {
						m_keyBank.Add(m_builder[index].PreFab.name,index);
					}
				}
			}
		}
		
		void OnLevelWasLoaded(int level)
		{
			if(m_loadOnLevel == true && m_bHasLoadedBefore == false) {
				createBuilderBanks();
			}
		}
	}
}