using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class GameMaster : BaseGameController {

		public static string VERSION = "1.1.1";
		public static string GAME_SKU = "GrooveGame";

		public override void Awake()
		{
			Debug.Log ("Starting " + GAME_SKU + " Version " + VERSION);
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