using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class GameStateInit :  FSMState<BaseGameController> {
		
		static readonly GameStateInit instance = new GameStateInit();
		public static GameStateInit Instance 
		{
			get { return instance; }
		}
		
		static GameStateInit() { }
		private GameStateInit() { }
		
		public override void Enter (BaseGameController p_game)
		{
			//Debug.Log("I am in Game state init");
		
			/*
			 * OnStateInitEnter
			 */

			p_game.PostMessage("OnStateInitEnter");
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{
			/*
			 * OnStateInitUpdate
			 */
			p_game.PostMessage("OnStateInitUpdate");
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{
			/*
			 * OnStateInitFixedUpdate
			 */
			p_game.PostMessage("OnStateInitFixedUpdate");
		}
		
		public override void Exit(BaseGameController p_game) 
		{
			/*
			 * OnStateInitExit
			 */
			p_game.PostMessage("OnStateInitExit");
			//Debug.Log("Leaving Game state init");
		}
	}
}