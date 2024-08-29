using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeShotter : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    public MazeWeapon mazeWeapon;

    public float health = 100f; // Saúde do jogador

    private Vector2 MoveDirection;
    private Vector2 mousePosition;

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
            mazeWeapon.Fire();
        }

        MoveDirection = new Vector2(moveX, moveY);
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Move()
    {
        rb.velocity = new Vector2(MoveDirection.x * moveSpeed, MoveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Lógica para a morte do jogador (ex: desativar o jogador, tocar animação de morte, etc.)
        Debug.Log("Player has died");
        gameObject.SetActive(false);
    }
}
