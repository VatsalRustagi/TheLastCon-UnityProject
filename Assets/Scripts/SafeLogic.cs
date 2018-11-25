using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeLogic : MonoBehaviour {
    [SerializeField] Text code;
    [SerializeField] Button[] buttons;
    [SerializeField] Image egg;

    string[] fields = { "_", "_", "_", "_" };
    string[] keypadNums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
    string codeToOpen = "3 4 7 3";

    // Use this for initialization
    void Start () {
        for (int i = 0; i < keypadNums.Length; i++) {
            Debug.Log(i);
            Button button = buttons[i];
            string keyPadNum = keypadNums[i];
            button.onClick.AddListener(() => updateCode(keyPadNum));
        }

        egg.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space) && egg.IsActive()) {

        }
	}

    void updateCode(string num) {
        Debug.Log("Here");
        Debug.Log(num);
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].Equals("_")) {
                fields[i] = num;
                break;
            }
        }

        code.text = string.Join(" ", fields);
    }

    public void clearCode() {
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i] = "_";
        }
        code.text = "_ _ _ _";
    }

    public void checkCode()
    {
        if(codeToOpen.Equals(code.text)) {
            egg.gameObject.SetActive(true);
        }
        else {
            clearCode();
        }
    }
}
