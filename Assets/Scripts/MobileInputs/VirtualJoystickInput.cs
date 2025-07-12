using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class JoystickInputEvent : UnityEvent<Vector2, float> { }

public class VirtualJoystickInput : MonoBehaviour
{
    [SerializeField] private float deadZone;
    [SerializeField] private float maxDistance;

    private Vector2 startTouchPosition;
    private bool isTouching = false;

    public JoystickInputEvent OnJoystickInput;

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isTouching)
                    {
                        Vector2 currentPosition = touch.position;
                        Vector2 delta = currentPosition - startTouchPosition;
                        float distance = delta.magnitude;

                        if (distance > deadZone)
                        {
                            Vector2 direction = delta.normalized;
                            float clampedDistance = Mathf.Clamp(distance - deadZone, 0, maxDistance);
                            float intensity = clampedDistance / maxDistance;
                            OnJoystickInput?.Invoke(direction, intensity);
                        }
                        else
                        {
                            OnJoystickInput?.Invoke(Vector2.zero, 0f);
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    OnJoystickInput?.Invoke(Vector2.zero, 0f);
                    break;
            }
        }
        else if (isTouching)
        {
            isTouching = false;
            OnJoystickInput?.Invoke(Vector2.zero, 0f);
        }
    }
}