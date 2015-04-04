using UnityEngine;
using System.Collections;

namespace BoogieDownGames {

	public class MenuCtrl : BaseSceneController {

		public override void GoToScene (int p_scene)
		{
			GameMaster.Instance.SceneFsm.ChangeState(CtrlStateGame.Instance);
		}

		public void QuitGame()
		{
			GameMaster.Instance.QuitGame();
		}
	}
}