using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < Screen.width)
        {
            animator.SetBool("Walking", true);
            var direction = 1; //  or -1, depending which way you want the sprite pointing.
            // Sets the local scale of the current GameObject
            transform.localScale = new Vector3(direction, 1, 1);
            transform.Translate(5f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > 0)
        {
            animator.SetBool("Walking", true);
            var direction = -1; //  or -1, depending which way you want the sprite pointing.
            // Sets the local scale of the current GameObject
            transform.localScale = new Vector3(direction, 1, 1);
            transform.Translate(-5f, 0f, 0f);
        } else {
            animator.SetBool("Walking", false);
        }
    }
}
