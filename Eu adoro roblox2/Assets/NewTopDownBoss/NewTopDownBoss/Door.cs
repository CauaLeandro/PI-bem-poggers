using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 posicaoAberta; // Posi��o final da porta ao abrir
    public float velocidade = 2f; // Velocidade de abertura

    private Vector3 posicaoInicial;
    private bool abrir = false;

    private void Start()
    {
        // Salva a posi��o inicial da porta
        posicaoInicial = transform.position;
    }

    private void Update()
    {
        if (abrir)
        {
            // Move a porta para a posi��o aberta
            transform.position = Vector3.Lerp(transform.position, posicaoAberta, velocidade * Time.deltaTime);
        }
    }

    public void AbrirPorta()
    {
        abrir = true;
    }
}
