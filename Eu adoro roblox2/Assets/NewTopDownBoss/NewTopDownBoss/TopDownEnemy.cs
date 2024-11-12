using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemy : MonoBehaviour
{
    public float speed = 3f; // Velocidade do inimigo 
    public float stoppingDistance = 1.5f; // Dist�ncia em que o inimigo para de se mover
    public float attackDistance = 1.5f; // Dist�ncia em que o inimigo consegue atacar
    public float detectionDistance = 5f; // Dist�ncia para detectar o jogador
    public int damage = 20; // Dano do inimigo
    public float attackCooldown = 1.5f; // Cooldown do inimigo
    private float lastAttackTime = 0; // Tempo do �ltimo ataque

    public int health = 100; // Vida do inimigo
    private GameObject player; // Refer�ncia ao jogador

    private Rigidbody2D rb; // Componente Rigidbody2D para movimenta��o
    private Vector2 movement; // Dire��o do movimento

    void Start()
    {
        // Encontra o jogador pela tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Obt�m o componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calcula a dist�ncia entre o inimigo e o jogador
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Se o jogador estiver dentro da dist�ncia de detec��o
            if (distanceToPlayer < detectionDistance)
            {
                // Se o jogador estiver mais longe que a dist�ncia de parada
                if (distanceToPlayer > stoppingDistance)
                {
                    // Move em dire��o ao jogador
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    movement = direction;

                    // Muda o sprite com base na dire��o do movimento (opcional)
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
                    // Para o movimento quando est� dentro da dist�ncia de parada
                    movement = Vector2.zero;
                }

                // Ataque ao jogador se estiver dentro da dist�ncia de ataque
                if (distanceToPlayer <= attackDistance && Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time; // Reseta o cooldown do inimigo
                }
            }
            else
            {
                // Para o movimento se o jogador estiver fora da dist�ncia de detec��o
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
