using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float sideMovementSpeed = 5f;

    private bool isGrounded;
    private Rigidbody rb;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isTouching = false;
    private float swipeThreshold = 50f;
    private bool swipeUpDetected = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isTouching = true;
                    swipeUpDetected = false;
                    break;

                case TouchPhase.Moved:
                    endTouchPosition = touch.position;
                    float verticalSwipeDistance = endTouchPosition.y - startTouchPosition.y;

                    if (Mathf.Abs(verticalSwipeDistance) > swipeThreshold)
                    {
                        if (verticalSwipeDistance > 0)
                        {
                            swipeUpDetected = true;
                        }
                    }
                    else
                    {
                        MovePlayerWithTouch();
                    }
                    break;

                case TouchPhase.Ended:
                    if (swipeUpDetected)
                    {
                        Jump();
                    }
                    isTouching = false;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void MovePlayerWithTouch()
    {
        float swipeDistanceLeft = startTouchPosition.x - endTouchPosition.x;
        float swipeDistanceRight = endTouchPosition.x - startTouchPosition.x;
        if (swipeDistanceRight > 0)
        {
            transform.Translate(Vector3.right * sideMovementSpeed * Time.deltaTime);
        }
        if(swipeDistanceLeft > 0)
        {
            transform.Translate(Vector3.left * sideMovementSpeed * Time.deltaTime);
        }

        startTouchPosition = endTouchPosition;
    }
}