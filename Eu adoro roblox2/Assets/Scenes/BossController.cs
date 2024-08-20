using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab do projétil que será disparado
    public Transform firePoint; // Ponto de origem do disparo dos projéteis

    public float moveSpeed = 5f; // Velocidade de movimento do boss
    public float fireRate = 1f; // Taxa de disparo dos projéteis (segundos entre disparos)

    private bool movingRight = true; // Direção inicial de movimento

    void Start()
    {
        // Começa a rotina de disparo de projéteis
        StartCoroutine(FireProjectiles());
    }

    void Update()
    {
        // Movimento horizontal do boss
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // Muda a direção quando atinge as bordas da área de jogo
        if (transform.position.x >= 8f)
        {
            movingRight = false;
        }
        else if (transform.position.x <= -8f)
        {
            movingRight = true;
        }
    }

    IEnumerator FireProjectiles()
    {
        while (true)
        {
            // Instancia um projétil no ponto de origem do disparo
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Aguarda pela próxima taxa de disparo
            yield return new WaitForSeconds(fireRate);
        }
    }
}
