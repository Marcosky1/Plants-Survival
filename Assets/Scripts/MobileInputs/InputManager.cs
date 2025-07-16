using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// InputManager class
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [System.Serializable]
    public class Vector2Event : UnityEvent<Vector2> { }

    public UnityEvent<Vector2> OnSwipe;
    public Vector2Event OnTap;
    public Vector2Event OnLongPress;

    [SerializeField] private float swipeThreshold = 50f;
    [SerializeField] private float longPressTime = 0.5f;

    private TouchReader touchReader;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        touchReader = new TouchReader(swipeThreshold, longPressTime);
    }

    private void Update()
    {
        if (IsPointerOverUI()) return;

        touchReader.Update();

        if (touchReader.DetectedSwipe)
            OnSwipe?.Invoke(touchReader.SwipeDirection);
        else if (touchReader.DetectedTap)
            OnTap?.Invoke(touchReader.LastTouchPosition);
        else if (touchReader.DetectedLongPress)
            OnLongPress?.Invoke(touchReader.LastTouchPosition);
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
        {
            Debug.LogWarning("No EventSystem found in the scene. UI detection disabled.");
            return false;
        }

        // Editor: Manejar clics de ratón
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.OSXEditor ||
            Application.platform == RuntimePlatform.LinuxEditor)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                return EventSystem.current.IsPointerOverGameObject();
            }
            return false;
        }

        // Móviles: Manejar toques
        if (Input.touchCount > 0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }

        return false;
    }
}