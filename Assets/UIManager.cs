using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;

    [SerializeField] private GameObject StartUI;

    [SerializeField] private GameObject GameOverUI;
    GameManager gm;

    private void Start(){
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(SetGameOverUI);
    }

    public void PlayButtonHandler() { 
        gm.StartGame();
    }

    public void SetGameOverUI() {
        GameOverUI .SetActive(true);
    }

    private void OnGUI() {
        scoreUI.text = gm.TidyScore();
    }
}
