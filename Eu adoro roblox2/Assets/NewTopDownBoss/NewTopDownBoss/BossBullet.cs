using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 1; // Dano que a bala causa

    void Update()
    {
       
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthTopDownBoss>().TakeDamage(damage);
            Destroy(gameObject); // Destroi a bala ao atingir o player
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroi a bala ao atingir uma parede
        }
    }
   
}

