using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInMarieHouse : MonoBehaviour {

    [SerializeField] Image twinPlayer;
    [SerializeField] Image Marie;
    [SerializeField] Text subtitlesText;

    int index = 0;

    string[] subtitles = { "Future Connor: Wow you have a beautiful and big house!",
        "Marie: Oh thank you monsieur, let me show the painting I was talking about. Let's take the elevator upstairs",
        "Connor: Hi Marie, is there a washroom I could use on this floor?",
        "Marie: Yes monsieur, the bathroom is inside my bedroom, which the second door you see.",
        "Connor: Ah great, you both can continue upstairs. I'll join you guys briefly.",
        "*Future Connor & Marie step into the elevator*"};

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (index >= subtitles.Length) {
            Marie.gameObject.SetActive(false);
            twinPlayer.gameObject.SetActive(false);

            if (Mathf.Abs(Screen.width / 2 - transform.position.x) <= 100)
            {
                subtitlesText.text = "Press [Space Bar] to enter Marie's bedroom.";
            } else {
                subtitlesText.text = "Go to Marie's bedroom to execute the plan.";
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && index < subtitles.Length)
        {
            subtitlesText.text = subtitles[index];
            index++;
        } else if (Input.GetKeyUp(KeyCode.Space) && Mathf.Abs(Screen.width / 2 - transform.position.x) <= 100) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
    }
}
