using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void Start() { 
        GameManager.Instance.onPlay.AddListener(ActivatePlay);
    }

    private void ActivatePlay() {
        gameObject.SetActive(true); 
    }
    private void OnCollisionEnter2D(Collision2D other) { 
        if(other.transform.tag == "Obstacle") {
            // Game Manager Set Game Over
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }
}
