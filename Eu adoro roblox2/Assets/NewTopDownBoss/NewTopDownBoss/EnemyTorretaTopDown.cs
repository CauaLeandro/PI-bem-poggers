using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTorretaTopDown : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab da bala
    public Transform firePoint; // Ponto de disparo
    public float shootingInterval = 2f; // Intervalo de disparo em segundos
    public float bulletSpeed = 10f; // Velocidade da bala

    private float timeSinceLastShot = 0f; // Tempo desde o último disparo
    private GameObject player; // Referência ao jogador
    private Collider2D enemyCollider; // Collider do inimigo

    void Start()
    {
        // Encontra o jogador pela tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        // Obtém o Collider2D do inimigo
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Verifica se o jogador existe e se o tempo para o próximo disparo foi atingido
        if (player != null && timeSinceLastShot >= shootingInterval)
        {
            Shoot(); // Chama a função de disparar
            timeSinceLastShot = 0f; // Reseta o tempo do último disparo
        }
    }

    void Shoot()
    {
        if (player != null)
        {
            // Cria a bala no ponto de disparo
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Ignora colisão entre o inimigo e a bala
            Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(bulletCollider, enemyCollider);

            // Calcula a direção do jogador a partir da posição do firePoint
            Vector2 direction = (player.transform.position - firePoint.position).normalized;

            // Aplica a direção e velocidade à bala
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;
        }
    }
}


