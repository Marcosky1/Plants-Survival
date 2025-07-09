using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    public void OnMoveDirection(Vector2 direction)
    {
        rigidBody.linearVelocity = direction * _movementSpeed;
    }
}