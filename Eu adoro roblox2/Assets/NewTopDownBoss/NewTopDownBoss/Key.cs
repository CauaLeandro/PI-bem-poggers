using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject porta; // Referência ao objeto da porta

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador coletou a chave
        if (other.CompareTag("Player"))
        {
            // Desativa a porta
            porta.SetActive(false);

            // Desativa o coletável (a chave)
            gameObject.SetActive(false);
        }
    }
}

