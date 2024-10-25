using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bl_Joystick joystick;
    public float moveSpeed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        movement = new Vector2(horizontal, vertical).normalized;
    }

    void FixedUpdate()
    {
        rb2d.velocity = movement * moveSpeed;
    }
}


