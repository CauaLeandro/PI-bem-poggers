using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWeapon : MonoBehaviour
{
    public GameObject bullet;

    public Transform firePoint;

    public float fireForce;

    public void Fire()
    {
       GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);

    }

}
