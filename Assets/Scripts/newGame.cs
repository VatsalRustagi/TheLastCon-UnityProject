﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("moving left");
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("moving right");
        }
	}
}
