using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverCtrl : MonoBehaviour {

	private string winText, loseText;

	public void Start(){
		winText = "You Win! \nGreat job getting your groove on!";
		loseText = "Nice Try! \n Better luck next time!";
	}

	public void formatGameOver(bool isWin, int score, int coins){
		GameObject.Find ("GameOverText").GetComponent<Text> ().text = (isWin) ? winText : loseText;

		GameObject.Find ("Score Text").GetComponent<Text> ().text = score.ToString ();

		GameObject.Find ("Coins Earned").GetComponent<Text> ().text = coins.ToString ();

		GameObject.Find ("GameOverCanvas").GetComponent<Canvas> ().enabled = true;
	}
}
