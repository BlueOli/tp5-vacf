using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text endGoodText;
    [SerializeField]
    private Text endText;

    [SerializeField]
    private GameObject endGameBox;

    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject retryButton;
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private GameObject quitButton;

    public GameObject player;
    public DifficultyManager difficultyManager;

    private PlayerHealth playerHealth;

    public void Start()
    {
        Time.timeScale = 1f;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        if(playerHealth.health <= 0)
        {
            DeathByRoomba();
        }
    }

    public void DoorReached()
    {
        Time.timeScale = 0f;
        int friendsAmount = player.GetComponent<PlayerInteraction>().friendsTagged;

        if (friendsAmount > 1)
        {
            endText.enabled = false;
            endGoodText.enabled = true;
            endGoodText.text = "You've escaped with " + friendsAmount + " friends.";
            retryButton.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            continueButton.SetActive(true);
            endGameBox.SetActive(true);
        }
        else if (friendsAmount == 1)
        {
            endText.enabled = false;
            endGoodText.enabled = true;
            endGoodText.text = "You've escaped with " + friendsAmount + " friend";
            retryButton.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            continueButton.SetActive(true);
            endGameBox.SetActive(true);
        }
        else
        {
            endText.enabled = true;
            endGoodText.enabled = false;
            endText.text = "You've left your friends behind...";
            retryButton.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            continueButton.SetActive(false);
            endGameBox.SetActive(true);
        }

        if(friendsAmount < difficultyManager.currentDifficulty)
        {
            endText.enabled = true;
            int friendsLeft = difficultyManager.currentDifficulty - friendsAmount;
            endText.text = "You've left " + friendsLeft + " friends behind...";
        }

        if (difficultyManager.currentDifficulty >= 9)
        {
            continueButton.SetActive(false);
        }
    }

    public void DeathByRoomba()
    {
        Time.timeScale = 0f;
        endText.enabled = true;
        endGoodText.enabled = false;
        endText.text = "You've been killed by a Roomba";
        retryButton.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);
        continueButton.SetActive(false);
        endGameBox.SetActive(true);
    }

    public void Restart()
    {
        difficultyManager.diffcultySO.difficulty = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        difficultyManager.diffcultySO.difficulty++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
