// TouchReader class
using UnityEngine;

public class TouchReader
{
    private float swipeThreshold;
    private float longPressTime;

    private Vector2 touchStartPos;
    private float touchStartTime;

    public bool DetectedSwipe { get; private set; }
    public bool DetectedTap { get; private set; }
    public bool DetectedLongPress { get; private set; }
    public Vector2 SwipeDirection { get; private set; }
    public Vector2 LastTouchPosition { get; private set; }

    public TouchReader(float swipeThreshold, float longPressTime)
    {
        this.swipeThreshold = swipeThreshold;
        this.longPressTime = longPressTime;
    }

    public void Update()
    {
        DetectedSwipe = false;
        DetectedTap = false;
        DetectedLongPress = false;

        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        LastTouchPosition = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                touchStartTime = Time.time;
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                float touchDuration = Time.time - touchStartTime;
                Vector2 delta = touch.position - touchStartPos;

                if (delta.magnitude > swipeThreshold)
                {
                    DetectedSwipe = true;
                    SwipeDirection = delta.normalized;
                }
                else if (touchDuration >= longPressTime)
                {
                    DetectedLongPress = true;
                }
                break;

            case TouchPhase.Ended:
                touchDuration = Time.time - touchStartTime;
                delta = touch.position - touchStartPos;

                if (delta.magnitude > swipeThreshold)
                {
                    DetectedSwipe = true;
                    SwipeDirection = delta.normalized;
                }
                else if (touchDuration < longPressTime)
                {
                    DetectedTap = true;
                }
                break;
        }
    }
}