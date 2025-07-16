using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIRoomManager : MonoBehaviour
{
    [Header("ROOM SETTINGS")]
    [SerializeField] private GameObject roomHolder;
    [SerializeField] private RectTransform roomParent;

    [Header("CREATE SETTINGS")]
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TMP_InputField roomCodeInput;
    [SerializeField] private Toggle isPrivateToggle;

    [Header("JOIN SETTINGS")]
    [SerializeField] private TMP_InputField joinInput;
}
