using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public UnityEvent OnPlayerKillEnemy;
    public UnityEvent OnPause;
    public UnityEvent OnUnPause;
    public int score;
    public float Life = 3f;
    public float lifeMax;
    public GameObject bullet;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public float speed = 5f;
    public float jumpStrength = 5f;
    public float bulletSpeed = 8f;
    public GameObject restartText; // Not used in this code
    public GameObject imagem; // Not used in this code

    private Rigidbody2D body;
    private float horizontal;
    private bool isGrounded;
    private int direction = 1;

    void Start()
    {
        lifeMax = Life;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleShooting();
        HandlePause();
        UpdateDirection();
    }

    private void HandleMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
    }

    private void HandleJumping()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpStrength);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * direction, 0);
    }

    private void HandlePause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                OnUnPause.Invoke();
            }
            else
            {
                Time.timeScale = 0;
                OnPause.Invoke();
            }
        }
    }

    private void UpdateDirection()
    {
        if (horizontal != 0)
        {
            direction = (int)Mathf.Sign(horizontal); // Use Mathf.Sign to determine direction
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(collision.gameObject.GetComponent<BossLegal>().damage);
        }
    }

    private void TakeDamage(float damage)
    {
        Life -= damage;
        if (Life <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Implement game over logic here (e.g., show restart options)
    }

    public void Restart()
    {
        SceneManager.LoadScene("boss battle");
        Time.timeScale = 1;
    }
}
