using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWeapon : MonoBehaviour
{
    public GameObject bullet;

    public Transform firePoint;

    public void Fire()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

}
