using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float lifeTime = 5f; // Tempo de vida da bala antes de ser destru�da
    public int damage = 1; // Dano que a bala causa

    void Start()
    {
        // Destroi a bala ap�s lifeTime segundos
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Caso a bala colida com o jogador, aplica dano
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aqui voc� pode chamar o m�todo que aplica dano ao jogador
            // collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Destroi a bala ap�s colidir
            Destroy(gameObject);
        }

        // Se a bala colidir com outro objeto, destr�i a bala
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

