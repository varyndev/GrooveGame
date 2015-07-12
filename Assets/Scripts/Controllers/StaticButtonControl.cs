using UnityEngine;
using System.Collections;

public class StaticButtonControl : MonoBehaviour {
	
	public bool isHomepageButton, isCoinButton, isMainMenuButton, isPlayAgainButton;

	public void activateButton(){
		if (isHomepageButton) {
			Application.OpenURL("http://groovegame.com");
		}

		if (isCoinButton) {
			Application.OpenURL("http://groovegame.com/ggcoin");
		}

		if (isMainMenuButton) {
			Application.LoadLevel("Menu");
		}

		if (isPlayAgainButton) {
			Application.LoadLevel(Application.loadedLevel);
		}

	}
}
