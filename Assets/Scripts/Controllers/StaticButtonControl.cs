using UnityEngine;
using System.Collections;

public class StaticButtonControl : MonoBehaviour {
	
	public bool isHomepageButton, isCoinButton, isMainMenuButton, isPlayAgainButton, isModelLockButton, isSceneLockButton, isSongLockButton;

	public void activateButton(){
		if (isHomepageButton) {
			Application.OpenURL("http://groovegame.com");
			return;
		}

		if (isCoinButton) {
			Application.OpenURL("http://groovegame.com/ggcoin");
			return;
		}

		if (isMainMenuButton) {
			Application.LoadLevel("Menu");
			return;
		}

		if (isPlayAgainButton) {
            AmazonAdController.playAd += 1;
			Application.LoadLevel(Application.loadedLevel);
			return;
		}
		
		if (isModelLockButton) {
			// TODO: link to code to begin the purchase model flow
			Application.OpenURL("http://groovegame.com/songs");
			return;
		}
		
		if (isSceneLockButton) {
			// TODO: link to code to begin the purchase scene flow
			Application.OpenURL("http://groovegame.com/songs");
			return;
		}
		
		if (isSongLockButton) {
			// TODO: link to code to begin the purchase songs flow
			Application.OpenURL("http://groovegame.com/songs");
			return;
		}
	}
}
