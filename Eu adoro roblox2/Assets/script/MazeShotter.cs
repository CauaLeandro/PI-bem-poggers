using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeShotter : MonoBehaviour
{


    public Camera sceneCamera;

    public float moveSpeed;

    private Vector2 MoveDirection;
    public Rigidbody2D rb;

    public MazeWeapon weapon;



    private Vector2 moveDirection;

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

        if (Input.GetMouseButtonDown(0))
        {
            //MazeWeapon.Fire();

        }
        MoveDirection = new Vector2(moveX, moveY);
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Move()
    {
        rb.velocity = new Vector2(MoveDirection.x * moveSpeed, MoveDirection.y * moveSpeed);

        // gira o player

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 0f;
        rb.rotation = aimAngle;
    }
}
