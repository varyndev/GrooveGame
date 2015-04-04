using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateWon :  FSMState<BaseGameController> {
		
		static readonly GameStateWon instance = new GameStateWon();
		public static GameStateWon Instance 
		{
			get { return instance; }
		}
		
		static GameStateWon() { }
		private GameStateWon() { }
		
		public override void Enter (BaseGameController p_game)
		{
			Debug.Log("I am in Won song state");

			DanceGameController.Instance.InitWonSequence();
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{
			DanceGameController.Instance.RunWonSequence();
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			
			
		}
	}
}