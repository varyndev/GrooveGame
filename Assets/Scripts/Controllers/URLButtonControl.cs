using UnityEngine;
using System.Collections;

public class URLButtonControl : MonoBehaviour {
	
	public bool isHomepageButton;
	public bool isCoinButton;

	public void visitURL(){
		if (isHomepageButton) {
			Application.OpenURL("http://groovegame.com");
		}

		if (isCoinButton) {
			Application.OpenURL("http://groovegame.com");
		}
	}
}
