using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class CtrlStateGame :  FSMState<BaseGameController> {
		
		static readonly CtrlStateGame instance = new CtrlStateGame();

		public static CtrlStateGame Instance 
		{
			get { return instance; }
		}
		
		static CtrlStateGame() { }
		private CtrlStateGame() { }
		
		public override void Enter (BaseGameController p_game)
		{
			// Ask the SceneController which scene was set by the user, map that index to a scene id, then load that scene
			int nextScene = SceneController.Instance.GetCurrentSceneFromIndex(GameMaster.Instance.CurrentScene);
			Application.LoadLevel(nextScene);
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