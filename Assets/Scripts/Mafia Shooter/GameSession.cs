using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 0;
    [SerializeField] GameObject bigExplosion;
    [SerializeField] GameObject smallExplosion1;
    [SerializeField] GameObject smallExplosion2;

    private void Awake()
    {
        SetUpSingleton();
    }

    void Update()
    {
        if (score >= 5000) {
            GameObject explosion1 = Instantiate(bigExplosion, new Vector3(1.39f, -2.34f, 1), Quaternion.identity);
            GameObject explosion2 = Instantiate(smallExplosion1, new Vector3(7.91f,-2.89f,1), Quaternion.identity);
            GameObject explosion3 = Instantiate(smallExplosion2, new Vector3(-4.37f,-4.42f,1), Quaternion.identity);
            Destroy(explosion1, 3f);
            Destroy(explosion2, 2f);
            Destroy(explosion3, 2f);
            FindObjectOfType<Level>().LoadGameSuccess();
        }
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore(){
        return score;
    }

    public void AddToScore(int scoreValue){
        score += scoreValue;
    }

    public void ResetGame(){
        Destroy(gameObject);
    }
}
