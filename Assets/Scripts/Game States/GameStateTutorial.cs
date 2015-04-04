using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateTutorial :  FSMState<BaseGameController> {
		
		static readonly GameStateTutorial instance = new GameStateTutorial();
		public static GameStateTutorial Instance 
		{
			get { return instance; }
		}
		
		static GameStateTutorial() { }
		private GameStateTutorial() { }
		
		public override void Enter (BaseGameController p_game)
		{
			p_game.PostMessage("OnStateTutorialEnter");
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{
			p_game.PostMessage("OnStateTutorialUpdate");
			
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			p_game.PostMessage("OnStateTutorialFixedUpdate");
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			p_game.PostMessage("OnStateTutorialExit");	
		}
	}
}