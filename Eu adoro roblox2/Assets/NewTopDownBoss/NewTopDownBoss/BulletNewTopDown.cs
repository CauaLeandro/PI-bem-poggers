using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNewTopDown : MonoBehaviour
{
    public float lifeTime = 2f; // Tempo de vida da bullet

    void Start()
    {
       
        Destroy(gameObject, lifeTime);// a bullet desaparece depois de um tempo
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            Debug.Log("Colidiu com o boss!");
            collision.GetComponent<TopDownTrueBoss>().TakeDamage(50);
            Destroy(gameObject);
           
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destrói a bullet ao colidir com a parede
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {

            collision.gameObject.GetComponent<TopDownEnemy>().TakeDamage(50); // dano ao atingir o enemy
            Destroy(gameObject);
        }
    }
   
}
