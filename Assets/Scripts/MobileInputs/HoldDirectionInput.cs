using UnityEngine;
using UnityEngine.Events;

public class HoldDirectionInput : MonoBehaviour
{
    [SerializeField] private float MinMagnitude;

    private bool isTouching = false;
    private Vector2 startTouchPosition;

    public UnityEvent<Vector2> OnDirectionHeld;

    void Update()
    {
        HandleTouch();
    }
    void HandleTouch()
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
                        Vector2 currentPos = touch.position;
                        Vector2 direction = currentPos - startTouchPosition;
                        if (direction.magnitude > MinMagnitude)
                        {
                            OnDirectionHeld?.Invoke(direction.normalized);
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    break;
            }
        }
        else
        {
            OnDirectionHeld?.Invoke(Vector2.zero);
        }
    }
}