using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float pipeSpeed = 3f;
    public TextMeshProUGUI scoreText, highScoreText;
    public GameObject overText;
    public RectTransform canvas;
    Animator ani;

    private int score = 0;
    public bool isGameOver = false;

    void Start()
    {
        // Call the SpawnPipe function every 'spawnRate' seconds, starting after 1 second.
        overText.SetActive(false);
        InvokeRepeating("SpawnPipe", 1f, spawnRate);

        if (PlayerPrefs.HasKey("HScore"))
        {

        highScoreText.text = PlayerPrefs.GetInt("HScore").ToString();
        }
        else
        {
            highScoreText.text = "0";
        }
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

        ani.SetBool("RUnning", true);
        ani.Play("put the gun to the back");

    }

    public void IncreaseScore()
    {
        if (isGameOver) return;

        scoreText.rectTransform.DOScale(1.4f, 0.2f).OnComplete(() => scoreText.rectTransform.DOScale(1f, 0.2f));
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Camera.main.transform.DOShakePosition(0.3f, 1f);
        canvas.DOShakePosition(0.3f, 1f);
        Debug.Log("Game Over!");
        overText.SetActive(true);
        if(!PlayerPrefs.HasKey("HScore") || score> PlayerPrefs.GetInt("HScore"))
        {
            PlayerPrefs.SetInt("HScore", score);
        }
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

