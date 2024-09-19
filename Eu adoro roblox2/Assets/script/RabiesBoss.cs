using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabiesBoss : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do boss
    public float bounceForce = 10f; // For�a do quique
    public float damage = 1f; // Dano causado ao jogador
    public Transform groundCheck; // Ponto para verificar se est� no ch�o
    public LayerMask groundLayer; // Layer do ch�o
    public float groundCheckRadius = 0.2f; // Raio para verificar se est� no ch�o

    private Rigidbody2D rb;
    private bool isGrounded;
    private int direction = 1; // 1 para direita, -1 para esquerda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica se o boss est� no ch�o
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Se estiver no ch�o, quique
        if (isGrounded)
        {
            rb.velocity = new Vector2(direction * speed, bounceForce);
        }
    }

    void FixedUpdate()
    {
        // Movimento horizontal
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Muda de dire��o ao colidir com paredes
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // Inverte a dire��o
        }

        // Causa dano ao jogador se colidir
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Life -= damage;
        }
    }
}
