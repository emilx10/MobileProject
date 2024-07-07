using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f; // Minimum swipe distance to be considered a swipe

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record the starting touch position
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Record the ending touch position
                    endTouchPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    void DetectSwipe()
    {
        // Calculate the swipe distance in both directions
        float swipeDistanceX = endTouchPosition.x - startTouchPosition.x;
        float swipeDistanceY = endTouchPosition.y - startTouchPosition.y;

        // Check if the swipe distance is greater than the threshold
        if (Mathf.Abs(swipeDistanceX) > swipeThreshold || Mathf.Abs(swipeDistanceY) > swipeThreshold)
        {
            // Determine the direction of the swipe
            if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY))
            {
                if (swipeDistanceX > 0)
                {
                    // Right swipe
                    Debug.Log("Swipe Right");
                }
                else
                {
                    // Left swipe
                    Debug.Log("Swipe Left");
                }
            }
            else
            {
                if (swipeDistanceY > 0)
                {
                    // Up swipe
                    Debug.Log("Swipe Up");
                }
                else
                {
                    // Down swipe
                    Debug.Log("Swipe Down");
                }
            }
        }
        else
        {
            Debug.Log("Swipe too short");
        }
    }
}
