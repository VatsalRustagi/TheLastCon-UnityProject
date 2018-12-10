using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTheLastConBedroom : MonoBehaviour {

    [SerializeField] Text instructions;
    [SerializeField] Image bed;
    [SerializeField] Image closet;
    [SerializeField] Image note;
    [SerializeField] Sprite openCloset;

    // Use this for initialization
    void Start () {
        if (StaticGameData.didGetEgg)
        {
            closet.sprite = openCloset;
        }
        note.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(StaticGameData.didGetEgg) {
            instructions.text = "You have the egg now, exit the room";
            if(transform.position.x <= 50) {
                SceneManager.LoadScene(13);
            }
            return;
        }

        if(isNearNote()) {
            instructions.text = "Press [Space Bar] to read note";
            if(Input.GetKeyUp(KeyCode.Space)) {
                note.gameObject.SetActive(!note.IsActive());
            }
        } else if (isNearCloset())
        {
            if (closet.sprite == openCloset) {
                instructions.text = "Press [Space Bar] to interact with the safe";
            } else {
                instructions.text = "Press [Space Bar] to interact with the closet";
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if(closet.sprite == openCloset) {
                    var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
                    SceneManager.LoadScene(nextIndex);
                }
                closet.sprite = openCloset;
            }
        }
        else
        {
            instructions.text = "Explore the room for clues...";
        }
    }

    bool isNearNote() {
        return (int)Mathf.Abs(transform.position.x - bed.transform.position.x) <= 30;
    }

    bool isNearCloset()
    {
        return (int)Mathf.Abs(transform.position.x - closet.transform.position.x) <= 30;
    }
}
