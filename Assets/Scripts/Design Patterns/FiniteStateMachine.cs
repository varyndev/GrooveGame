/*
 * The finite state machine.  Used to change and update the states,
 * being runned.
 * */

public class FiniteStateMachine <T>  {
	
	private T Owner;
	private FSMState<T> m_currentState;
	private FSMState<T> m_previousState;
	private FSMState<T> m_globalState;
	private string m_stateName;
	
	#region Properties
		public FSMState<T> CurrentState
		{
			get{ return m_currentState; }
			set{ m_currentState = value; }
		}
	
		public FSMState<T> PreviousState
		{
			get{ return m_previousState; }
			set{ m_previousState = value; }
		}
		
		public FSMState<T> GlobalState
		{
			get{ return m_globalState; }
			set{ m_globalState = value; }
		}
		
	#endregion
	
	public void Awake()
	{		
		m_currentState = null;
		m_previousState = null;
		m_globalState = null;
	}
	
	public void Configure(T owner, FSMState<T> InitialState) {
		Owner = owner;
		ChangeState(InitialState);
	}

	public void  runOnUpdate()
	{
		if (m_globalState != null)  m_globalState.ExecuteOnUpdate(Owner);
		if (m_currentState != null) m_currentState.ExecuteOnUpdate(Owner);
	}

	public void runOnFixedUpdate()
	{
		if (m_globalState != null)  m_globalState.ExecuteOnFixedUpdate(Owner);
		if (m_currentState != null) m_currentState.ExecuteOnFixedUpdate(Owner);
	}
 
	public void  ChangeState(FSMState<T> NewState)
	{	
		m_previousState = m_currentState;
 
		if (m_currentState != null)
			m_currentState.Exit(Owner);
 
		m_currentState = NewState;
 
		if (m_currentState != null)
			m_currentState.Enter(Owner);
	}
 
	public void  RevertToPreviousState()
	{
		if (m_previousState != null)
			ChangeState(m_previousState);
	}
};