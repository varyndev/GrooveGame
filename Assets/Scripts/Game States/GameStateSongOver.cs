using UnityEngine;
using System.Collections;

// Song ended, implement the state logic for the end game scenario.
// Show UI score, coins earned
// Animate character based on win/lose
// Wait for Menu button click

namespace BoogieDownGames {
	
	public sealed class GameStateSongOver : FSMState<BaseGameController> {
		
		static readonly GameStateSongOver instance = new GameStateSongOver();

		public GameObject gameOverCanvas;
		private bool playerWins;

		public static GameStateSongOver Instance 
		{
			get { return instance; }
		}
		
		static GameStateSongOver () { }
		private GameStateSongOver ()
		{
			playerWins = false;
		}
		
		public override void Enter (BaseGameController p_game)
		{
			DanceGameController danceGameContoller = DanceGameController.Instance;
			GameMaster gameMaster = (GameMaster) GameMaster.Instance;
			Player.Instance.SetLastPlayCompleted (danceGameContoller.FinalScore, danceGameContoller.CoinsEarned, gameMaster.CurrentScene, gameMaster.CurrentModel, gameMaster.CurrentSong);
			playerWins = gameMaster.SongWon;
			PlayWinLoseAnimation (p_game);

			// Show Game Over UI
			UIManager uiManager = UIManager.Instance;
			if (uiManager != null) {
				uiManager.ShowGameOver(true);
			}
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

		private void PlayWinLoseAnimation (BaseGameController p_game)
		{
			if (playerWins) {
				NotificationCenter.DefaultCenter.PostNotification (p_game, "PlayWin");
			} else {
				NotificationCenter.DefaultCenter.PostNotification (p_game, "PlayLose");
			}
		}
	}
}