using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity);
        }
        else
        {
            Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        }
    }
}