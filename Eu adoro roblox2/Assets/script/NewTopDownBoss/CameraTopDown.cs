using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopDown : MonoBehaviour
{
    public Transform player;  
    public float offsetY = 0f; 
    public float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = new Vector3(player.position.x, transform.position.y + offsetY, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

