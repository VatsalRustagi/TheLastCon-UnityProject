using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabItem : MonoBehaviour {

    [SerializeField] Button[] buttons;

    public void ItemClicked(string id)
    {
        if (id == "ring") 
        {
            Debug.Log("Picked Ring");
            StaticGameData.didGetRing = true;
        } 
        else if (id == "watch")
        {
            Debug.Log("Picked Watch");
            StaticGameData.didGetWatch = true;
        } 
        else if (id == "pearls")
        {
            Debug.Log("Picked Pearls");
            StaticGameData.didGetNecklace = true;
        }
        foreach (Button button in buttons) {
            button.gameObject.SetActive(false);
        }
    }
}
