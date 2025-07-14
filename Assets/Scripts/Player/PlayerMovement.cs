using UnityEngine;

public class PlayerMovement : PhotonIdentity
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        if(!view.IsMine)
            return;

        rigidBody = GetComponent<Rigidbody2D>();
    }
    public void OnMoveDirection(Vector2 direction, float intensity)
    {
        if (!view.IsMine)
            return;

        rigidBody.position += direction.normalized * intensity * _movementSpeed * Time.deltaTime;
    }
}