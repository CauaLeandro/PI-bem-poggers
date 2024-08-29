using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public int cenaAtual;
    void Start()
    {
        cenaAtual = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(cenaAtual + 1);
        }
    }
}
