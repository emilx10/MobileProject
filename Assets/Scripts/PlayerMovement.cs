using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public Button leftArrowButton;
    public Button rightArrowButton;
    public Button jumpButton;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;

    private float minX = -1.6f;
    private float maxX = 1.6f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        EventTrigger leftTrigger = leftArrowButton.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(leftTrigger, EventTriggerType.PointerDown, (eventData) => OnLeftArrowButtonPressed());
        AddEventTrigger(leftTrigger, EventTriggerType.PointerUp, (eventData) => OnButtonReleased());

        EventTrigger rightTrigger = rightArrowButton.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(rightTrigger, EventTriggerType.PointerDown, (eventData) => OnRightArrowButtonPressed());
        AddEventTrigger(rightTrigger, EventTriggerType.PointerUp, (eventData) => OnButtonReleased());

        jumpButton.onClick.AddListener(OnJumpButtonPressed);
    }

    void Update()
    {
        HandleTouchInput();
        HandleButtonInput();
    }

    void FixedUpdate()
    {
        CheckGrounded();
    }

    void HandleTouchInput()
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
                        Debug.Log("Swipe Up detected in jumping");
                        Jump();
                    }
                    isTouching = false;
                    break;
            }
        }
    }

    void HandleButtonInput()
    {
        if (moveLeft)
        {
            MovePlayer(Vector3.left);
        }

        if (moveRight)
        {
            MovePlayer(Vector3.right);
        }
    }

    void MovePlayer(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * sideMovementSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }

    void CheckGrounded()
    {
        float raycastDistance = 0.2f; // Increased the distance
        Vector3 origin = transform.position + Vector3.up * 0.1f; // Adjust the raycast origin slightly upwards

        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit, raycastDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Debug.DrawRay(origin, Vector3.down * raycastDistance, Color.red);
        Debug.Log("IsGrounded: " + isGrounded);
    }

    void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("Jumping!");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }
        else
        {
            Debug.Log("Cannot jump, not grounded");
        }
    }

    void MovePlayerWithTouch()
    {
        float swipeDistanceLeft = startTouchPosition.x - endTouchPosition.x;
        float swipeDistanceRight = endTouchPosition.x - startTouchPosition.x;
        if (swipeDistanceRight < 0)
        {
            MovePlayer(Vector3.right);
        }
        if (swipeDistanceLeft < 0)
        {
            MovePlayer(Vector3.left);
        }

        startTouchPosition = endTouchPosition;
    }

    public void OnLeftArrowButtonPressed()
    {
        moveLeft = true;
        moveRight = false;
    }

    public void OnRightArrowButtonPressed()
    {
        moveRight = true;
        moveLeft = false;
    }

    public void OnButtonReleased()
    {
        moveLeft = false;
        moveRight = false;
    }

    public void OnJumpButtonPressed()
    {
        Debug.Log("Jump pressed");
        Jump();
    }

    void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((data) => action(data));
        trigger.triggers.Add(entry);
    }
}
