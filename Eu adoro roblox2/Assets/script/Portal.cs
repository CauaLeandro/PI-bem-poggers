using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vegeta"))
        {
            SceneManager.LoadScene("LAbirinto");
        }
        if (collision.CompareTag("Goku"))
        {
            SceneManager.LoadScene("RabiesBoss");
        }
        if (collision.CompareTag("Freeza"))
        {
            SceneManager.LoadScene("Creditos");
        }
        if (collision.CompareTag("Gohan"))
        {
            SceneManager.LoadScene("Influenza boss battle");
        }
        if (collision.CompareTag("Tenshinhan")) 
        {
            SceneManager.LoadScene("TopDownBoss");
        }
    }
}
