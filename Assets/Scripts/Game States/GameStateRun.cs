using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateRun : FSMState<BaseGameController> {
		
		static readonly GameStateRun instance = new GameStateRun();
		public static GameStateRun Instance 
		{
			get { return instance; }
		}

		static GameStateRun() { }
		private GameStateRun() { }
		
		public override void Enter (BaseGameController p_game)
		{
			if (p_game != null) {
				p_game.PostMessage("OnStateRunEnter");
			}
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{
			p_game.PostMessage("OnStateRunUpdate");
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			p_game.PostMessage("OnStateRunFixedUpdate");
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			p_game.PostMessage("OnStateRunExit");
		}
	}
}