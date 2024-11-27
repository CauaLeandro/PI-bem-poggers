using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public float Life = 3f; // Vida atual do jogador
    public float lifeMax = 3f; // Vida máxima do jogador
    public Image healthBar; // Referência à barra de vida (UI)

    private void Start()
    {
        UpdateHealthBar(); // Inicializa a barra de vida no início
    }

    public void TakeDamage(float damage)
    {
        Life -= damage; // Reduz a vida com base no dano recebido
        if (Life < 0) Life = 0; // Garante que a vida não fique negativa
        UpdateHealthBar(); // Atualiza a barra de vida

        if (Life <= 0)
        {
            GameOver(); // Chama o método de fim de jogo
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")) // Verifica se colidiu com algo da tag "Boss"
        {
            float bossDamage = collision.gameObject.GetComponent<NewFirstBoss>().damage; // Pega o dano do boss
            TakeDamage(bossDamage); // Aplica o dano ao jogador
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = Life / lifeMax; // Atualiza a proporção da barra de vida
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        // Aqui você pode adicionar lógica adicional, como reiniciar a cena ou exibir uma tela de game over
    }
}
