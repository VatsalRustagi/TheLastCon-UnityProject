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

    bool isTrunkOpen = false;

	// Use this for initialization
	void Start () {
        ring.gameObject.SetActive(false);
        watch.gameObject.SetActive(false);
        pearls.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        bool itemPickedUp = StaticGameData.didGetNecklace || StaticGameData.didGetRing || StaticGameData.didGetWatch;
        if (IsNearCar() && Input.GetKeyUp(KeyCode.Space) && !isTrunkOpen) {
            // Open the trunk here
            ring.gameObject.SetActive(true);
            watch.gameObject.SetActive(true);
            pearls.gameObject.SetActive(true);
            Debug.Log("Opened Trunk");
            isTrunkOpen = true;
        }

        if (transform.position.x <= 5.0 && itemPickedUp) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
	}

    bool IsNearCar() {
        return Mathf.Abs(transform.position.x - car.transform.position.x) <= 100;
    }
}
