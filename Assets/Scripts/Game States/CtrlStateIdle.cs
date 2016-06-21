using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class CtrlStateIdle :  FSMState<BaseGameController> {
		
		static readonly CtrlStateIdle instance = new CtrlStateIdle();
		public static CtrlStateIdle Instance 
		{
			get { return instance; }
		}
		
		static CtrlStateIdle() { }
		private CtrlStateIdle() { }
		
		public override void Enter (BaseGameController p_game)
		{
			//Debug.Log("I am in idle state");

		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{

		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			
			
		}
	}
}