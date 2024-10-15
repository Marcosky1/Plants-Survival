using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bl_Joystick joystick; 
    public float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        movement = new Vector2(horizontal, vertical).normalized;

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false; 
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true; 
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}


