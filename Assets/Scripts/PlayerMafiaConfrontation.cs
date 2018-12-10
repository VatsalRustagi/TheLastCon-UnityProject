using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMafiaConfrontation : MonoBehaviour {

    [SerializeField] Text subtitles;

    string[] dialogues = {"Mobster: We want the money now!",
        "Connor: Ok look I had the money but my brother stole. Give me some more time",
        "Mobster: You had your time, you're gonna die now",
    "You will now play a mini arcade shooting game against the mobsters. Use the arrows to move and [Space Bar] to shoot", ""};

    int index = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyUp(KeyCode.Space) && index < dialogues.Length) {
            subtitles.text = dialogues[index++];
        }
        else if(index >= dialogues.Length) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
        
	}
}
