using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MultiTouchHandler : MonoBehaviour
{
    bool isPinching = false;
    bool isSwiping = false;
    float initialDistance;
    Vector2 initialPos;
    float pinchThreshhold = 0.01f;
    float swipeDistance = 0.1f;
    // Update is called once per frame
    void Update()
    {
        int touchCount = Input.touchCount;

        if (touchCount < 1)
            return;

        if (touchCount == 1)
        {
            Touch touchs0 = Input.GetTouch(0);

            if (touchs0.phase == TouchPhase.Began || touchs0.phase == TouchPhase.Stationary)
            {
                Vector2 initialPos = touchs0.position;
            }
            else if(touchs0.phase == TouchPhase.Moved || touchs0.phase == TouchPhase.Ended)
            {
                float swipeDistanceCalculate = Vector2.Distance(initialPos, touchs0.position);
                isSwiping = true;
                if (swipeDistanceCalculate < swipeDistance)
                {
                    Debug.Log("Swiped left");

                }
                else
                {
                    Debug.Log("Swiped right");
                }
            }
        }
        if (touchCount >= 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if(touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
               initialDistance = Vector2.Distance(touch0.position, touch1.position);
               isPinching = true;
            }
            else if( touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                if(isPinching)
                {
                    float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                    if(Mathf.Abs(currentDistance -  initialDistance) > pinchThreshhold)
                    {
                        if(currentDistance > initialDistance)
                        {
                            Debug.Log("Pinch out (Zoom In)");
                        }
                        else
                        {
                            Debug.Log("Pinch in(Zoom Out)");
                        }

                        initialDistance = currentDistance;
                    }
                }
            }
            else if(touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Canceled)
            {
                isPinching = false;
            }
        }


        for(int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            Debug.Log("Touch " + i + " - Position: " + touch.position + " Phase: " + touch.phase);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Touch " + i + " Started at position " + touch.position);
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Touch " + i + " Moved to position " + touch.position);
                    break;
                case TouchPhase.Stationary:
                    Debug.Log("Touch " + i + " Is stationary at position " + touch.position);
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch " + i + " Ended at position " + touch.position);
                    break;
                case TouchPhase.Canceled:
                    Debug.Log("Touch " + i + " Was canceled at position " + touch.position);
                    break;
            }

        }
    }
}
