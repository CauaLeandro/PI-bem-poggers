using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFirstBoss : MonoBehaviour
{
    public float life = 100f; // Vida do Boss
    public float maxLife = 100f; // Vida máxima do Boss
    public float damage = 10f; // Dano do Boss

    public float speed = 3f;     // Velocidade de movimento do Boss

    private int direction = -1;  // Direção inicial do Boss
    private Rigidbody2D rb; 

    public GameObject objectToDeactivate; // Referência ao objeto associado que será desativado
    public Image lifeBar; // Barra de vida do Boss

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
     
        UpdateLifeBar();
    }

    private void Update()
    {
        Move();
        // Atualiza a barra de vida do Boss
        UpdateLifeBar();
    }
    private void Move()
    {
        // Faz o Boss se mover de um lado para outro
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // Inverte a direção ao bater na parede
            Flip();
        }

    }
        
    public void TakeDamage(float amount)
    {
        life -= amount;
        if (life <= 0)
        {
            Die();
        }
    }

    private void UpdateLifeBar()
    {
        if (lifeBar != null)
        {
            lifeBar.fillAmount = life / maxLife;
        }
    }

    private void Die()
    {
        // Desativa o objeto associado
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false); // Desativa o objeto
            Debug.Log("Objeto associado desativado.");
        }

        // Destrói o Boss
        Destroy(gameObject);

        Debug.Log("Boss morreu!");
    }
    private void Flip()
    {
        // Inverte a escala para mudar a direção visual
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}