using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class ScreenshotController : MonoBehaviour {

    //Track what screenshot number it is, based on values 0-9
    private string screenShotNum = "screenShotNum";
    private int screenShot;
    //Track what screenshot is being used as the texture, based on player selection
    private string loadScreenNum = "Load Screen Number";
    private int loadNum;
    //Array for screenshot numbers selected within the game
    int[] screenShotIndex;
    //List to hold the screenshots after then are converted to textures
    public List<Texture2D> screenShotTextures;
    //Rect to determine the size of the taken screenshot
    public Rect capRect;
    //Main camera of the scene
    public Camera cam;

    void Awake(){
        //If this is the first time playing, Unity will populate these variables with default values
        if (!PlayerPrefs.HasKey(screenShotNum))
            PlayerPrefs.SetInt(screenShotNum, 0);
        if (!PlayerPrefs.HasKey(loadScreenNum))
            PlayerPrefs.SetInt(loadScreenNum, -1);
        //We then set the variables equal to the saved values and save the values to Unity
        screenShot = PlayerPrefs.GetInt(screenShotNum);
        loadNum = PlayerPrefs.GetInt(loadScreenNum);
        /*screenShot = 0;
        loadNum = -1;
        PlayerPrefs.SetInt(screenShotNum, screenShot);
        PlayerPrefs.SetInt(loadScreenNum, loadNum);*/
        PlayerPrefs.Save();
    }

    void Start(){
        //At the start we load any previously taken textures into the texture list
        LoadScreenShots();
        //Set the capture range of the screenshot and set the resolution as 256 x 256
        capRect.xMin = (Screen.width - 256) / 2; 
        capRect.yMin = (Screen.height - 256) / 2;
        capRect.width = 256;
        capRect.height = 256;
    }

   public void ScreenShot(){
        //Start the screenshot process and tell the script to run the SaveScreenShot() function
        if (Application.platform != RuntimePlatform.WindowsEditor){
            StartCoroutine(SaveScreenShot());
        }
        else {
            StartCoroutine(SaveScreenShot());
        }
    }

    //Uses current screenshot integer to create a save name for the screenshot
    void TrackIndex(){
        //Check if the max number of screenshots has been reached, if not, then increase the screen shot number
        if (screenShot <= 8)
            screenShot++;
        //If the max has been reached, then reset the screenshot numbers
        else screenShot = 0;
        //If there have been less than 9 screenshots saved, then we increase the number of textures to load
        if (loadNum < 9)
            loadNum++;
        //We then save all values to Unity's system
        PlayerPrefs.SetInt(screenShotNum, screenShot);
        PlayerPrefs.SetInt(loadScreenNum, loadNum);
        PlayerPrefs.Save();
    }
    
    public IEnumerator SaveScreenShot(){
        yield return new WaitForEndOfFrame();
        //We set the resolution scaling number. This will increase the resolution of our screenshot
        int resoFactor = 2;

        //Make screenshot a texture based on the rect size we specified earlier
        Texture2D tex = new Texture2D((int)capRect.width * resoFactor, (int)capRect.height * resoFactor, TextureFormat.ARGB32, false);
        //Increase the resolution of the original capture to make the image bigger and better looking
        RenderTexture texRen = new RenderTexture(Screen.width * resoFactor, Screen.height * resoFactor, 24, RenderTextureFormat.ARGB32);
        RenderTexture.active = texRen;
        //Tell the main camera what texture to look at and render
        cam.targetTexture = texRen;
        cam.Render();
        //Create a new Rect based on the dimensions of the old rect x the resolution integer
        Rect capRect2 = new Rect(capRect.xMin * resoFactor, capRect.yMin * resoFactor, capRect.width * resoFactor, capRect.height * resoFactor);
        //We then tell the texture to capture the pixles within the new rect's bounds
        tex.ReadPixels(capRect2, 0, 0);
        //Then apply the new bounds to the texture
        tex.Apply();

        //The texture is converted into a .png file for saving
        var bytes = tex.EncodeToPNG();
        //We then reset the values of the camera and texture above to prepare for a new screenshot later
        RenderTexture.active = null;
        cam.targetTexture = null;
        //Save the texture to the Unity system based on the platform the game is being played on
        if (Application.platform != RuntimePlatform.WindowsEditor)
            File.WriteAllBytes(Application.persistentDataPath + "/Screenshot" + screenShot + ".png", bytes);
        else File.WriteAllBytes(Application.dataPath + "/Screenshot" + screenShot + ".png", bytes);

        //Increase the number of screenshots the player has taken
        TrackIndex();
    }

    void LoadScreenShots(){
        //for loop to add each taken screenshot to a list the player can choose from
        for (int i = 0; i <= loadNum; i++){
            byte[] file;
            //We make sure the texture is a 1x1 file
            Texture2D tex = new Texture2D(1, 1);
            //check which platform game is being run on to decide how searching will occur
            //If the platform is not the Unity editor...
            if (Application.platform != RuntimePlatform.WindowsEditor){
                //Check if there is a file located at the following path within Unity's game folders
                if (File.ReadAllBytes(Application.persistentDataPath + "/Screenshot" + i + ".png") != null){
                    //if there is a file, set it into the byte file
                    file = File.ReadAllBytes(Application.persistentDataPath + "/Screenshot" + i + ".png");

                    //We then load the texture located at the byte file we just specified
                    //We check if there is a texture located at the specified location
                    if (tex.LoadImage(file) != false){
                        //if there is a texture located at the specified location
                        //we check the list to make sure we are not overwriting a texture already in the list
                        if (screenShotTextures[i] != null){
                            //If there is already a texture at this spot in the list, we remove it and add the new texture
                            screenShotTextures.RemoveAt(i);
                            screenShotTextures.Insert(i, tex);
                        }
                        //If there is not already a texture in this spot on the list, then we simply add the texture to the list
                        else screenShotTextures.Insert(i, tex);
                    }else return;
                }
            }
            //If the platform is the Unity editor
            else{
                //Check if there is a file located at the following path within Unity's game folders
                if (File.ReadAllBytes(Application.dataPath + "/Screenshot" + i + ".png") != null){
                    //if there is a file, set it into the byte file
                    file = File.ReadAllBytes(Application.dataPath + "/Screenshot" + i + ".png");

                    //We then load the texture located at the byte file we just specified
                    //We check if there is a texture located at the specified location
                    if (tex.LoadImage(file) != false){
                        //if there is a texture located at the specified location
                        //we check the list to make sure we are not overwriting a texture already in the list
                        if (screenShotTextures[i] != null){
                            //If there is already a texture at this spot in the list, we remove it and add the new texture
                            screenShotTextures.RemoveAt(i);
                            screenShotTextures.Insert(i, tex);
                        }
                        //If there is not already a texture in this spot on the list, then we simply add the texture to the list
                        else screenShotTextures.Insert(i, tex);
                    } else return;
                }else return;
            }
        }
    }
}
