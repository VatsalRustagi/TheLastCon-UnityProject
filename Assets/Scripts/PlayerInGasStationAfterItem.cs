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

    string[] dialogues = {"Kyle: Hey I got the item from my car!",
        "Stranger: Good! So here's the plan, lets do a pigeon drop with the item on" +
        " the clerk inside",
        "Kyle: Yeah sounds good! You make the call and I'll take car of 'finding' the item",
        "Stranger: Sounds like a plan! I'll make the call now"
    };

    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(-1, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        var rt = (RectTransform)transform;
        var width = GetComponent<UnityEngine.UI.Image>().sprite.rect.width;

        if(Mathf.Abs(transform.position.x - twinPlayer.transform.position.x) <= width / 2) 
        {
            if(!playersAreClose) 
            {
                subtitles.text = "You are now close enough to talk to your " +
                    "future self. Press the space bar to have a converstion.";
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
        else if(doneMakingTheCall) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            playersAreClose = false;
            subtitles.text = "Move towards your future self to talk and plan out the con.";
        }
    }
}
