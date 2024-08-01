using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 2;
    public int damage = 2;
    public float speed;
    public GameObject coin;
    int direction = -1;

    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            direction *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {//Aqui o collision representa o Bullet
            life -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);//Esse destroi o tiro
            if (life <= 0)
            {
                Instantiate(coin, transform.position, transform.rotation);
                Destroy(gameObject);//Esse destroi o inimigo
            }

        }
    }


}


