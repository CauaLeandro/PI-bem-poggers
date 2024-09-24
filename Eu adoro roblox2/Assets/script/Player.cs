using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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

    void Start()
    {
        lifeMax = Life;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle horizontal movement
        horizontal = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpStrength);
        }

        // Handle shooting
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * direction, 0);
        }

        // Handle pausing and unpausing
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                OnUnPause.Invoke();
            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                OnPause.Invoke();
            }
        }

        // Update direction
        if (horizontal != 0)
        {
            direction = (int)horizontal;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            if(Life > 0)
            {
                Life -= collision.gameObject.GetComponent<BossLegal>().damage;
            }
            else if (Life <= 0)
            {
            
            }
        }
    }

    void GameOver()
    {
    }

    public void Recomecar()
    {
        SceneManager.LoadScene("boss battle");
        Time.timeScale = 1F;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Handle any necessary logic when the player exits a collision
    }
}

