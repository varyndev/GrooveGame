using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateLostSong :  FSMState<BaseGameController> {
		
		static readonly GameStateLostSong instance = new GameStateLostSong();
		public static GameStateLostSong Instance 
		{
			get { return instance; }
		}

		static GameStateLostSong() { }
		private GameStateLostSong() { }
		
		public override void Enter (BaseGameController p_game)
		{
			p_game.PostMessage("OnStateLostSongEnter");
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{
			p_game.PostMessage("OnStateLostSongUpdate");

		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			p_game.PostMessage("OnStateLostSongFixedUpdate");
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			p_game.PostMessage("OnStateLostSongExit");	
		}
	}
}