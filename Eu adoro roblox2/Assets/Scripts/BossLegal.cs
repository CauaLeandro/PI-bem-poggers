using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegal : MonoBehaviour
{
    public int damage = 2; // Dano que o inimigo pode causar (se necessário)
    public float speed = 3f; // Velocidade do inimigo
    public int Life = 15; // Vida do inimigo
    private int direction = -1; // Inicializa com uma direção padrão
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // Define a velocidade inicial do inimigo
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }

    void Update()
    {
        // Move o inimigo na direção atual
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Inverte a direção ao colidir com o chão ou a parede
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            ReverseDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Diminui a vida do inimigo ao ser atingido por uma bala
        if (collision.CompareTag("Bullet"))
        {
            Life -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject); // Destrói a bala

            // Verifica se a vida do inimigo chegou a 0
            if (Life <= 0)
            {
                Destroy(gameObject); // Destrói o inimigo
            }
        }
    }

    private void ReverseDirection()
    {
        // Inverte a direção do movimento
        direction *= -1;
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }
}
