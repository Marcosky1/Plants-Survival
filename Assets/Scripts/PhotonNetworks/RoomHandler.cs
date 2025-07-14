using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private TextMeshProUGUI roomPlayerCount;
    
    public Button JoinRoom;
    public string RoomName => roomName.text;
    public int PlayerCount => int.Parse(roomPlayerCount.text);

    public void ApplyInfo(string name, int playerCount)
    {
        roomName.text = name;
        roomPlayerCount.text = playerCount.ToString();
    }
}