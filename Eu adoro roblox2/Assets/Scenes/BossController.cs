using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab do proj�til que ser� disparado
    public Transform firePoint; // Ponto de origem do disparo dos proj�teis

    public float moveSpeed = 5f; // Velocidade de movimento do boss
    public float fireRate = 1f; // Taxa de disparo dos proj�teis (segundos entre disparos)

    private bool movingRight = true; // Dire��o inicial de movimento

    void Start()
    {
        // Come�a a rotina de disparo de proj�teis
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

        // Muda a dire��o quando atinge as bordas da �rea de jogo
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
            // Instancia um proj�til no ponto de origem do disparo
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Aguarda pela pr�xima taxa de disparo
            yield return new WaitForSeconds(fireRate);
        }
    }
}
