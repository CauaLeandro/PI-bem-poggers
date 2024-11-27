using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFirstBoss : MonoBehaviour
{
    public float life = 100f; 
    public float maxLife = 100f; 
    public float speed = 3f; 
    public float damage = 1f;

    public Image lifeBar; 
    public Canvas lifeBarCanvas; 
    private int direction = -1; 

    private Rigidbody2D rb;

    private float lastDamageTime; 
    public float damageCooldown = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        UpdateLifeBar();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
       
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
       // faz ele andar igual a um gumba XD
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; 
            Flip();
        }
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bulletScript = collision.gameObject.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                TakeDamage(bulletScript.damage);
                Destroy(collision.gameObject); 
            }
        }


        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10f);
            Destroy(collision.gameObject); 
        }
        
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Aplica dano ao Player com cooldown
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null && Time.time >= lastDamageTime + damageCooldown)
            {
                playerScript.TakeDamage(damage); // Aplica dano ao Player
                lastDamageTime = Time.time; // Atualiza o tempo do último dano
            }
        }
    }
    public void TakeDamage(float amount)
    {
        life -= amount;
        UpdateLifeBar();

        if (life <= 0)
        {
            Die();
        }
    }

    private void UpdateLifeBar()
    {
        // Atualiza a barra de vida (proporção da vida atual para a máxima)
        if (lifeBar != null)
        {
            lifeBar.fillAmount = life / maxLife;
        }
    }

    private void Flip()
    {
        // Inverte a escala para virar o sprite
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Die()
    {
        // Executa ações ao morrer (pode ser animação, som, etc.)
        Destroy(gameObject);
        Debug.Log("Boss morreu!");
    }
}
