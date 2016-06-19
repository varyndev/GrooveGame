using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotCanvas : MonoBehaviour {

    public int theShot;
    public FemaleClothesChanger femC;

    public void ScreenClicked(){
        FemaleClothesChanger.screenTexture = theShot;
        femC.ChangeMaterial();
    }
}
