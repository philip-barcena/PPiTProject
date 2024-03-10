using System.Collections;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite run1Sprite;
    public Sprite run2Sprite;
    public float switchInterval = 0.5f; // Interval between sprite changes

    private SpriteRenderer spriteRenderer;
    private bool isRunning = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Start the animation
        StartAnimation();
    }

    public void StartAnimation()
    {
        isRunning = true;
        StartCoroutine(AnimateSprite());
    }

    public void StopAnimation()
    {
        isRunning = false;
        StopCoroutine(AnimateSprite());
    }

    IEnumerator AnimateSprite()
    {
        while (isRunning)
        {
            // Change the sprite to run1Sprite
            spriteRenderer.sprite = run1Sprite;
            yield return new WaitForSeconds(switchInterval);

            // Change the sprite to run2Sprite
            spriteRenderer.sprite = run2Sprite;
            yield return new WaitForSeconds(switchInterval);
        }
    }

    // Method to restart the animation
    public void Replay()
    {
        StartAnimation();
    }
}
