using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float pipeSpeed = 3f;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        // Call the SpawnPipe function every 'spawnRate' seconds, starting after 1 second.
        InvokeRepeating("SpawnPipe", 1f, spawnRate);
    }

    void SpawnPipe()
    {
        if (isGameOver) return; // Stop spawning pipes if the game is over

        // 1. Define a spawn position off-screen to the right (e.g., X=10) with a random height.
        Vector3 spawnPosition = new Vector3(10f, Random.Range(-1.5f, 1.5f), 0f);

        // 2. Instantiate a new pipe AT THE CALCULATED SPAWN POSITION.
        GameObject newPipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);

        // 3. Set its velocity to move left.
        newPipe.GetComponent<Rigidbody2D>().velocity = Vector2.left * pipeSpeed;
    }

    public void IncreaseScore()
    {
        if (isGameOver) return;
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over!");
        // Stop all pipes from spawning
        CancelInvoke("SpawnPipe");
        // Optional: Add a delay before restarting
        Invoke("RestartGame", 2f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

