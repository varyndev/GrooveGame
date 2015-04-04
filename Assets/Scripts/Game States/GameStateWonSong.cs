using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateWonSong :  FSMState<BaseGameController> {
		
		static readonly GameStateWonSong instance = new GameStateWonSong();
		public static GameStateWonSong Instance 
		{
			get { return instance; }
		}
		
		
		
		static GameStateWonSong() { }
		private GameStateWonSong() { }
		
		public override void Enter (BaseGameController p_game)
		{

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