using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class CtrlStateMenu :  FSMState<BaseGameController> {
		
		static readonly CtrlStateMenu instance = new CtrlStateMenu();
		public static CtrlStateMenu Instance 
		{
			get { return instance; }
		}
		
		static CtrlStateMenu() { }
		private CtrlStateMenu() { }
		
		public override void Enter (BaseGameController p_game)
		{
			Application.LoadLevel(1);
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