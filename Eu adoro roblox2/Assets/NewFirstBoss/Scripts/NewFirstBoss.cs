using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFirstBoss : MonoBehaviour
{
    public float life = 100f; // Vida do Boss
    public float maxLife = 100f; // Vida máxima do Boss
    public float speed = 3f; // Velocidade do Boss
    public float damage = 1f; // Dano do Boss
    public float damageCooldown = 0.5f; // Tempo de espera entre danos
    public GameObject objectToDeactivate; // Objeto associado que será desativado
    public Image lifeBar; // Barra de vida do Boss

    private Rigidbody2D rb;
    private float lastDamageTime;
    private int direction = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateLifeBar();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y); // Mantém o movimento
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // Troca a direção
            Flip(); // Inverte o sprite
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                DealDamageToPlayer(collision.gameObject);
                lastDamageTime = Time.time; // Atualiza o cooldown
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void DealDamageToPlayer(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(damage); // Reduz a vida do player
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
        if (lifeBar != null)
        {
            lifeBar.fillAmount = life / maxLife;
        }
    }

    private void Die()
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false); // Desativa o objeto associado
        }

        Destroy(gameObject); // Remove o Boss
        Debug.Log("Boss morreu!");
    }
}