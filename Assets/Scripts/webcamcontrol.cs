using UnityEngine;
using System.Collections;

public class webcamcontrol : MonoBehaviour {
    static public bool isEnterWebCam=false;
    static public bool isInsideWebCam=false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!isEnterWebCam)
        {
            if (transform.position.x < -10 &&
                transform.position.x > -13 &&
                transform.position.y > 1 &&
                transform.position.y < 2 &&
                transform.position.z < -1.5 &&
                transform.position.z > -4)
            {
                isEnterWebCam = true;
                Application.LoadLevel("MapScene");
            }
        }
        if (isInsideWebCam)
        {
            if (transform.position.x < -10 &&
                transform.position.x > -13 &&
                transform.position.y > 1 &&
                transform.position.y < 2 &&
                transform.position.z < -1.5 &&
                transform.position.z > -4)
            {
                isEnterWebCam = false;
                isInsideWebCam = false;
                Application.LoadLevel("ClubHouse");
            }
        }
        if (!isInsideWebCam && isEnterWebCam)
        {
            if (transform.position.x > -10 ||
                transform.position.x < -13 ||
                transform.position.y < 1 ||
                transform.position.y > 2 ||
                transform.position.z > -1.5 ||
                transform.position.z < -4)
            {
                isInsideWebCam = true;
            }
        }
    }
}
