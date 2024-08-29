using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
public class BossLegal : MonoBehaviour
{
    public int damage = 2;
    public float speed;
    int direction = -1;
    int direction2 = 1;
    private int Life = 70;
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
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            direction = -1;

        }
       if (collision.gameObject.CompareTag("Wall"))
        {
            direction2 = -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            direction *= -1;
        }
        if (collision.CompareTag("Bullet"))
        {//Aqui o collision representa o Bullet
            Life -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);//Esse destroi o tiro
            if (Life <= 0)
            {

                Destroy(gameObject);//Esse destroi o inimigo
            }

        }
    }
}