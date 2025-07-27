using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Jump on left mouse click
        {
            rb.velocity = Vector2.zero; // Reset velocity to ensure consistent jump height
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When the bird hits anything, tell the GameManager the game is over.
        FindObjectOfType<GameManager>().GameOver();
    }
}