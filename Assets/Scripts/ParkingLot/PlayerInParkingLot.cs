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
            if (!isTrunkOpen && !itemPickedUp && (Input.GetKeyUp(KeyCode.Space)))
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
                instructions.text = "Press [Space Bar] to open the trunk";
            }
            else if (isTrunkOpen && !itemPickedUp)
            {
                instructions.text = "You have opened the trunk. Pick one of the 3 objects using the [Mouse] to use for the con";            
            }
            else if (isTrunkOpen && itemPickedUp)
            {
                instructions.text = "Now that you've picked the item, head back to the gas station";
                isTrunkOpen = false;
                car.sprite = carTrunkClosed;

            }
        }
        else if(!itemPickedUp) {
            instructions.text = "Walk towards the car to grab an item to do the pigeon drop con";
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
