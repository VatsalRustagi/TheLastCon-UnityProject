using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StorylineUpdates : MonoBehaviour {

    Text storyText;
    int index;
    bool continueGame;
    [SerializeField] Image otherCharacter;
    [SerializeField] Image mainCharacter;

    string[] storylines = { "The Last Con",
                           "Once upon a time, there was a man of great intelligence and cunning who made his living by simply knowing what people’s deepest darkest desires are and fulfilling those for them, with a catch of course.",
                           "He ran his confidence tricks with perfection and finesse for years until be stumbled upon a big stakes opportunity. High risk, but high reward.",
                           "Unbeknownst to him, he was about to get involved in something much bigger than himself.",
        "He conned his way into the wrong crowd and cut to where he is right now: on the run, from the El Chapo Mafia, who is demanding he return their money back; with interest.",
        "Press space to continue..."};
    float lastTime;
	// Use this for initialization
	void Start () {
        lastTime = Time.time;
        storyText = GetComponent<UnityEngine.UI.Text>();
        index = 0;
        storyText.text = storylines[index];
        continueGame = false;
        var animator = mainCharacter.GetComponent<Animator>();
        var animator2 = mainCharacter.GetComponent<Animator>();
        animator.SetBool("Walking", true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastTime > 2.0 && index < storylines.Length - 1) {
            storyText.text = storylines[++index];
            lastTime = Time.time;
        } else if (index == storylines.Length -1 && Input.GetKeyDown(KeyCode.Space) && continueGame == false) {
            // Continue to the next scene... The first motherfucking con
            storyText.text = "...";
            continueGame = true;
        }

        if (continueGame && Screen.width + 100 > mainCharacter.transform.position.x) {
            mainCharacter.transform.Translate(5f, 0f, 0f);
        } else if (continueGame && Screen.width + 100 > otherCharacter.transform.position.x) {
            storyText.text = "Suspicious man follows...";
            otherCharacter.transform.Translate(5f, 0f, 0f);
        } else if (continueGame) {
            Debug.Log("Next Scene");
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }


    }
}
