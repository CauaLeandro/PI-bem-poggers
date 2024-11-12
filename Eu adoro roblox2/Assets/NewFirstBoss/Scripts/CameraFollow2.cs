using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Transform player; // Refer�ncia ao Transform do jogador
    public Vector3 offset;    // Offset para ajustar a posi��o da c�mera em rela��o ao jogador
    public float smoothSpeed = 0.125f; // Velocidade da suaviza��o da c�mera

    void LateUpdate()
    {
        if (player != null)
        {
            // Define a posi��o desejada da c�mera
            Vector3 desiredPosition = player.position + offset;

            // Suaviza a transi��o para a posi��o desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Atualiza a posi��o da c�mera
            transform.position = smoothedPosition;
        }
    }
}
