using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownTrueBoss : MonoBehaviour
{

    public GameObject dropItemPrefab; // Prefab do item que ser� dropado
    public Transform dropPoint;

    [Header("Ataque")]
    public GameObject bulletPrefab; // Prefab da bala do boss
    public Transform firePoint; // Ponto de disparo da bala
    public float bulletSpeed = 10f; // Velocidade da bala
    public float shootInterval = 2f; // Tempo entre disparos
    private float timeSinceLastShot;

    [Header("Vida")]
    public int maxHealth = 100; // Vida m�xima do boss
    private int currentHealth; // Vida atual do boss
    public Image healthBarFill; // Refer�ncia para a barra de vida do boss

    private Transform player; // Refer�ncia para o player

    public float secondaryAttackInterval = 10f; // Intervalo do ataque secund�rio
    public int numberOfBullets = 8;
    private float lastSecondaryAttackTime;
    void Start()
    {
        // Inicializa a vida
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Busca o player na cena
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time >= lastSecondaryAttackTime + secondaryAttackInterval)
        {
            PerformSecondaryAttack();
            lastSecondaryAttackTime = Time.time; // Atualiza o tempo do �ltimo ataque
        }
        if (player == null) return;

        // Atira no player em intervalos de tempo
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shootInterval)
        {
            Shoot();
            timeSinceLastShot = 0f; // Reseta o tempo do �ltimo disparo
        }
    }

    void Shoot()
    {
        if (player == null) return;

        // Instancia a bala no firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calcula a dire��o para o player
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Define a rota��o da bala para apontar na dire��o do player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Aplica movimento na bala
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.velocity = direction * bulletSpeed;
    }

    public void TakeDamage(int damage)
    {
        // Reduz a vida do boss
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar(); // Atualiza a barra de vida

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        // Atualiza a barra de vida com base na vida atual
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Boss morreu!");
        Destroy(gameObject); // Remove o boss da cena
        if (dropItemPrefab != null && dropPoint != null)
        {
            Instantiate(dropItemPrefab, dropPoint.position, Quaternion.identity);
        }

    }
    void PerformSecondaryAttack()
    {
        float angleStep = 360f / numberOfBullets; // Divide o c�rculo pelas balas
        float angle = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calcula a dire��o para cada bala
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            // Instancia a bala
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Define a velocidade da bala
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            rbBullet.velocity = direction * bulletSpeed;

            // Incrementa o �ngulo para a pr�xima bala
            angle += angleStep;
        }
    }
}
