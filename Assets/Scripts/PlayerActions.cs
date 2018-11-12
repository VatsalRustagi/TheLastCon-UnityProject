using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour {

    [SerializeField] GameObject bottle;
    [SerializeField] Text textObj;
    [SerializeField] Image backgroundImage;
    bool bottleFell = false;
    // Use this for initialization
    void Start () {
        textObj.enabled = true;
    }

	// Update is called once per frame
	void Update () {
        var rt = (RectTransform)transform;
        var width = rt.rect.width;
        Debug.Log("Width" + width);
        // Checks whether the player is close to the bottle
        if (Mathf.Abs(bottle.transform.position.x - transform.position.x) < 50 
            + width/2 && bottleFell == false)
        {
            textObj.text = "You are now close to the bottle. " +
                "Press the space bar to push it down.";

            // If the player interacts with the bottle
            if (Input.GetKeyDown(KeyCode.Space))
            {
                textObj.text = "You have successfully completed the tutorial. " +
                    "Throughout the game you can interact with different objects " +
                    "and each interaction will affect the outcome of the game." +
                    "You can now exit this room through the door and start the game!";
                bottleFell = true;
                bottle.transform.Translate(100f, -1*bottle.transform.position.y + 15f, 0f);
                bottle.transform.Rotate(0f, 0f, -90f);
            }
        }
        else {
            textObj.text = "Welcome to The Last Con! This level " +
                "will take you through the basic controls in this game. " +
                "Use the right and left arrows to move about the room. " +
                "As you get close to some objects " +
                "you will have the option to interact with them";
        }

        Debug.Log(transform.position.x);

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x > Screen.width - width + 33
            && transform.position.x < Screen.width - 33 && bottleFell) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
	}
}
;