using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateLost :  FSMState<BaseGameController> {
		
		static readonly GameStateLost instance = new GameStateLost();
		public static GameStateLost Instance 
		{
			get { return instance; }
		}
		
		static GameStateLost() { }
		private GameStateLost() { }
		
		public override void Enter (BaseGameController p_game)
		{
			Debug.LogError("Im in lost");
			
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{

		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			Application.LoadLevel(1);
			
		}
	}
}