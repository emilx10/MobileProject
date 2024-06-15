using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    bool leftButton = false;
    bool rightButton = false;
    bool jumpButton = false;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        CheckInputs();

        if (jumpButton)
        {
            Jump();
        }
    }

    void CheckInputs()
    {
        if (leftButton)
        {
            MoveLeft();
        }
        if (rightButton)
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        Vector3 newPosition = transform.position + Vector3.left * speed * Time.deltaTime;

        if (newPosition.x > -1f)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void MoveRight()
    {
        Vector3 newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

        if (newPosition.x < 2f)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0);

        jumpButton = false;
    }

    public void IsClickedRight()
    {
        rightButton = true;
    }

    public void IsClickedLeft()
    {
        leftButton = true;
    }

    public void IsClickedJump()
    {
        jumpButton = true;
    }

    public void IsNotClickedRight()
    {
        rightButton = false;
    }

    public void IsNotClickedLeft()
    {
        leftButton = false;
    }

    public void IsNotClickedJump()
    {
        jumpButton = false;
    }
}
