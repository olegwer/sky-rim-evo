using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    public GameObject spawnCtr;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        Time.timeScale = 1;
        if (gameOver == true)
        {
            Invoke("startGame", 3);
        }
    }

   
    public void ApplicationQuit()
    {
        Application.Quit();
    }
    public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Application.LoadLevel("GS_v01");
    }
    void startGame()
    {
       
        Instantiate(player, Vector3.zero, Quaternion.identity);
        Instantiate(spawnCtr);
        gameOver = false;
        _uiManager.HideTitleScreen();
    }
}
