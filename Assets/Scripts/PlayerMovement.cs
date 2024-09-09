using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float sideMovementSpeed = 5f;
    public float crouchDuration = 0.7f;
    public float crouchSpeedMultiplier = 2f;
    public Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);
    public Vector3 originalScale = new Vector3(1f, 1f, 1f);

    private bool isGrounded;
    private Rigidbody rb;
    private bool isCrouching = false;
    private float crouchEndTime = 0f;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isTouching = false;
    private float swipeThreshold = 50f;
    private bool swipeUpDetected = false;
    private bool swipeDownDetected = false;

    public Button leftArrowButton;
    public Button rightArrowButton;
    public Button jumpButton;
    public Button downArrowButton;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;
    private bool crouch = false;

    private float minX = -1.6f;
    private float maxX = 1.6f;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found!");
        }

        rb = GetComponent<Rigidbody>();

        EventTrigger leftTrigger = leftArrowButton.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(leftTrigger, EventTriggerType.PointerDown, (eventData) => OnLeftArrowButtonPressed());
        AddEventTrigger(leftTrigger, EventTriggerType.PointerUp, (eventData) => OnButtonReleased());

        EventTrigger rightTrigger = rightArrowButton.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(rightTrigger, EventTriggerType.PointerDown, (eventData) => OnRightArrowButtonPressed());
        AddEventTrigger(rightTrigger, EventTriggerType.PointerUp, (eventData) => OnButtonReleased());

        jumpButton.onClick.AddListener(OnJumpButtonPressed);
        downArrowButton.onClick.AddListener(OnDownArrowButtonPressed);  // Down arrow button listener
    }

    void Update()
    {
        HandleTouchInput();
        HandleButtonInput();

        if (isCrouching && Time.time >= crouchEndTime)
        {
            EndCrouch();
        }
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
                    swipeDownDetected = false;
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
                        else
                        {
                            swipeDownDetected = true;
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
                    if (swipeDownDetected)
                    {
                        Debug.Log("Swipe Down detected in crouching");
                        StartCrouch();
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

        if (crouch)
        {
            StartCrouch();
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
        float raycastDistance = 1.1f;
        Vector3 origin = transform.position + Vector3.up * 0.1f;

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
        Debug.Log("isGrounded: " + isGrounded);
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

    void StartCrouch()
    {
        if (isGrounded && !isCrouching)
        {
            Debug.Log("Crouching!");
            isCrouching = true;
            crouchEndTime = Time.time + crouchDuration;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * crouchSpeedMultiplier);
            transform.localScale = crouchScale;
        }
        else
        {
            Debug.Log("Cannot crouch, not grounded or already crouching");
        }
    }

    void EndCrouch()
    {
        Debug.Log("Ending Crouch");
        isCrouching = false;
        transform.localScale = originalScale;
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
        crouch = false;
    }

    public void OnJumpButtonPressed()
    {
        Jump();
    }

    public void OnDownArrowButtonPressed()
    {
        crouch = true;
        StartCrouch();
    }


    void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((data) => action(data));
        trigger.triggers.Add(entry);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            scoreManager.GameOver();
            Debug.Log("Ouch");
        }
    }


}
