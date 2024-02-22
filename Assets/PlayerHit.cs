using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) { 
        if(other.transform.tag == "Obstacle") {
            Destroy(gameObject);
            // Game Manager Set Game Over
            GameManager.Instance.GameOver();
        }
    }
}
