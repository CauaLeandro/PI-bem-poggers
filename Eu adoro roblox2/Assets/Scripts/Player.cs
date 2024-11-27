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

    public float jumpForce = 5f;
    private bool isGrounded = true;

    public float speed = 5f;
    public float bulletSpeed = 8f;

    public GameObject restartText; 
    public GameObject imagem; 
    
    private Rigidbody2D body;
   
    private float horizontal;
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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }

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
            if(direction < 0)
            {
                if(transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }

            } else if(direction > 0)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }

            // Ajusta a escala do player para virar o sprite
            
        }
    }
    private void HandleMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
        
       
        

       
        
    }
    

    private void HandleJumping()
    {
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
            TakeDamage(collision.gameObject.GetComponent<NewFirstBoss>().damage);
        }
    }

    public void TakeDamage(float damage)
    {
        Life -= damage;
        if (Life <= 0)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            float bossDamage = collision.gameObject.GetComponent<NewFirstBoss>().damage;
            TakeDamage(bossDamage);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
      void GameOver()
      {
        Debug.Log("Game Over!"); 
        if (restartText != null)
        {
            restartText.SetActive(true);
        }
        speed = 0f;
        body.velocity = Vector2.zero;
        Invoke("Restart", 3f);
        Destroy(gameObject);
      }

    public void Restart()
    {
        SceneManager.LoadScene("boss battle");
        Time.timeScale = 1;
    }
}
