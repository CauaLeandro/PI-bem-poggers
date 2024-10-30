using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_TopDown : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade do player
    public GameObject bulletPrefab; // Prefab da bullet
    public Transform firePoint; // da onde a bala sai
    public float bulletSpeed = 10f; // Velocidade da bullet

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePosition;

    
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);// faz o mouse aparecer na tela

        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();//faz o player atirar caso clique no mouse
        }
    }

    void FixedUpdate()
    {
        
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        
        rbBullet.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

    }

}
