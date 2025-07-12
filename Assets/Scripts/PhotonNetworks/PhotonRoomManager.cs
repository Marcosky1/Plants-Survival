using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }
    public void JoinRoon()
    {
        PhotonNetwork.CreateRoom(joinInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}