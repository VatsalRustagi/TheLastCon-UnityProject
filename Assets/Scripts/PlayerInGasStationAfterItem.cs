using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInGasStationAfterItem : MonoBehaviour {

    [SerializeField] Text subtitles;
    [SerializeField] Image twinPlayer;

    int index = -1;
    bool playersAreClose = false;
    bool doneMakingTheCall = false;


    string[] dialogues = {"Connor: Hey I got the item from your car!",
        "Future Connor: Good! So here's the plan, I'll call the clerk inside and " +
        "tell her that I lost a valuable item in the store and that I'll reward her if she can find it",
        "Connor: Ok sounds good, then I'll head inside and tell her that I found it and get some money for it",
        "Future Connor: Sounds like a plan! I'll make the call now",
        "*Phone Ringing*",
        "Clerk: Hello, this is Trish, How may I help you?",
        string.Format("Future Connor: Hi, my name is Jim and I lost my {0} in the store. " +
                      "Could you please find it if possible, I'll give you $4000 if you do", StaticGameData.itemGrabbed),
        "Clerk: Yes! I'll look around and hold it for you if I find it",
        "*Ends call*",
        "Future Connor: Ok we're set!",
        "Go inside the store to finish the con"
    };

    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(-1, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        var rt = (RectTransform)transform;
        var width = GetComponent<UnityEngine.UI.Image>().sprite.rect.width;

        if(Mathf.Abs(transform.position.x - twinPlayer.transform.position.x) <= width / 2 + 50 && !doneMakingTheCall) 
        {
            if(!playersAreClose) 
            {
                subtitles.text = "You are now close enough to talk to your " +
                    "future self. Keep pressing the [Space Bar] slowly to have a converstion.";
                playersAreClose = true;
            }

            if (Input.GetKeyUp(KeyCode.Space) && index < dialogues.Length - 1)
            {
                subtitles.text = dialogues[++index];
            }
            else if(index >= dialogues.Length - 1)
            {
                doneMakingTheCall = true;
            }
        }
        else if(doneMakingTheCall && Screen.width - transform.position.x <= 350) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
        else if(!doneMakingTheCall)
        {
            playersAreClose = false;
            subtitles.text = "Move towards your future self to talk and plan out the con.";
        }
    }
}
