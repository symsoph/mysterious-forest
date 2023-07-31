using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Movement Variables

    public float movementSpeed = 5f; // 5 by default
    public float jumpForce = 7f;
    public Rigidbody2D playerRB;
    public bool grounded;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    #endregion

    #region General Behavior Loop

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), 0);
        TurnAround(movementVector);
        Move(movementVector);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();

            grounded = false;
        }
        SmoothenJump();
    }


    // Checking if player returns to the ground and able to jump again
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground") && grounded == false)
        {
            grounded = true;
        }
    }

    #endregion 

    #region Player Movement Functions

    private void Move(Vector2 directionVector)
    {
        TurnAround(directionVector);
        playerRB.velocity = new Vector2(directionVector.x * movementSpeed, playerRB.velocity.y);
    }

    private void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        playerRB.velocity += Vector2.up * jumpForce;
    }

    private void SmoothenJump()
    {
        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

    private void TurnAround(Vector2 directionVector)
    {
        if (directionVector == new Vector2(1, 0))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if (directionVector == new Vector2(-1, 0))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    #endregion 
}