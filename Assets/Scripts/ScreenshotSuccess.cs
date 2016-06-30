using UnityEngine;
using System.Collections;

public class ScreenshotSuccess : MonoBehaviour {

    [SerializeField] GameObject successPanel;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
	}

    public void Success(){
        timer = 1.0f;
        successPanel.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	    if (timer > 0)
            timer -= 1 * Time.deltaTime;
        if (timer <= 0)
            successPanel.SetActive(false);
	}
}
