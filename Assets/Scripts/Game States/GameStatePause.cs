using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStatePause :  FSMState<BaseGameController> {
		
		static readonly GameStatePause instance = new GameStatePause();
		public static GameStatePause Instance 
		{
			get { return instance; }
		}
		
		static GameStatePause() { }
		private GameStatePause() { }
		
		public override void Enter (BaseGameController p_game)
		{
			Debug.Log("I am in Game state Pause");

			p_game.PostMessage("OnStatePauseEnter");

			if(p_game.GameFsm.PreviousState == GameStatePause.Instance) {
				Debug.Log ("Previous state was pause so switching to play");
				p_game.GameFsm.ChangeState(GameStateRun.Instance);
			} else {
				//Time.timeScale = 0f;
			}
			
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{
			p_game.PostMessage("OnStatePauseUpdate");
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			p_game.PostMessage("OnStatePauseFixedUpdate");
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			p_game.PostMessage("OnStatePauseExit");
			//Time.timeScale = 1f;
		}
	}
}