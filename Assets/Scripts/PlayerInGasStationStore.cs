using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInGasStationStore : MonoBehaviour {

    [SerializeField] Image checkoutDesk;
    [SerializeField] Text subtitles;

    bool closeToHelpDesk = false;
    int index = -1;
    bool conIsSuccessful = false;

    string[] dialogues = {"Kyle: Hi!",
        "Clerk: Hi! How can I help you ?",
        "Kyle: I found this item on the floor, do you know whose it is?",
        "Clerk: Oh it's my friends"
    };

    // Use this for initialization
    void Start () {
        if(StaticGameData.didGetRing) 
        {
            conIsSuccessful = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        var width = GetComponent<UnityEngine.UI.Image>().sprite.rect.width;

        if (Mathf.Abs(transform.position.x - checkoutDesk.transform.position.x) <= width / 2)
        {
            if (!closeToHelpDesk)
            {
                subtitles.text = "You are close enough to talk to the clerk now. Press the space bar to begin the interaction and continue.";
                closeToHelpDesk = true;
            }
            if (Input.GetKeyUp(KeyCode.Space) && index < dialogues.Length - 1)
            {
                subtitles.text = dialogues[++index];
            }
            else if(index >= dialogues.Length - 1)
            {
                if(conIsSuccessful) 
                {
                    Debug.Log("Time to move onto planning!");
                }
                else
                {
                    // Loading the Parking lot scene again
                    SceneManager.LoadScene(4);
                }
            }
        }
        else 
        {
            closeToHelpDesk = false;
            subtitles.text = "Approach the clerk to start a conversation and complete the con.";
        }
    }
}
