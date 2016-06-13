using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class ScreenshotController : MonoBehaviour {

    //Track what screenshot number it is
    private string screenShotNum = "screenShotNum";
    private int screenShot;
    //List for screenshot numbers
    int[] screenShotIndex;
    public List<Texture2D> screenShotTextures;

    void Awake(){
        //If this is the first playthrough, creating variables for what number screenshot it is
        if (!PlayerPrefs.HasKey(screenShotNum))
            PlayerPrefs.SetInt(screenShotNum, 0);

        screenShot = PlayerPrefs.GetInt(screenShotNum);
        PlayerPrefs.Save();
    }

    void Start()
    {
        LoadScreenShots();
    }

   public void ScreenShot(){
        //Take a screenshot on button press
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            Application.CaptureScreenshot(Application.persistentDataPath + "/Screenshot" + screenShot + ".png");
            SaveScreenShot();
        }
        else {
            Application.CaptureScreenshot(Application.dataPath + "/Screenshot" + screenShot + ".png");
            SaveScreenShot();
        }

        //LoadScreenShots();
        //Incremenet the screenshot number
        TrackIndex();
    }

    //Uses current screenshot int to create a save name and cycles to the beginning after the max number has been reached
    void TrackIndex(){
        //Check if the max number of screenshots has been reached, if not, then increase the screen shot number
        if (screenShot <= 8)
            screenShot++;
        //If the max has been reached, then reset the screenshot numbers
        else screenShot = 0;

        PlayerPrefs.SetInt(screenShotNum, screenShot);
        PlayerPrefs.Save();
    }

    void SaveScreenShot(){
        //Make screenshot a texture
        var texture = new Texture2D(1, 1);
        //Texture becomes a .png file
        var bytes = texture.EncodeToPNG();
        //Write file to system
        if (Application.platform != RuntimePlatform.WindowsEditor)
            File.WriteAllBytes(Application.persistentDataPath + "/Screenshot" + screenShot + ".png", bytes);
        else File.WriteAllBytes(Application.dataPath + "/Screenshot" + screenShot + ".png", bytes);
    }

    void LoadScreenShots(){
        //for loop to run through each option for each screenshot
        for (int i = 0; i <= 9; i++){
            byte[] file;
            Texture2D tex = new Texture2D(1, 1);
            //check which platform game is being run on to decide how searching will occur
            if (Application.platform != RuntimePlatform.WindowsEditor){
                //If there is a file located at the following path
                if (File.ReadAllBytes(Application.persistentDataPath + "/Screenshot" + i + ".png") != null){
                    //if there is a file, set it into the byte file
                    file = File.ReadAllBytes(Application.persistentDataPath + "/Screenshot" + i + ".png");

                    //if there is already something located in that position of the list, delete it and add this new texture
                    if (tex.LoadImage(file) != false){
                        if (screenShotTextures[i] != null){
                            screenShotTextures.RemoveAt(i);
                            screenShotTextures.Insert(i, tex);
                        }
                        else screenShotTextures.Insert(i, tex);
                    }else return;
                }
            }
            else{
                if (File.ReadAllBytes(Application.dataPath + "/Screenshot" + i + ".png") != null){
                    file = File.ReadAllBytes(Application.dataPath + "/Screenshot" + i + ".png");

                    if (tex.LoadImage(file) != false){
                        if (screenShotTextures[i] != null){
                            screenShotTextures.RemoveAt(i);
                            screenShotTextures.Insert(i, tex);
                            Debug.Log("We removed the old and added the new");
                        }
                        else screenShotTextures.Insert(i, tex);
                    } else return;
                }else return;
            }
        }
    }
}
