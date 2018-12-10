using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanningTheCon : MonoBehaviour {

    string[] dialogues = {
        "Future Connor: Good job! We pulled it off, " +
        "now we have enough for the last con to get you out of this mess.",
        "Connor: So, do you have something planned already?",
        "Future Connor: Yes, I know a con that can clear your debt with the mafia but you're going to have to steal something in the end.",
        "Connor: Steal? No I don't do that, I'm just a confidence man! I'm no theif.",
        "Future Connor: Now is not the time to have a moral compass, we've been cheating people for years and I don't want to die because my past self " +
        "can't do what it takes!",
        "Connor: Ok Calm Down. I'll do it. So who's the mark and what are we stealing?",
        "Future Connor: The mark is a rich, single women by the name of Mariè Phillips who has a very rare Fabergé egg. I used the money from previous con to get us " +
        "some valuable information. I know everything we'll need to get into her house get the egg and disappear forever.",
        "Connor: So what's the first step now?",
        "Future Connor: My contacts told me that she's going to be at this new restaurant called Alinéa tomorrow night. " +
        "We'll 'bump' into her there and say that we're brothers who are in the art business and get her to invite us" +
        " back to her place to show off her art collection",
        "Connor: Sounds like a plan. Lets get cleaned up and head over to Alinéa for dinner tomorrow",
        "They both exit the scene"
    };
    int index = -1;
    bool conversationIsDone = false;

    [SerializeField] Text subtitles;
    [SerializeField] Image twinPlayer;
    [SerializeField] Image car;
    [SerializeField] Sprite carWithPeople;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(conversationIsDone && Screen.width + 25 > car.transform.position.x);
        if (Input.GetKeyUp(KeyCode.Space) && index < dialogues.Length - 1)
        {
            subtitles.text = dialogues[++index];
        }
        else if(index >= dialogues.Length - 1)
        {
            conversationIsDone = true;
            twinPlayer.gameObject.SetActive(false);
            GetComponent<UnityEngine.UI.Image>().transform.localScale = new Vector3(0, 0, 0);
            car.sprite = carWithPeople;
        }
        if(conversationIsDone && Screen.width > car.transform.position.x)
        {
            car.transform.Translate(5f, 0f, 0f);
        }
        else if(conversationIsDone) 
        {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
    }
}
