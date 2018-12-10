using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInGasStation : MonoBehaviour {

    [SerializeField] Image twinPlayer;
    [SerializeField] Text subtitles;
    Animator twinPlayerAnimator;

    string[] dialogues = {"Connor: Are you talkin' to me?",
        "Stranger: Yeah, you! You need my help",
        "Connor: I don't need anybody's help. Who're you?",
        "Stranger: Knowing myself, you aren't going to believe me but...",
        "Stranger: ...I'm you from the future!",
        "Connor: HAHAHAH nice try buddy, you think I'm that dumb?",
        "Stranger: I know you messed and now you owe the mafia money, else they're going to kill you",
        "Connor: Anybody could've found that out, you're really bad at this",
        "Stranger: I know you don't trust anybody ever since your parents abandoned you",
        "Connor: wait...how'd you know that?",
        "Stranger: Like I said, I'm you from the future. I'm here you help you out of this sticky situation with the mafia",
        "Connor: But why would you help me?",
        "Stranger: Because if you die...I DIE",
        "Connor: Ok lets say you're me from the future, what do you propose we do?",
        "Future Connor: I know for a fact that you're out of money and " +
        "we're going to need some startup money to plan a big con",
        "Connor: So you want to pull a con now and make some quick money?",
        "Future Connor: Yeah, I was thinking we could do a pigeon drop on the clerk inside",
        "Connor: That would work if I had jewellery on me.",
        "Future Connor: It's all good. I'm always prepared, I've got some stuff in my car's trunk. Go and pick something from it.",
        "Move to the left to exit the scene and head to the parking lot"
    };

    int index = -1;
    bool playersMet = false;

	// Use this for initialization
	void Start () {
        playersMet = false;
        twinPlayerAnimator = twinPlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        var width = GetComponent<UnityEngine.UI.Image>().sprite.rect.width;

        if (!playersMet && transform.position.x - twinPlayer.transform.position.x >= width / 2 + 50)
        {
            Debug.Log("Player is moving");
            twinPlayer.transform.Translate(5f, 0f, 0f);
            twinPlayerAnimator.SetBool("Walking", true);
        }
        else if (!playersMet)
        {
            playersMet = true;
            twinPlayerAnimator.SetBool("Walking", false);
            transform.localScale = new Vector3(-1, 1, 1);
            subtitles.text = dialogues[++index];
        }

        if (Input.GetKeyUp(KeyCode.Space) && playersMet && index < dialogues.Length - 1)
        {
            subtitles.text = dialogues[++index];
        }

        if (DidEndSubtitles() && DidReachEndOfScreen())
        {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
    }

    bool DidEndSubtitles() {
        if (index == dialogues.Length - 1) {
            return true;
        }
        return false;
    }

    bool DidReachEndOfScreen() {
        return transform.position.x <= 0;
    }
}
