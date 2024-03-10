using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Single


    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    #endregion

    public float currentScore = 0f;

    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();

    public UnityEvent onGameOver = new UnityEvent();

    private ScoresDB scoresDB;

     private void Start() {
         scoresDB = GetComponent<ScoresDB>();
     }

    private void Update() {
        if (isPlaying) {
            currentScore += Time.deltaTime;
        }
      
    }

    public void StartGame() { 
        onPlay.Invoke();
        isPlaying = true;
    }

    public void GameOver() {
        onGameOver.Invoke();
        currentScore = 0;
        isPlaying = false;

        int score = Mathf.RoundToInt(currentScore);
        scoresDB.InsertScore(score);
    }
    public string TidyScore () {
        return Mathf.RoundToInt(currentScore).ToString();
    }

   public void RetrieveScore()
    {
        // Retrieve score from SQLite database
        int score = scoresDB.GetLatestScore();
        Debug.Log("Latest score: " + score);
    }

}
