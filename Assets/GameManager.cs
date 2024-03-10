using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Single


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion

    public float currentScore = 0f;

    public Data data;

    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();

    public UnityEvent onGameOver = new UnityEvent();

    private ScoresDB scoresDB;

    public SpriteAnimation spriteAnimation;


    private void Start()
    {
        scoresDB = GetComponent<ScoresDB>();

        data = new Data();

    }

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }

    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        // Reset the current score for the next game
        currentScore = 0;
        spriteAnimation.StartAnimation();


    }

    public void GameOver()
    {
        

        if (data.highscore < currentScore)
        {
            data.highscore = currentScore;
        }

        int finalScore = Mathf.RoundToInt(data.highscore);

        // Insert the final score into the database
        scoresDB.InsertScore(finalScore);
        isPlaying = false;
        onGameOver.Invoke();
    }
    public string TidyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string TidyHighScore()
    {
        return Mathf.RoundToInt(data.highscore).ToString();
    }

    public void RetrieveScore()
    {
        // Retrieve score from SQLite database
        int score = scoresDB.GetLatestScore();
        Debug.Log("Latest score: " + score);
    }

}
