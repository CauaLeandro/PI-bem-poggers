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

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject);//destroi a bullet depois de bater em algo
        
       
       if (collision.gameObject.CompareTag("Enemy"))
       {
         
         collision.gameObject.GetComponent<TopDownEnemy>().TakeDamage(50); // dano ao atingir o enemy
         Destroy(gameObject); // Destroi a bullet depois de atingir o enemy
       }
        
    }

}
