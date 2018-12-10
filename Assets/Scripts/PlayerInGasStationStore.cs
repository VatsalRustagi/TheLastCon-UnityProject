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

    string[] dialogues = {};

    string[] ringDialogues = {"Connor: Hi!",
        "Trish: Hi! How can I help you ?", 
        "Connor: Hi! I found this ring near one of the aisles and its look valuable",
        "Trish: Oh yes! A customer just called me. I said I would hold it for him till he gets here",
        "Connor: Oh good! So do I get some kind of reward for this?",
        "Trish: Well...there was a reward for it, guess we can split it.",
        "Connor: Here you go then!",
        "*Connor hands over the ring*",
        "Trish: Looks real, here's the money",
        "*Trish gets $2000 from the register*",
        "Connor: Thank you! Have a great day!",
        "You have successfully pulled off the con! Press [Space Bar] to move onto the next level", ""};

    string[] pearlDialogues = {"Connor: Hi!",
        "Trish: Hi! How can I help you ?",
        "Connor: Hi! I found these pearls near one of the aisles and its look valuable",
        "Trish: Oh yes! A customer just called me. I said I would hold it for him till he gets here",
        "Connor: Oh good! So do I get some kind of reward for this?",
        "Trish: Well...there was a reward for it, guess we can split it.",
        "Connor: Here you go then!",
        "*Connor hands over the pearls but they slip and fall down and break.*",
        "Trish: Looks like you found the wrong ones, sorry I can't help you",
        "The con failed because you picked the wrong item, press [Space Bar] to restart the level", ""};

    string[] watchDialogues = {"Connor: Hi!",
        "Trish: Hi! How can I help you ?",
        "Connor: Hi! I found this watch near one of the aisles and its look valuable",
        "Trish: Oh yes! A customer just called me. I said I would hold it for him till he gets here",
        "Connor: Oh good! So do I get some kind of reward for this?",
        "Trish: Well...there was a reward for it, guess we can split it.",
        "Connor: Here you go then!",
        "*Connor hands over the watch*",
        "Trish: Hmm why does it say Relex on it buddy. Think you found a fake Rolex, unfortunately I can't give you the money",
        "The con failed because you picked the wrong item, press [Space Bar] to restart the level", ""};

    // Use this for initialization
    void Start () {
        if(StaticGameData.didGetRing) 
        {
            conIsSuccessful = true;
            dialogues = ringDialogues;
        }
        if(StaticGameData.didGetNecklace) {
            dialogues = pearlDialogues;
        }
        else if(StaticGameData.didGetWatch) {
            dialogues = watchDialogues;
        }
	}
	
	// Update is called once per frame
	void Update () {
        var width = GetComponent<UnityEngine.UI.Image>().sprite.rect.width;

        if (Mathf.Abs(transform.position.x - checkoutDesk.transform.position.x) <= width / 2 + 50)
        {
            if (!closeToHelpDesk)
            {
                subtitles.text = "You are close enough to talk to the clerk now. Press the [Space Bar] to begin the interaction and continue.";
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
                    var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
                    SceneManager.LoadScene(nextIndex);

                }
                else
                {
                    // Loading the Parking lot scene again
                    StaticGameData.didGetNecklace = false;
                    StaticGameData.didGetRing = false; 
                    StaticGameData.didGetWatch = false;
                    SceneManager.LoadScene(4);
                }
            }
        }
        else if(index < dialogues.Length - 1)
        {
            closeToHelpDesk = false;
            subtitles.text = "Approach the clerk to start a conversation and complete the con.";
        }
    }
}
