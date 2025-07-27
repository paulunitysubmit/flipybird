using UnityEngine;

public class PipeScorer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the bird passes through the trigger zone, increase the score.
        if (other.CompareTag("Player")) // Make sure your bird has the "Player" tag
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

    private void Update()
    {
        // Destroy the pipe after it goes off-screen to the left
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}