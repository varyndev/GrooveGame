using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class CtrlStateSplash :  FSMState<BaseGameController> {
		
		static readonly CtrlStateSplash instance = new CtrlStateSplash();
		public static CtrlStateSplash Instance 
		{
			get { return instance; }
		}
		
		static CtrlStateSplash() { }
		private CtrlStateSplash() { }
		
		public override void Enter (BaseGameController p_game)
		{
			Debug.Log("Entering Ctrlstate splash");
			SplashSceneCtrl.Instance.Init();
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) 
		{

			SplashSceneCtrl.Instance.Run();
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game)
		{

		}
		
		public override void Exit(BaseGameController p_game) 
		{
			Debug.Log("Leaving ctrlstate");
		}
	}
}