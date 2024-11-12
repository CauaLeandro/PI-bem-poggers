using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float lifeTime = 5f; // Tempo de vida da bala antes de ser destruída
    public int damage = 1; // Dano que a bala causa

    void Start()
    {
        // Destroi a bala após lifeTime segundos
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Caso a bala colida com o jogador, aplica dano
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aqui você pode chamar o método que aplica dano ao jogador
            // collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Destroi a bala após colidir
            Destroy(gameObject);
        }

        // Se a bala colidir com outro objeto, destrói a bala
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

