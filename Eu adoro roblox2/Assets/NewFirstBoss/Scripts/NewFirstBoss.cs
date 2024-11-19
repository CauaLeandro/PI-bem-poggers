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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

       
        if (lifeBarCanvas != null)
        {
            lifeBarCanvas.enabled = false;
        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta colis�o com BoxCollider e muda a dire��o
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // Inverte a dire��o
            Flip();
        }

        // Detecta colis�o com balas e reduz a vida do Boss
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10f); // Reduz 10 de vida ao ser atingido
            Destroy(collision.gameObject); // Destr�i a bala
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta entrada do jogador na �rea
        if (other.CompareTag("Player"))
        {
            if (lifeBarCanvas != null)
            {
                lifeBarCanvas.enabled = true; // Mostra a barra de vida
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Detecta sa�da do jogador da �rea
        if (other.CompareTag("Player"))
        {
            if (lifeBarCanvas != null)
            {
                lifeBarCanvas.enabled = false; // Esconde a barra de vida
            }
        }
    }

    private void TakeDamage(float amount)
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
        // Atualiza a barra de vida (propor��o da vida atual para a m�xima)
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
        // Executa a��es ao morrer (pode ser anima��o, som, etc.)
        Destroy(gameObject);
        Debug.Log("Boss morreu!");
    }
}
