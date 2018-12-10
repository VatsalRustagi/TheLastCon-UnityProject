using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInMotel : MonoBehaviour
{
    [SerializeField] Image twinPlayer;
    [SerializeField] Text subtitles;
    [SerializeField] Image note;
    [SerializeField] Image enlargedNote;

    string[] dialogues = { "Connor: We did it! Thanks for helping out future me!",
        "Future Connor: We really pulled it off! I already knew we would but it still feels good",
        "Connor: Ok, stay here with the egg, I'm going to get some food and beer"};

    int index = 0;
    Animator twinAnimator;
    bool firstExit = false;
    bool hasEgg = false;
    bool secondExit = false;
    bool readNote = false;

	// Use this for initialization
	void Start () {
        twinAnimator = twinPlayer.GetComponent<Animator>();
        twinAnimator.SetBool("Walking", false);
        note.gameObject.SetActive(false);
        enlargedNote.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
    void Update () {
        if (hasEgg) {
            twinAnimator.SetBool("Walking", true);
            twinPlayer.transform.localScale = new Vector3(-1, 1, 1);
            twinPlayer.transform.Translate(-5f, 0f, 0f);

            if (twinPlayer.transform.position.x <= 50) {
                twinPlayer.gameObject.SetActive(false);
                note.gameObject.SetActive(true);
                subtitles.text = "Connor sees a note on the bed. Move towards the note to read it.";
                secondExit = true;
                hasEgg = false;
                firstExit = false;
            }

            return;
        }

        if (readNote && transform.position.x <= 50) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }

        if (secondExit) {
            GetComponent<UnityEngine.UI.Image>().transform.localScale = new Vector3(1, 1, 1);

            if(Mathf.Abs(transform.position.x - note.transform.position.x) <= 50) {
                if(!readNote) {
                    subtitles.text = "Press [Space Bar] to read the note";
                }

                if(Input.GetKeyUp(KeyCode.Space)) {
                    if(!readNote) {
                        enlargedNote.gameObject.SetActive(true);
                        readNote = true;
                    }
                    else {
                        enlargedNote.gameObject.SetActive(false);
                        subtitles.text = "Leave the room to chase after Kyle";
                    }
                        
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && index < dialogues.Length)
        {
            subtitles.text = dialogues[index++];

        }
        else if (index >= dialogues.Length && !firstExit && !secondExit) {
            GetComponent<UnityEngine.UI.Image>().transform.localScale = new Vector3(0, 0, 0);
            firstExit = true;
            subtitles.text = "...";
        }

        if(firstExit) {
            if (Mathf.Abs(twinPlayer.transform.position.x - Screen.width) > 350)
            {
                twinAnimator.SetBool("Walking", true);
                twinPlayer.transform.localScale = new Vector3(1, 1, 1);
                twinPlayer.transform.Translate(5f, 0f, 0f);
            }
            else
            {
                twinAnimator.SetBool("Walking", false);
                subtitles.text = "*Future Connor takes the egg*";
                hasEgg = true;
            }
        }
	}
}
