using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI GameOverScoreUI;
    [SerializeField] private TextMeshProUGUI HighScoreTextUI;
    [SerializeField] private TextMeshProUGUI HighScoresTable;

    [SerializeField] private GameObject StartUI;

    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject HighscoresTableUI;

    GameManager gm;
    private ScoresDB scoresDB;


    private void Start()
    {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(SetGameOverUI);

        scoresDB = FindObjectOfType<ScoresDB>();

        if (scoresDB != null)
        {
            List<int> highestScores = scoresDB.GetHighestScores();
            UpdateHighScoresTable(highestScores);
        }
        else
        {
            Debug.LogError("ScoresDB component not found in the scene.");
        }
    }

    public void PlayButtonHandler()
    {
        gm.StartGame();

    }

    public void SetGameOverUI()
    {
        GameOverUI.SetActive(true);
        HighscoresTableUI.SetActive(true);

        GameOverScoreUI.text = "Score: " + gm.TidyScore();

        HighScoreTextUI.text = "Highscore: " + gm.TidyHighScore();



    }
 private void UpdateHighScoresTable(List<int> scores)
    {
        if (HighScoresTable != null)
        {
            HighScoresTable.text = "Top 5 Highest Scores:\n";
            for (int i = 0; i < scores.Count; i++)
            {
                HighScoresTable.text += (i + 1) + ". " + scores[i] + "\n";
            }
        }
    }


    private void OnGUI()
    {
        scoreUI.text = gm.TidyScore();
    }
}
