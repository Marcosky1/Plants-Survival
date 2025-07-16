using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header ("ROOM SETTINGS")]
    [SerializeField] private GameObject roomHolder;
    [SerializeField] private RectTransform roomParent;

    [Header ("CREATE SETTINGS")]
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TMP_InputField roomCodeInput;
    [SerializeField] private Toggle isPrivateToggle;
    
    [Header ("JOIN SETTINGS")]
    [SerializeField] private TMP_InputField joinInput;
    private List<RoomHolder> allRoomHolders = new List<RoomHolder>();
    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();
    private bool isRefreshing;

    [Header("EVENTS")]
    public UnityEvent _OnLeftRoom;
    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.IsVisible = !isPrivateToggle.isOn;
        options.IsOpen = true;
        options.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(roomNameInput.text, options, TypedLobby.Default);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void RefreshRoomList()
    {
        if (!PhotonNetwork.IsConnected || isRefreshing)
            return;

        isRefreshing = true;

        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnJoinedLobby()
    {
        isRefreshing = false;
    }
    public override void OnLeftLobby()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiplayerGame");
    }
    public override void OnLeftRoom()
    {
        _OnLeftRoom?.Invoke();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        cachedRoomList.Clear();
        DestroyAllRoomObjects();

        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList)
            {
                //Create a new holder on UI
                RoomHolder newHolder = Instantiate(roomHolder, roomParent).GetComponent<RoomHolder>();

                //Adding to lists
                allRoomHolders.Add(newHolder);
                cachedRoomList.Add(room);

                //Applying info to holder
                newHolder.ApplyInfo(room.Name, room.PlayerCount);
                newHolder.JoinRoom.onClick.AddListener(() => PhotonNetwork.JoinRoom(newHolder.RoomName));
            }
        }
    }
    public void DestroyAllRoomObjects()
    {
        if (allRoomHolders.Count > 0)
        {
            for (int i = 0; i < allRoomHolders.Count; i++)
            {
                Destroy(allRoomHolders[i].gameObject);
            }
        }
    }
}