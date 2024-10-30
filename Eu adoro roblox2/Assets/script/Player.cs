using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject restartText; 
    public GameObject imagem; 

    private Rigidbody2D body;
    private float horizontal;
    private bool isGrounded;
    private int direction = 1;
    private SpriteRenderer spriteRenderer;

    // esses dois a baixo são para o cooldown do bullet do player :) (por bernardo)
    public float shootCooldown = 1f; 
    private float lastShootTime = 0f;

    public Transform shootingPoint;
    void Start()
    {
        lifeMax = Life;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       
        HandleMovement();
        HandleJumping();
        HandleShooting();
        HandlePause();
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        if (horizontal != 0)
        {
            direction = (int)Mathf.Sign(horizontal);

            // Ajusta a escala do player para virar o sprite
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        }
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
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time; // Atualiza o tempo do último tiro
        }
    }

    private void Shoot()
    {

        GameObject temp = Instantiate(bullet, shootingPoint.position, Quaternion.identity); // Usa a posição do shootingPoint
        Rigidbody2D bulletRb = temp.GetComponent<Rigidbody2D>();

        bulletRb.velocity = new Vector2(bulletSpeed * direction, 0);
        bulletRb.gravityScale = 0;

        if (direction == -1)
        {
            temp.transform.localScale = new Vector3(-Mathf.Abs(temp.transform.localScale.x), temp.transform.localScale.y, temp.transform.localScale.z);
        }
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
       
    }

    public void Restart()
    {
        SceneManager.LoadScene("boss battle");
        Time.timeScale = 1;
    }
}
