using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;

    public float score=0;
    public float totalscore;
    public Text scoreText;

    public float damagerate=0;
    public float liveEnemy=0;
    public Text damagerateText;
    
    public GameObject titleScreen;
    public GameObject gameOverScr;
    public GameObject winScr;
    private GameObject spawnCtr;
    public GameObject damageScr;

    public int timeLeft = 232; //Seconds Overall
    public Text countdown; 

    public int timeLeftStart = 3; 
    public Text countdownStart; 

    public Text winScore;
    public Text winKillRate;
    public Text wintotalScore;

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
        if (currentLives < 3)
        {
            StartCoroutine("damage");
        }
    }

    public void UpdateScore()
    {
        score ++ ;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLiveEnemy()
    {
        liveEnemy++;        
    }
    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
    }
    public void ShowGameOverScreen()
    {
        gameOverScr.SetActive(true);
    }
    public void ShowWinScreen()
    {
        winScr.SetActive(true);
    }
    void Start()
    {
        StartCoroutine("LoseTime");
        StartCoroutine("LoseTimeStart");
        Time.timeScale = 1;
    }

    private void Update()
    {
        damagerate = (100f * (score - liveEnemy) / score);
        damagerateText.text = "Kill rate: " + damagerate.ToString("0.0") + "%";
        countdown.text = ("Stay alive: " + timeLeft + "s");
        countdownStart.text = (" " + timeLeftStart + " ");
        spawnCtr = GameObject.FindGameObjectWithTag("spawn");
        if (timeLeft==0)
        {
            ShowWinScreen();
            Destroy(spawnCtr);
            winScore.text = "Your Score: " + score;
            winKillRate.text = "Kill rate: " + damagerate.ToString("0.0") + "%";
            totalscore = score * (100f * (score - liveEnemy) / score);
            wintotalScore.text = "Total score: " + totalscore;
            Time.timeScale = 0.0f;
        }
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    IEnumerator LoseTimeStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeftStart--;
        }
    }
    IEnumerator damage()
    {
        damageScr.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        damageScr.SetActive(false);
    }
}
