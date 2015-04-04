using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateCutScene :  FSMState<BaseGameController> {
		
		static readonly GameStateCutScene instance = new GameStateCutScene();
		public static GameStateCutScene Instance 
		{
			get { return instance; }
		}
		
		
		
		static GameStateCutScene() { }
		private GameStateCutScene() { }
		
		public override void Enter (BaseGameController p_game)
		{
			if(p_game != null) {
				p_game.PostMessage("OnStateCutSceneEnter");
			}
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{
			
			p_game.PostMessage("OnStateCutSceneUpdate");
			
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			p_game.PostMessage("OnStateCutSceneFixedUpdate");
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			p_game.PostMessage("OnStateCutSceneExit");
			
		}
	}
}