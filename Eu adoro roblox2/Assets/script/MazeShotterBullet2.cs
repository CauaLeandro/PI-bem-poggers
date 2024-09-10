using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeShotterBullet2 : MonoBehaviour
{
    public int damage = 1;  // Dano causado pelo projétil

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto atingido tem um componente EnemyHealth
        BossHp enemyHealth = collision.gameObject.GetComponent<BossHp>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);  // Aplica o dano ao inimigo
            Destroy(gameObject);  // Destrói o projétil após a colisão
        }
    }
    
}
