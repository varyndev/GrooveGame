using UnityEngine;
using System.Collections;

public class ClothesScript : MonoBehaviour {

    [SerializeField] GameObject theHairs;
    [SerializeField] GameObject theShirts;
    [SerializeField] GameObject thePants;
    [SerializeField] GameObject theShoes;

    public void OpenHairs()
    {
        theHairs.SetActive(true);
        theShirts.SetActive(false);
        thePants.SetActive(false);
        theShoes.SetActive(false);
    }

    public void OpenShirts()
    {
        theHairs.SetActive(false);
        theShirts.SetActive(true);
        thePants.SetActive(false);
        theShoes.SetActive(false);
    }

    public void OpenPants()
    {
        theHairs.SetActive(false);
        theShirts.SetActive(false);
        thePants.SetActive(true);
        theShoes.SetActive(false);
    }

    public void OpenShoes()
    {
        theHairs.SetActive(false);
        theShirts.SetActive(false);
        thePants.SetActive(false);
        theShoes.SetActive(true);
    }
}
