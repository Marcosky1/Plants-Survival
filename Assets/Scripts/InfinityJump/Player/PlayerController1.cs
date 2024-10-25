using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController1 : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool canJump = true;
    public LayerMask platformLayer;
    public float raycastDistance = 0.5f;
    private GameObject currentPlatform = null;
    private BoxCollider2D playerCollider;

    private bool isOnPlatform = false;
    private Vector3 lastPlatformPosition;
    private int score; 
    public ScoreManager scoreManager; 
    public string playerName; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        playerCollider = GetComponent<BoxCollider2D>();
        score = 0;
    }

    void Update()
    {
        if (canJump && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began ||
            Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();
        }

        CheckIfGrounded();
        ManageCollider();
        MoveWithPlatform();
        FixScale();
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
        isOnPlatform = false;
    }

    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, platformLayer);
        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

        if (hit.collider != null)
        {
            canJump = true;

            if (currentPlatform == null || currentPlatform != hit.collider.gameObject)
            {
                currentPlatform = hit.collider.gameObject;
                isOnPlatform = true;
                lastPlatformPosition = currentPlatform.transform.position;
                score++;
                Debug.Log("Puntaje: " + score); 
            }
        }
        else
        {
            isOnPlatform = false;
        }
    }

    void ManageCollider()
    {
        if (rb.velocity.y > 0)
        {
            playerCollider.isTrigger = true;
        }
        else if (rb.velocity.y < 0)
        {
            playerCollider.isTrigger = false;
        }
    }

    void MoveWithPlatform()
    {
        if (isOnPlatform && currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.transform.position - lastPlatformPosition;
            transform.position += platformMovement;
            lastPlatformPosition = currentPlatform.transform.position;
        }
    }

    void FixScale()
    {
        if (transform.localScale != Vector3.one * 2)
        {
            transform.localScale = Vector3.one * 2;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dead"))
        {
            Die();
        }
    }

    void Die()
    {
        if (scoreManager != null)
        {
            scoreManager.SaveScore(playerName, score);
        }
        SceneManager.LoadScene("MenuJump"); 
    }
}





