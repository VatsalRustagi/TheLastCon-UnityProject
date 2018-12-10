using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    float delayInSeconds = 1f;
    public int playerHealth;
    public int enemyHealth;

    public void LoadLevelSelectMenu() {
        SceneManager.LoadScene(15);
    }

    public void EasyGame() {
        playerHealth = 500;
        enemyHealth = 300;
        LoadGame();
    }

    public void MediumGame() {
        playerHealth = 400;
        enemyHealth = 400;
        LoadGame();
    }

    public void HardGame() {
        playerHealth = 300;
        enemyHealth = 400;
        LoadGame();
    }

    public void LoadGame() {
        SceneManager.LoadScene(16);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameSuccess() {
        FindObjectOfType<GameSession>().ResetGame();
        StartCoroutine(WaitAndLoad(18));
    }

    public void LoadGameLost() {
        StartCoroutine(WaitAndLoad(17));
    }

    IEnumerator WaitAndLoad(int sceneNumber) {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneNumber);
    }

    public int GetPlayerHealth(){
        return playerHealth;
    }

    public int GetEnemyHealth(){
        return enemyHealth;
    }
}
