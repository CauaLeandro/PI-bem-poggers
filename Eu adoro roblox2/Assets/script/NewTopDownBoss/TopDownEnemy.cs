using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemy : MonoBehaviour
{
    public float speed = 3f; // Velocidade do inimigo 
    public float stoppingDistance = 1.5f; // Distância em que o inimigo para de se mover
    public float attackDistance = 1.5f; // Distância em que o inimigo consegue atacar
    public float detectionDistance = 5f; // Distância para detectar o jogador
    public int damage = 20; // Dano do inimigo
    public float attackCooldown = 1.5f; // Cooldown do inimigo
    private float lastAttackTime = 0; // Tempo do último ataque

    public int health = 100; // Vida do inimigo
    private GameObject player; // Referência ao jogador

    private Rigidbody2D rb; // Componente Rigidbody2D para movimentação
    private Vector2 movement; // Direção do movimento

    void Start()
    {
        // Encontra o jogador pela tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Obtém o componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calcula a distância entre o inimigo e o jogador
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Se o jogador estiver dentro da distância de detecção
            if (distanceToPlayer < detectionDistance)
            {
                // Se o jogador estiver mais longe que a distância de parada
                if (distanceToPlayer > stoppingDistance)
                {
                    // Move em direção ao jogador
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    movement = direction;

                    // Muda o sprite com base na direção do movimento (opcional)
                    // if (direction.x > 0)
                    // {
                    //     spriteRenderer.sprite = spriteRight; // Olhando para a direita
                    // }
                    // else if (direction.x < 0)
                    // {
                    //     spriteRenderer.sprite = spriteLeft; 
                    // }
                }
                else
                {
                    // Para o movimento quando está dentro da distância de parada
                    movement = Vector2.zero;
                }

                // Ataque ao jogador se estiver dentro da distância de ataque
                if (distanceToPlayer <= attackDistance && Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time; // Reseta o cooldown do inimigo
                }
            }
            else
            {
                // Para o movimento se o jogador estiver fora da distância de detecção
                movement = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        // Move o inimigo
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void AttackPlayer()
    {
        // Dano ao jogador
        player.GetComponent<PlayerHealthTopDownBoss>().TakeDamage(damage);
        Debug.Log("Inimigo atacou o jogador!"); // Log no console
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Inimigo tomou dano! Vida restante: " + health); // Log no console

        if (health <= 0)
        {
            Die(); // O inimigo morre
        }
    }

    void Die()
    {
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject); // Remove o inimigo da cena
    }
}
