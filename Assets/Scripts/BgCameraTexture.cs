using UnityEngine;
using System.Collections;

public class BgCameraTexture : MonoBehaviour {
#if !UNITY_STANDALONE_OSX && !UNITY_WEBGL
	public WebCamTexture webCamTexture;
#endif
    public int j;
    public GameObject wall;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject floor;
    public GameObject dancingFloor;
    public GameObject ceiling;
    public GameObject joan;
    public GameObject mia;
    public GameObject roy;
    private GUITexture myGUITexture;
    private Animator ani;
    void Awake()
    {
        //myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
    }
    // Use this for initialization
#if UNITY_WEBPLAYER
	IEnumerator Start() {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone)) {
        } else {
        }
#else
    void Start() {
#endif

#if !UNITY_STANDALONE_OSX && !UNITY_WEBGL
		webCamTexture = new WebCamTexture();
		GetComponent<GUITexture>().texture = webCamTexture;

#endif
        j=0;
        ani = mia.GetComponent("Animator") as Animator;
	}
	
	// Update is called once per frame
	void Update () {
        // Make sure the user pressed the mouse down
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
#if !UNITY_STANDALONE_OSX && !UNITY_WEBGL
                    if (WebCamTexture.devices.Length > 0)
                    {
                        webCamTexture.Stop();
                        j++;
                        if (j > 1)
                            j = 0;
                        webCamTexture.deviceName = WebCamTexture.devices[j].name;
                        webCamTexture.Play();
                    }
#endif
                    //Application.LoadLevel("MapScene");
                    break;
            }
        }


        if (Input.GetKeyUp(KeyCode.Z))
        {
#if !UNITY_STANDALONE_OSX && !UNITY_WEBGL
                if (WebCamTexture.devices.Length > 0)
                {
                    webCamTexture.Stop();
                    j++;
                    if (j > 1)
                       j = 0;
                    webCamTexture.deviceName = WebCamTexture.devices[j].name;
                        webCamTexture.Play();
                }
#endif
        }
        //if (Input.GetKeyUp(KeyCode.W))
        {
            //Application.LoadLevel("MapScene");
        }
        if (webcamcontrol.isEnterWebCam)
        {
#if !UNITY_STANDALONE_OSX && !UNITY_WEBGL
			webCamTexture.Play();
#endif
            wall.GetComponent<Renderer>().enabled = false;
            wall1.GetComponent<Renderer>().enabled = false;
            wall2.GetComponent<Renderer>().enabled = false;
            wall3.GetComponent<Renderer>().enabled = false;
            floor.GetComponent<Renderer>().enabled = false;
            ceiling.GetComponent<Renderer>().enabled = false;
            dancingFloor.GetComponent<Renderer>().enabled = false;
            //if (roy)
            //    GameObject.Destroy(roy);
            //if (joan)
            //    GameObject.Destroy(joan);
            //if (mia)
            //    GameObject.Destroy(mia);
        }
    }
    
    public Camera FindCamera ()
    {
        if (GetComponent<Camera>())
            return GetComponent<Camera>();
        else
            return Camera.main;
    }

}
