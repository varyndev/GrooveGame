using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class GameMaster : BaseGameController {


		public override void Awake()
		{
			base.Awake();
		}

		public override void Update () 
		{
			GameFsm.runOnUpdate();
			SceneFsm.runOnUpdate();
		}
		
		public override void FixedUpdate()
		{
			GameFsm.runOnFixedUpdate();
			SceneFsm.runOnFixedUpdate();
		}

		public override void Pause()
		{
			GameFsm.ChangeState(GameStatePause.Instance);
		}

		public override void UnPause()
		{
			base.UnPause();
			GameFsm.ChangeState(GameStateRun.Instance);
		}
	}
}