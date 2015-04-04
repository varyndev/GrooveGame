using UnityEngine;
using System.Collections;


abstract public class FSMState <T> {
	
	private string m_name; 	//The name of the state
	public string StateName
	{
		get{ return m_name; }
		set{ m_name = value; }
	}
	abstract public void Enter (T entity);
		
	abstract public void ExecuteOnUpdate (T entity);

	abstract public void ExecuteOnFixedUpdate (T entity);

	abstract public void Exit(T entity);
}
