using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBoss : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab do projétil
    public float fireRate = 1f;           // Tempo entre disparos
    public float projectileSpeed = 5f;    // Velocidade do projétil
    public float damageAmount = 10f; // Quantidade de dano que o boss causa

    private void Start()
    {
        StartCoroutine(FireProjectiles());
    }

    private IEnumerator FireProjectiles()
    {
        while (true)
        {
            FireInAllDirections();
            yield return new WaitForSeconds(fireRate);  // Espera pelo cooldown
        }
    }

    private void FireInAllDirections()
    {
        // Cria um array de direções
        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(1, 1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(-1, -1).normalized,
            new Vector2(1, -1).normalized
        };

        foreach (Vector2 direction in directions)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MazeShotter player = other.GetComponent<MazeShotter>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }
    }
}
