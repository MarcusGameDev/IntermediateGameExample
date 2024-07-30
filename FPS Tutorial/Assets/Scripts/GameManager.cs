using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HighScores highScores;

    // reference to the overlay text to display winning text, etc
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public GameObject highScorePanel;
    public TextMeshProUGUI highScoresText;

    public Button newGameButton;
    public Button highScoresButton;

    public TargetHealth[] targets;
    public GameObject player;
    public Camera worldCamera;

    public float startTimerAmount = 3;
    private float startTimer;

    public float targetActivatetimerAmount = 1;
    private float targetActivatetimer;

    public float gameTimerAmount = 20;
    private float gameTimer;

    private int score = 0;

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState gameState;
    public GameState State { get { return gameState; } }

    private void Awake()
    {
        gameState = GameState.GameOver;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        player.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        for(int i = 0; i < targets.Length; i++)
        {
            targets[i].GameManager = this;
            targets[i].gameObject.SetActive(false);
        }
        startTimer = startTimerAmount;
        messageText.text = "Press Enter to Start";
        timerText.text = "";
        scoreText.text = "";

        highScorePanel.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(true);
        highScoresButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        switch(gameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateGameOver();
                break;
        }


    }

    private void GameStateStart()
    {
        startTimer -= Time.deltaTime;

        messageText.text = "Get Ready " + (int)(startTimer + 1);

        if(startTimer < 0)
        {
          //  Cursor.lockState = CursorLockMode.Locked;
            messageText.text = "";
            gameState = GameState.Playing;
            gameTimer = gameTimerAmount;
            startTimer = startTimerAmount;
            score = 0;

            highScorePanel.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(false);
            highScoresButton.gameObject.SetActive(false);

            player.SetActive(true);
            worldCamera.gameObject.SetActive(false);
        }
    }

    private void GameStatePlaying()
    {
        gameTimer -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(gameTimer);
        timerText.text = string.Format("Time: {0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

        if(gameTimer <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            // Debug.Log("Game Over Score: " + score);
            messageText.text = "Game Over! Score: " + score;
            gameState = GameState.GameOver;
            player.SetActive(false) ;
            worldCamera.gameObject.SetActive(true);
            for(int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.SetActive(false);
            }
            highScores.AddScores(score);
            highScores.SaveScoresToFile();
            newGameButton.gameObject.SetActive(true);
            highScoresButton.gameObject.SetActive(true);
        }

        //timer before activating target.
        targetActivatetimer -= Time.deltaTime;
        if(targetActivatetimer <= 0)
        {
            ActivateRandomTarget();
            targetActivatetimer = targetActivatetimerAmount;
        }
    }

    private void GameStateGameOver()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            gameState = GameState.Start;
            timerText.text = "";
            scoreText.text = "";
        }
    }

    //Randomly activates a target.
    private void ActivateRandomTarget()
    {
        int randomIndex = Random.Range(0, targets.Length);
        targets[randomIndex].gameObject.SetActive(true);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void OnNewGame()
    {
        gameState = GameState.Start;
    }

    public void OnHighScores()
    {
        messageText.text = "";

        highScoresButton.gameObject.SetActive(false);
        highScorePanel.gameObject.SetActive(true);

        string text = "";
        for(int i = 0; i < highScores.scores.Length; i++)
        {
            text += highScores.scores[i] + "\n";
        }
        highScoresText.text = text;

    }
}
