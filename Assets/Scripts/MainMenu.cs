using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject mainMenuPanel; 
    public GameObject gameUI; 
    public TextMeshProUGUI bestScoreText;
    public GameObject bestscoreshow;

    private void Start()
    {


        GoToMainMenu(); 
        SetBestScore();
       
    }

    public void PlayGame()
    {
       
        mainMenuPanel.SetActive(false); 
        gameUI.SetActive(true); 

        
        gameManager.ResetGame(); 

         bestscoreshow.SetActive(false);
    }

public void GoToMainMenu()
{
    mainMenuPanel.SetActive(true); 
    gameUI.SetActive(false); 
    gameManager.gameover.SetActive(false);
    bestscoreshow.SetActive(false); 

    SetBestScore();
}

    public void SetBestScore() {
    int bestScore = PlayerPrefs.GetInt("BestScore", 0);
    bestScoreText.text = "Best score: " + bestScore;
    bestscoreshow.SetActive(true);
}
public void BestScoreDone()
{
    bestscoreshow.SetActive(true); 
    gameManager.GetComponent<AudioSource>().PlayOneShot(gameManager.bestscoreshow);
}

}

