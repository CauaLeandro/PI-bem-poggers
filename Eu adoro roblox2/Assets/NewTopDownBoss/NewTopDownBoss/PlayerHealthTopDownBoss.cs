using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthTopDownBoss : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Image healthBarFill; // Referência para a imagem de preenchimento da barra de vida

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // Inicializa a barra de vida no início
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar(); // Atualiza a barra de vida ao tomar dano

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            // Calcula o preenchimento com base na vida atual
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Jogador morreu!");
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
