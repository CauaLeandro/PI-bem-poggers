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
        // Detecta colisão com BoxCollider e muda a direção
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // Inverte a direção
            Flip();
        }

        // Detecta colisão com balas e reduz a vida do Boss
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10f); // Reduz 10 de vida ao ser atingido
            Destroy(collision.gameObject); // Destrói a bala
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta entrada do jogador na área
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
        // Detecta saída do jogador da área
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
