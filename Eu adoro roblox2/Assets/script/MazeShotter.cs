using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeShotter : MonoBehaviour
{
    public Camera sceneCamera;

    public float moveSpeed;

    private Vector2 MoveDirection;
    public Rigidbody2D rb;

    private Vector2 mouseDirection;

    private Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MoveDirection = new Vector2(moveX, moveY);
    }

    private void Move()
    {
        rb.velocity = new Vector2(MoveDirection.x * moveSpeed, MoveDirection.y * moveSpeed);
    }
}
