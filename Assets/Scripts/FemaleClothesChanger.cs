using UnityEngine;
using System.Collections;

public class FemaleClothesChanger : MonoBehaviour {

    //Lists for the clothing
    [SerializeField] GameObject [] shirts;
    [SerializeField] GameObject [] pants;
    [SerializeField] GameObject [] shoes;
    //Variables to control which screenshot is being used as a material
    public static int screenTexture;
    public ScreenshotController sCon;
    //Variable for texture number
    private string theScreenTex = "Screen Texture";
    private int screenTex;
    //Save variables for what material is active
    private string theShirtsMat = "ShirtsMat";
    private int shirtsMaterial;
    //Int to store what shirt is selected
    private string firstFShirt = "FemaleShirt";
    private int theFShirt;
    private string firstFPants = "FemalePants";
    private int theFPants;
    private string firstFShoes = "FemaleShoes";
    private int theFShoes;

     void Awake(){
        //If this is the first playthrough, creating variables for what clothes are equipped
        if (!PlayerPrefs.HasKey(firstFShirt))
             PlayerPrefs.SetInt(firstFShirt, 0);
        if (!PlayerPrefs.HasKey(firstFPants))
             PlayerPrefs.SetInt(firstFPants, 1);
        if (!PlayerPrefs.HasKey(firstFShoes))
             PlayerPrefs.SetInt(firstFShoes, 0);
        //If this is the first playthrough, creating variables for what materials are equipped
        if (!PlayerPrefs.HasKey(theShirtsMat))
            PlayerPrefs.SetInt(theShirtsMat, 0);
        if (!PlayerPrefs.HasKey(theScreenTex))
            PlayerPrefs.SetInt(theScreenTex, 10);
        //save the starting int
        theFShirt = PlayerPrefs.GetInt(firstFShirt);
        theFPants = PlayerPrefs.GetInt(firstFPants);
        theFShoes = PlayerPrefs.GetInt(firstFShoes);
        //save the starting int
        shirtsMaterial = PlayerPrefs.GetInt(theShirtsMat);
        screenTex = PlayerPrefs.GetInt(theScreenTex);
        PlayerPrefs.Save();
     }

    // Use this for initialization
    void Start(){
        shirts[theFShirt].GetComponent<SkinnedMeshRenderer>().enabled = true;
        pants[theFPants].GetComponent<SkinnedMeshRenderer>().enabled = true;
        shoes[theFShoes].GetComponent<SkinnedMeshRenderer>().enabled = true;
        screenTexture = screenTex;
        //Set the initial textures
        ChangeMaterial();
    }

    public void ChangeFShirt(int theNum) {
        screenTexture = 10;
        //Disable the current shirt
        shirts[theFShirt].GetComponent<SkinnedMeshRenderer>().enabled = false;
        //Change the list value, equip the new shirt, and save the shirt value
        theFShirt = theNum;
        shirts[theFShirt].GetComponent<SkinnedMeshRenderer>().enabled = true;
        ChangeMaterial();
        PlayerPrefs.SetInt(firstFShirt, theNum);
        PlayerPrefs.Save();
    }

    public void ChangeFPants(int theNum){
        //Disable the current pants
        pants[theFPants].GetComponent<SkinnedMeshRenderer>().enabled = false;
        //Change the list value, equip the new pants, and save the pants value
        theFPants = theNum;
        pants[theFPants].GetComponent<SkinnedMeshRenderer>().enabled = true;
        PlayerPrefs.SetInt(firstFPants, theNum);
        PlayerPrefs.Save();
    }

    public void ChangeFShoes(int theNum){
        //Disable the current shoes
        shoes[theFShoes].GetComponent<SkinnedMeshRenderer>().enabled = false;
        //Change the list value, equip the new shoes, and save the shoes value
        theFShoes = theNum;
        shoes[theFShoes].GetComponent<SkinnedMeshRenderer>().enabled = true;
        PlayerPrefs.SetInt(firstFShoes, theNum);
        PlayerPrefs.Save();
    }

    public void ChangeMaterial(){
        screenTex = screenTexture;
        PlayerPrefs.SetInt(theScreenTex, screenTexture);
        
        if (screenTexture <= 9){
            //Based on the int, set material to that screen shot and tile it
                shirts[theFShirt].GetComponent<SkinnedMeshRenderer>().material.mainTexture = sCon.screenShotTextures[screenTexture];
                shirts[theFShirt].GetComponent<SkinnedMeshRenderer>().material.mainTextureScale = new Vector2(1, 1);
                shirtsMaterial = screenTexture;
                PlayerPrefs.SetInt(theShirtsMat, screenTexture);
        }
        if (screenTexture > 9){
            //Sets the material back to the original material
                shirts[theFShirt].GetComponent<SkinnedMeshRenderer>().material.mainTexture = null;
                shirtsMaterial = screenTexture;
                PlayerPrefs.SetInt(theShirtsMat, screenTexture);
        } PlayerPrefs.Save();
    }
}
