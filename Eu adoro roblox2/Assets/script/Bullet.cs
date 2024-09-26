using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public int damage = 1;
    public float projectileForce = 500f;
    public RabiesBoss script;
    // Start is called before the first frame update
    void Start()
    {
       
        
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * projectileForce); // Aplique a força
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1.0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.gameObject.CompareTag("Boss"))
        {
            script.TakeDamage(damage);
            Destroy(gameObject);
        }
    
    }
    

}