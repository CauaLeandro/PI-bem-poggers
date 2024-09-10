using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBetinha : MonoBehaviour
{
    public int damage = 2;
    public float speed;
    public int Life = 15;
    private int direction = -1; // Inicializa com uma dire��o padr�o
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // Define a velocidade inicial do Boss
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }

    void Update()
    {
        // Move o Inimigo na dire��o atual
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Inverte a dire��o quando colide com o ch�o ou a parede

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Inverte a dire��o ao colidir com a parede
        if (collision.CompareTag("Wall"))
        {
            ReverseDirection();
        }

        // Diminui a vida do Inimigo ao ser atingido por uma bala
        if (collision.CompareTag("Bullet"))
        {
            Life -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject); // Destr�i a bala
            if (Life <= 0)
            {
                Destroy(gameObject); // Destr�i o Inimigo
            }
      
        }
       
    }
    private void ReverseDirection()
    {
        // Inverte a dire��o do movimento
        direction *= -1;
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }
}


   

     
