using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity);
    }
}