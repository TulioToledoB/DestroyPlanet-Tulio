using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;
    public GameObject planet;
    public TextMeshProUGUI points;
    public TextMeshProUGUI lifeRemaining;
    public AudioClip explotions;
    public AudioClip desactivate;
    public GameObject gameover;
    public MainMenu menu;
    public TextMeshProUGUI textscore;
    public AudioClip gameoversound;
    public AudioClip bestscoreshow;
    private bool hasBestScoreBeenBeaten = false;


    
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        score = 0;
        points.text = "Score: " + score;
        lifeRemaining.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void AddScore()
    {
        score++;
        points.text = "Score: " + score;
        CheckBestScore();
    }
   
    public void TakeDamage()
    {
        lives--;
        lifeRemaining.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }
   
    public void ExplotionsSounds()
    {
        GetComponent<AudioSource>().PlayOneShot(explotions);
    }
   
    public void BombDesactivate()
    {
        GetComponent<AudioSource>().PlayOneShot(desactivate);
    }

    public void ResetGame()
{
    
    lives = 3;
    score = 0;
    points.text = "Score: " + score;
    lifeRemaining.text = "Lives: " + lives;

}

    public void CheckBestScore()
    {
        int currentBestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > currentBestScore && !hasBestScoreBeenBeaten)
        {
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();

            
            menu.BestScoreDone();
            hasBestScoreBeenBeaten = true;
        }
    }
public void PlayAgain()
{
    score = 0;
    lives = 3;
    points.text = "Score: " + score;
    lifeRemaining.text = "Lives: " + lives;
    hasBestScoreBeenBeaten = false; 

    
    menu.bestscoreshow.SetActive(false);

    gameover.SetActive(false);
    menu.mainMenuPanel.SetActive(false);
    menu.gameUI.SetActive(true);
    
}
public void GoToMenu()
{
    gameover.SetActive(false);
    menu.gameUI.SetActive(false);
    menu.mainMenuPanel.SetActive(true);
    CheckBestScore();
    menu.SetBestScore();
}
 public void GameOver()
 {
     
    gameover.SetActive(true);
    menu.gameUI.SetActive(false);
    textscore.text = "Score: " + score;
    LoseSound();

} 
 public void LoseSound()
    {
       GetComponent<AudioSource>().PlayOneShot(gameoversound);
 }


}
