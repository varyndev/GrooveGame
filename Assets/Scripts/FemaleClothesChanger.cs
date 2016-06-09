using UnityEngine;
using System.Collections;

public class FemaleClothesChanger : MonoBehaviour {

    //Lists for the clothing
    [SerializeField] GameObject [] shirts;
    [SerializeField] GameObject [] pants;
    [SerializeField] GameObject [] shoes;
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
        //save the starting int
        theFShirt = PlayerPrefs.GetInt(firstFShirt);
        theFPants = PlayerPrefs.GetInt(firstFPants);
        theFShoes = PlayerPrefs.GetInt(firstFShoes);
        PlayerPrefs.Save();
     }


    // Use this for initialization
    void Start(){
        //Equip the beginning clothes
        shirts[theFShirt].SetActive(true);
        pants[theFPants].SetActive(true);
        shoes[theFShoes].SetActive(true);
    }

    public void ChangeFShirt(int theNum) {
        //Disable the current shirt
        shirts[theFShirt].SetActive(false);
        //Change the list value, equip the new shirt, and save the shirt value
        theFShirt = theNum;
        shirts[theFShirt].SetActive(true);
        PlayerPrefs.SetInt(firstFShirt, theNum);
        PlayerPrefs.Save();
    }

    public void ChangeFPants(int theNum){
        //Disable the current pants
        pants[theFPants].SetActive(false);
        //Change the list value, equip the new pants, and save the pants value
        theFPants = theNum;
        pants[theFPants].SetActive(true);
        PlayerPrefs.SetInt(firstFPants, theNum);
        PlayerPrefs.Save();
    }

    public void ChangeFShoes(int theNum){
        //Disable the current shoes
        shoes[theFShoes].SetActive(false);
        //Change the list value, equip the new shoes, and save the shoes value
        theFShoes = theNum;
        shoes[theFShoes].SetActive(true);
        PlayerPrefs.SetInt(firstFShoes, theNum);
        PlayerPrefs.Save();
    }
}
