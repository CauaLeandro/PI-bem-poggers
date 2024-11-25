using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    public int maxHealth = 3;       // Saúde máxima do inimigo
    private int currentHealth;      // Saúde atual do inimigo

    private void Start()
    {
        currentHealth = maxHealth;  // Define a saúde inicial como máxima
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;    // Reduz a saúde pelo dano recebido

        if (currentHealth <= 0)
        {
            Die();  // Chama o método de morte se a saúde for menor ou igual a 0
        }
    }

    private void Die()
    {
        // Aqui você pode adicionar efeitos de morte, animações, etc.
        Destroy(gameObject);  // Remove o GameObject do inimigo da cena
    }
}
