using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] PlayerController player;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] Hole hole;
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }
    private void Update()
    {
        if (hole.Entered && gameOverPanel.activeInHierarchy == false)
        {
            // pauseGame();
            gameOverPanel.SetActive(true);
            gameOverText.text = "Finished! Shoot Count : " + player.ShootCount;
        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
        // resumeGame();
    }

    public void Replay()
    {
        SceneLoader.ReloadLevel();
        // resumeGame();
    }

    public void PlayNext()
    {
        SceneLoader.LoadNextLevel();
        // resumeGame();
    }

    // public void pauseGame(){
    //     Time.timeScale = 0;
    // }

    // public void resumeGame(){
    //     Time.timeScale = 1;
    // }

}
