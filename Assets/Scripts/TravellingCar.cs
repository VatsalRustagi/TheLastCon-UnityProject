using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravellingCar : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -0.2f){
            transform.Translate(0, 1f*Time.deltaTime, 0);
            var oldScale = transform.localScale;
            transform.localScale = new Vector3(oldScale.x - 0.2f*Time.deltaTime, oldScale.y - 0.2f*Time.deltaTime, oldScale.z);
        } else {
            LoadFinalScreen();
        }
	}

    private void LoadFinalScreen()
    {
        SceneManager.LoadScene(19);
    }
}
