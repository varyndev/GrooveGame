using UnityEngine;
using Soomla.Store;

public class RestorePurchases : MonoBehaviour {

    [SerializeField]
    GameObject textGameObject;

    // event handler for restoring purchases
    public void RestoreSoomla()
    {
        SoomlaStore.RestoreTransactions();

        UnityEngine.UI.Text restoreText = textGameObject.GetComponent<UnityEngine.UI.Text>();

        restoreText.text = "Purchases Restored!";
    }
}
