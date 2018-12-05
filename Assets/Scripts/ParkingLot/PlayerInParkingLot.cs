using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInParkingLot : MonoBehaviour {

    [SerializeField] Button ring;
    [SerializeField] Button watch;
    [SerializeField] Button pearls;
    [SerializeField] Image car;
    [SerializeField] Sprite carTrunkOpen;
    [SerializeField] Sprite carTrunkClosed;
    [SerializeField] Text instructions;

    bool isTrunkOpen = false;

	// Use this for initialization
	void Start () {
        ring.gameObject.SetActive(false);
        watch.gameObject.SetActive(false);
        pearls.gameObject.SetActive(false);
        car.sprite = carTrunkClosed;
    }
	
	// Update is called once per frame
	void Update () {
        bool itemPickedUp = StaticGameData.didGetNecklace || StaticGameData.didGetRing || StaticGameData.didGetWatch;
        if (IsNearCar()) {
            if (!isTrunkOpen && (Input.GetKeyUp(KeyCode.Space)))
            {
                // Open the trunk here
                car.sprite = carTrunkOpen;
                ring.gameObject.SetActive(true);
                watch.gameObject.SetActive(true);
                pearls.gameObject.SetActive(true);
                Debug.Log("Opened Trunk");
                isTrunkOpen = true;
            }
            else if(!isTrunkOpen && !itemPickedUp) 
            {
                instructions.text = "You are close enough to the car to open the trunk. Press [Space Bar] to open the trunk";
            }
            else if (isTrunkOpen && !itemPickedUp)
            {
                instructions.text = "You have opened the trunk. Pick one of the 3 objects using the [Mouse] to use for the con";            
            }
            else if (isTrunkOpen && itemPickedUp)
            {
                instructions.text = "Now that you've picked the item, you can close the trunk by " +
                    "pressing [Space Bar] and head back to the gas station";

                if(Input.GetKeyUp(KeyCode.Space)) 
                {
                    isTrunkOpen = false;
                    car.sprite = carTrunkClosed;
                    instructions.text = "Head back to the gas station and meet up with your future self";
                }
            }
        }


        if (Screen.width - transform.position.x <= 10 && itemPickedUp) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
	}

    bool IsNearCar() {
        return Mathf.Abs(transform.position.x - car.transform.position.x) <= ((RectTransform) car.transform).rect.width / 2;
    }
}
