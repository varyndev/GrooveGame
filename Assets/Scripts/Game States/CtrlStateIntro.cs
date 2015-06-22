﻿using UnityEngine;
using System.Collections;

namespace BoogieDownGames {
	
	public sealed class CtrlStateIntro :  FSMState<BaseGameController> {
		
		static readonly CtrlStateIntro instance = new CtrlStateIntro();

		private int movieScene = 5;
		private float movieLength = 12.0f;
		private IntroMovieVideoPlayer introMovie = null;
		private float movieTimer;

		public static CtrlStateIntro Instance 
		{
			get { return instance; }
		}
		
		static CtrlStateIntro() { }
		private CtrlStateIntro() { }
		
		public override void Enter (BaseGameController p_game) {
			Application.LoadLevel(movieScene);
		}
		
		public override void ExecuteOnUpdate (BaseGameController p_game) {
			if (introMovie == null) {
				introMovie = (IntroMovieVideoPlayer)GameObject.FindObjectOfType (typeof(IntroMovieVideoPlayer));
				PlayIntroVideo();
			} else {
				movieTimer += Time.deltaTime;
				if (movieTimer >= movieLength && introMovie != null) {
					GameMaster.Instance.SceneFsm.ChangeState (CtrlStateMenu.Instance);
				}
			}
		}
		
		public override void ExecuteOnFixedUpdate (BaseGameController p_game) {
		}
		
		public override void Exit(BaseGameController p_game) {
			if (introMovie != null) {
				introMovie.StopMovie();
				introMovie = null;
			}
		}

		private void PlayIntroVideo () {
			if (introMovie != null) {
				movieTimer = 0.0f;
				introMovie.StartMovie ();
			}
		}
	}
}