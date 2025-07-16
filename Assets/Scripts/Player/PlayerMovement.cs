using Photon.Pun;
using UnityEngine;

public class PlayerMovement : PhotonIdentity
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        if (!view.IsMine || !PhotonNetwork.IsConnected)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
    }
    public void OnMoveDirection(Vector2 direction, float intensity)
    {
        if (!view.IsMine || !PhotonNetwork.IsConnected)
        {
            rigidBody.position += direction.normalized * intensity * _movementSpeed * Time.deltaTime;
        }
    }
}