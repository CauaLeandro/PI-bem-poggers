using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador
    public Vector3 offset;    // Offset para ajustar a posição da câmera em relação ao jogador
    public float smoothSpeed = 0.125f; // Velocidade da suavização da câmera

    void LateUpdate()
    {
        if (player != null)
        {
            // Define a posição desejada da câmera
            Vector3 desiredPosition = player.position + offset;

            // Suaviza a transição para a posição desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Atualiza a posição da câmera
            transform.position = smoothedPosition;
        }
    }
}
