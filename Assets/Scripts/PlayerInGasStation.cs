using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInGasStation : MonoBehaviour {

    [SerializeField] Image twinPlayer;
    [SerializeField] Text subtitles;
    Animator twinPlayerAnimator;

    string[] dialogues = {"Kyle: You talking to me?",
        "Stranger: Yes, you!",
        "Kyle: Who are you? What do you want from me?",
        "Stranger: I'm you from the future!",
        "Kyle: So according to your plan we need to find the necklace"
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
