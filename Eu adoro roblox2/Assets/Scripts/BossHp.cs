using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    public int maxHealth = 3;       // Sa�de m�xima do inimigo
    private int currentHealth;      // Sa�de atual do inimigo

    private void Start()
    {
        currentHealth = maxHealth;  // Define a sa�de inicial como m�xima
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;    // Reduz a sa�de pelo dano recebido

        if (currentHealth <= 0)
        {
            Die();  // Chama o m�todo de morte se a sa�de for menor ou igual a 0
        }
    }

    private void Die()
    {
        // Aqui voc� pode adicionar efeitos de morte, anima��es, etc.
        Destroy(gameObject);  // Remove o GameObject do inimigo da cena
    }
}
